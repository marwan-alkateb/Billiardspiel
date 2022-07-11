using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Tracking.Core.Config;
using Tracking.Core.Debug;
using Tracking.Core.Maths;
using Tracking.Core.Utils;
using Tracking.Core.Video;
using Tracking.Model;

namespace Tracking.Core.Processing
{
    internal class TrackingHandler : IFrameHandler
    {
        public CueStick CueStick { get; private set; } = default;

        public IEnumerable<TrackedCircle> TrackedCircles => circleManager.Circles;

        public double AverageSpeed => circleManager.CircleCount != 0 ? TrackedCircles.Average(c => c.CurrentCenter.DistanceTo(c.PreviousCenter)) : 0;

        private readonly TrackedCircleManager circleManager = new TrackedCircleManager();
        private readonly TrackingConfig config;
        private readonly ICoordinateTransformer transformer;

        private readonly Mat heightmapLowRes = new Mat();
        private readonly Mat heightmapHighRes = new Mat();

        private Image<Gray, byte> binarizedDepth;
        private readonly Mat filterMat1 = new Mat();
        private readonly Mat filterMat2 = new Mat();

        public TrackingHandler(TrackingConfig config, ICoordinateTransformer transformer)
        {
            this.config = config;
            this.transformer = transformer;
        }

        public void HandleFrame(VideoFrame frame)
        {
            // Preprocessing
            ApplyHeightmapCorrection(frame.Depth.Mat);
            ApplyPocketCorrection(frame.Depth);
            var binarized = BinarizeDepth(frame.Depth);

            // Cue stick tracking
            CueStick = FindCueStick(frame.Color, frame.Depth, binarized);

            // Ball tracking
            var candidates = FindCandidates(binarized);
            TrackCandidates(candidates);
            RecomputeColorValues(frame);

            // If the tracer is attached, create a debug frame with all tracked circles highlighted
            if (FrameTracer.IsEnabled)
                CreateDebugFrame(frame);
        }

        /// <summary>
        /// Creates a debug frame for the 
        /// </summary>
        /// <param name="frame"></param>
        private void CreateDebugFrame(VideoFrame frame)
        {
            foreach (var circle in TrackedCircles)
            {
                var circleF = circle.ToCircleF(config.BallRadius);
                frame.Color.Draw(circleF, circle.ColorBuffer.Average(), -1);
                frame.Color.Draw(circleF, new Bgra(255, 0, 0, 255), 1);

                var velocityVector = new LineSegment2DF(circle.CurrentCenter, circle.CurrentCenter.Add(circle.VelocityBuffer.Average().Mul(2)));
                if (velocityVector.Length > 1)
                {
                    frame.Color.Draw(velocityVector, new Bgra(255, 0, 255, 255), 2);
                }
            }
            frame.Color.Draw(new LineSegment2D(CueStick.SrcPoint.ToInt(), CueStick.TablePoint.ToInt()), new Bgra(255, 255, 0, 255), 4);
            frame.Color.Draw(new Cross2DF(CueStick.TablePoint, 5, 5), CueStick.HasTouched ? new Bgra(0, 255, 0, 255) : new Bgra(0, 255, 255, 255), 4);
            FrameTracer.Trace("Tracking result", frame.Color.Mat);
        }

        /// <summary>
        /// Tries to find the Cue Stick in the depth frame
        /// </summary>
        /// <param name="depthFrame">Raw depth frame</param>
        /// <param name="depthFrameBinarized">Binarized depth frame</param>
        /// <returns>Information about the cue stick position and state</returns>
        private CueStick FindCueStick(Image<Bgra, byte> color, Image<Gray, byte> depthFrame, Image<Gray, byte> depthFrameBinarized)
        {
            // Find coarse position using Hough transform
            var lines = depthFrameBinarized.HoughLinesBinary(1, Math.PI / 180, 100, 0, 0)[0];
            var orderedLines = lines.Where(l => l.Length > 10)
                .OrderByDescending(l => l.Length)
                .ToList();

            if (orderedLines.Count == 0)
            {
                return new CueStick { IsVisible = false };
            }
            else
            {
                // Take the longest line, and orient it so that it follows the depth gradient.
                // This makes sure that the pointy end of the cue is the second point.
                var line = orderedLines[0];
                var points = new PointF[] { line.P1, line.P2 }
                    .OrderByDescending(p => depthFrame[(int)p.Y, (int)p.X].Intensity)
                    .ToList();

                // The position determined by Hough not very accurate, since it operates on the
                // binarized image gradient. Therefore, we follow the line manually along the depth
                // gradient to determine the endpoint.
                var traceSrcPoint = points[0].ToInt();
                var traceDstPoint = points[0].ToInt().MoveTowards(points[1].ToInt(), (int)(line.Length * 2));

                Bresenham.Walk(traceSrcPoint, traceDstPoint, p =>
                {
                    if (p.X < 0 || p.Y < 0 || p.X >= depthFrame.Width || p.Y >= depthFrame.Height)
                        return false;

                    var depthVal = depthFrame[p.Y, p.X].Intensity;
                    if (depthVal > 3)
                    {
                        points[1] = p;
                        return true;
                    }

                    return false;
                });

                // TODO: Touch detection
                return new CueStick()
                {
                    SrcPoint = points[0],
                    TablePoint = points[1],
                    IsVisible = true,
                    HasTouched = false,
                };
            }
        }

        /// <summary>
        /// Using a nearest neighbor algorithm, tries to find which known
        /// balls have moved to which positions, and which balls are new
        /// compared to the last frame.
        /// </summary>
        /// <param name="candidates">A list of points where a ball candidate was detected in the current frame</param>
        private void TrackCandidates(List<PointF> candidates)
        {
            // Mark all existing points as inactive
            foreach (var circle in TrackedCircles)
                circle.Active = false;

            // For each new point, find which TrackedCircles it could match
            var candidateMap = new Dictionary<int, List<PointF>>();
            foreach (var candidate in candidates)
            {
                var closest = circleManager.FindClosestTo(candidate);
                if (closest == null)
                {
                    circleManager.Register(candidate);
                }
                else
                {
                    if (!candidateMap.ContainsKey(closest.Id))
                        candidateMap[closest.Id] = new List<PointF> { candidate };
                    else
                        candidateMap[closest.Id].Add(candidate);
                }
            }

            // Nearest neighbor assignment
            foreach (var pair in candidateMap)
            {
                var circle = circleManager.FindById(pair.Key);

                var closestPos = circle.CurrentCenter;
                var closestDist = 999.9f;
                foreach (var nextPoint in pair.Value)
                {
                    var dist = circle.CurrentCenter.DistanceTo(nextPoint);
                    if (dist < closestDist)
                    {
                        closestPos = nextPoint;
                        closestDist = dist;
                    }
                }

                // Update position and velocity
                circle.PreviousCenter = circle.CurrentCenter;
                circle.CurrentCenter = closestPos;
                circle.PositionBuffer.Push(closestPos);
                if (!circle.PreviousCenter.IsEmpty)
                {
                    var velocity = circle.PreviousCenter.Sub(circle.CurrentCenter);
                    circle.VelocityBuffer.Push(velocity);
                }

                // Update active state
                circle.Active = true;
                circle.ActiveFrames++;
            }

            // Handle new circles
            foreach (var candidate in candidates)
                if (!circleManager.ContainsAt(candidate))
                    circleManager.Register(candidate);

            // Discard inactive circles
            circleManager.DiscardInactive();
        }

        /// <summary>
        /// Converts the raw depth frame into a thresholded and denoised binary frame
        /// which is easier to operate on
        /// </summary>
        /// <param name="depthFrame">Raw depth frame</param>
        /// <returns>Binarized frame</returns>
        private Image<Gray, byte> BinarizeDepth(Image<Gray, byte> depthFrame)
        {
            if (binarizedDepth == null)
            {
                binarizedDepth = new Image<Gray, byte>(depthFrame.Width, depthFrame.Height);
            }

            // High pass filter
            CvInvoke.GaussianBlur(depthFrame, filterMat1, new Size(config.HighPassGaussSize, config.HighPassGaussSize), 0);
            CvInvoke.Subtract(depthFrame, filterMat1, filterMat2);
            CvInvoke.Add(filterMat2, filterMat2, filterMat2);

            // Denoising with bilateral filter
            CvInvoke.BilateralFilter(filterMat2, binarizedDepth, -1, config.BilateralSize, config.BilateralSize);

            // Binarize the balls
            CvInvoke.Threshold(binarizedDepth, binarizedDepth, config.BallDepthThreshold, 255, ThresholdType.Binary);
            FrameTracer.Trace("Binarized", binarizedDepth.Mat);

            return binarizedDepth;
        }

        /// <summary>
        /// Finds possible billard balls from the depth frame
        /// </summary>
        /// <param name="depthFrame">The depth frame</param>
        /// <returns>Center points of the ball circles</returns>
        private List<PointF> FindCandidates(Image<Gray, byte> depthFrame)
        {
            // Contour detection
            using (var contours = new VectorOfVectorOfPoint())
            using (var hierarchy = new Mat())
            {
                CvInvoke.FindContours(depthFrame, contours, hierarchy, RetrType.External, ChainApproxMethod.ChainApproxSimple);

                var candidates = new List<PointF>();
                for (var i = 0; i < contours.Size; i++)
                {
                    var contour = contours[i];
                    var contourCircle = CvInvoke.MinEnclosingCircle(contour);
                    var contourArea = CvInvoke.ContourArea(contour);
                    var confidence = contourArea / contourCircle.Area;

                    if (confidence > config.BallCircularityThreshold && contourArea > config.BallAreaThreshold)
                    {
                        candidates.Add(contourCircle.Center);
                    }
                }

                return candidates;
            }
        }


        /// <summary>
        /// Eliminates noise in the corners by filling the pockets with
        /// solid black
        /// </summary>
        /// <param name="depthFrame"></param>
        private void ApplyPocketCorrection(Image<Gray, byte> depthFrame)
        {
            var pocketPoints = new Point[]
            {
                new Point(0, 0),
                new Point(depthFrame.Width, 0),
                new Point(0, depthFrame.Height),
                new Point(depthFrame.Width, depthFrame.Height),
            };
            foreach (var point in pocketPoints)
            {
                CvInvoke.Circle(depthFrame, point, 11, new MCvScalar(0), -1);
            }
        }

        /// <summary>
        /// Extract a heightmap from the current depth frame and renormalize
        /// the depth frame to that heightmap, to correct for the table not 
        /// being entirely flat and not at a depth value of 0
        /// </summary>
        /// <param name="depthFrame">Current depth frame</param>
        private void ApplyHeightmapCorrection(Mat depthFrame)
        {
            // Downscale depthframe
            CvInvoke.Resize(depthFrame, heightmapLowRes, Size.Empty, config.HeightmapScale, config.HeightmapScale);

            // "Low pass filter" (median blur) for heightmap extraction
            CvInvoke.MedianBlur(heightmapLowRes, heightmapLowRes, config.HeightmapMedianSize);

            // Scale heightmap to size of depth frame, and subtract it from the depth frame
            CvInvoke.Resize(heightmapLowRes * config.HeightmapIntensity, heightmapHighRes, depthFrame.Size);
            CvInvoke.Subtract(depthFrame, heightmapHighRes, depthFrame);

            FrameTracer.Trace("Heightmap", heightmapHighRes);
        }

        /// <summary>
        /// Extracts the color portion from an arbitrarily oriented billard ball
        /// </summary>
        /// <param name="frame">The current video frame</param>
        /// <param name="circleCenter">A point inside the billard ball circle</param>
        /// <returns>Average HSV and percentage of non-white pixels</returns>
        private unsafe (Hsv average, float colorPercentage) ExtractCircleColor(VideoFrame frame, PointF circleCenter)
        {
            var image = new FastImage(frame.ColorOriginal);

            var totalAverageBuf = new HsvAverageBuffer();
            var perRayAverageBuf = new HsvAverageBuffer();

            var srcPos = transformer.TransformPlayfieldToRaw(circleCenter);

            int colorPixels = 0;
            int totalPixels = 0;

            // Rotate around in a circle, with the given angular offset
            double angleOffset = (double) config.ExtractionAngle;
            for (double angle = 0; angle < 2 * Math.PI; angle += Math.PI * angleOffset)
            {
                // End pos of ray is 26 pixels away
                var dx = Math.Sin(angle) * config.ExtractionRadius;
                var dy = Math.Cos(angle) * config.ExtractionRadius;
                var dstPos = new Point((int)(srcPos.X + dx), (int)(srcPos.Y + dy));

                // Reset average
                perRayAverageBuf.Reset();

                // Raycast!
                Bresenham.Walk(srcPos.ToInt().MoveTowards(dstPos, 1), dstPos, pos =>
                {
                    // Don't walk outside the screen
                    if (pos.X < 0 || pos.Y < 0 || pos.X >= image.Width || pos.Y >= image.Height)
                        return false;

                    // Get pixel at that pos
                    var color = image[pos.X, pos.Y];
                    var colorHsv = color.ToHsv();

                    // Here, we abuse a property of the billiard balls, that they are not 100% white, but a yellowish off-white
                    // It has its hue center at 33, a low saturation and a high value. We compute a unitless factor that determines
                    // How close the pixel is to that color
                    var inverseHueDistanceFac = 1.0 - MathUtil.Clamp(Math.Abs(colorHsv.Hue - 33) / 33.0, 0.0, 1.0);
                    var inverseValueFac = 1.0 - (colorHsv.Value / 255.0);
                    var saturationFac = colorHsv.Satuation / 255.0;
                    var offWhiteFactor = MathUtil.Clamp(inverseHueDistanceFac - inverseValueFac - saturationFac, 0.0, 1.0);
                    var isOffWhitePixel = offWhiteFactor > 0;

                    // If we are NOT inside the white portion, sample the ball color
                    if (!isOffWhitePixel)
                    {
                        // Ignore pixels which have TOO low of a saturation
                        if (saturationFac < 0.1)
                            return true;

                        // Check if pixel is too far away from the average (is on table / different ball).
                        var rayAvg = perRayAverageBuf.Average;
                        var distance = colorHsv.DistanceTo(rayAvg);
                        if (perRayAverageBuf.Counter > 3 && (distance.Hue > config.ExtractionHueThreshold || distance.Satuation > config.ExtractionSaturationThreshold || distance.Value > config.ExtractionValueThreshold))
                            return true;

                        // Update averages
                        perRayAverageBuf.Push(colorHsv);
                        totalAverageBuf.Push(colorHsv, true);
                        colorPixels++;
                    }
                    totalPixels++;

                    return true;
                });
            }

            return (totalAverageBuf.Average, (float)colorPixels / totalPixels);
        }


        /// <summary>
        /// Recomputes the colors for all tracked circles
        /// </summary>
        /// <param name="colorFrame">The current color frame</param>
        private void RecomputeColorValues(VideoFrame frame)
        {
            foreach (var circle in TrackedCircles)
            {
                var (average, percentage) = ExtractCircleColor(frame, circle.CurrentCenter);
                circle.ColorPercentageBuffer.Push(percentage);
                circle.CurrentColor = average.ToBgra();
                circle.ColorBuffer.Push(circle.CurrentColor);
            }
        }

        /// <summary>
        /// Clears the list of known circles and resets the tracking algorithm
        /// </summary>
        public void Reset()
        {
            circleManager.Clear();
        }
    }
}
