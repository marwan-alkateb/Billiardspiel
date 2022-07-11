using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Tracking.Core.Config;
using Tracking.Core.Maths;
using Tracking.Core.Utils;
using Tracking.Core.Video;

namespace Tracking.Core.Processing
{
    internal class CalibrationHandler : IFrameHandler
    {
        public CalibrationResult Result { get; } = new CalibrationResult();

        public event EventHandler CalibrationCompleted;

        private readonly CalibrationConfig config;

        private int numFramesRecorded;
        private IList<PointF[]> playfieldBoundsAccum = new List<PointF[]>();
        private Image<Gray, int> depthAccum;

        public CalibrationHandler(CalibrationConfig config)
        {
            this.config = config;
        }

        public void HandleFrame(VideoFrame frame)
        {
            if (depthAccum == null)
            {
                depthAccum = new Image<Gray, int>(frame.Depth.Width, frame.Depth.Height);
            }

            if (numFramesRecorded < config.NumFrames)
            {
                RecordSingleFrame(frame.Depth);
                numFramesRecorded++;
            }
            else if (numFramesRecorded == config.NumFrames)
            {
                HandleCalibrationCompleted();
            }
        }

        /// <summary>
        /// Discards all calibration information
        /// </summary>
        public void Reset()
        {
            numFramesRecorded = 0;
            playfieldBoundsAccum.Clear();
            depthAccum = null;
            Result.PlayfieldTransform = null;
            Result.DepthBaseframe = null;
        }

        /// <summary>
        /// Runs the calibration algorithm for a single depth frame
        /// and updates the accumulators accordingly
        /// </summary>
        /// <param name="depthFrame">The depth frame to record</param>
        private void RecordSingleFrame(Image<Gray, byte> depthFrame)
        {
            var tableBounds = FindTableBounds(depthFrame);
            if (tableBounds.GetArea() < config.MinPlayfieldArea)
                return;

            var playfieldBounds = FindPlayfieldBounds(depthFrame, tableBounds);
            var vertices = OrderPointsClockwise(playfieldBounds.GetVertices(), playfieldBounds.Center);
            playfieldBoundsAccum.Add(vertices);
            depthAccum += depthFrame.Convert<Gray, int>();
        }

        /// <summary>
        /// Computes the calibration result after all frames have been
        /// recorded and invokes the callback.
        /// </summary>
        private void HandleCalibrationCompleted()
        {
            // Compute average of the playfield bounding rectangle
            var averagedPlayfieldBounds = new PointF[4];
            for (int i = 0; i < 4; i++)
            {
                var points = playfieldBoundsAccum.Select(pt => pt[i]);
                var xAvg = points.Average(p => p.X);
                var yAvg = points.Average(p => p.Y);
                averagedPlayfieldBounds[i] = new PointF(xAvg, yAvg);
            }

            // Compute the playfield transformation matrix
            Result.PlayfieldTransform = ComputePlayfieldTransform(averagedPlayfieldBounds, depthAccum.Width, depthAccum.Height);

            // Compute the depth baseframe
            depthAccum /= config.NumFrames;
            var baseframe = depthAccum.Convert<Gray, byte>();
            CvInvoke.WarpPerspective(baseframe, baseframe, Result.PlayfieldTransform, baseframe.Size);
            ImageUtil.InvertImage(baseframe.Mat);
            Result.DepthBaseframe = baseframe.Mat;

            // Call the event
            CalibrationCompleted.Invoke(this, null);
        }

        /// <summary>
        /// Takes the depth frame and runs a Canny edge detection filter.
        /// Gaps in the edges are closed using a morphological filter.
        /// </summary>
        /// <param name="depthFrame">Input depth frame</param>
        /// <returns>Edges of the input</returns>
        private Image<Gray, byte> FindEdges(Image<Gray, byte> depthFrame)
        {
            // Canny edge detection
            var edgeDepthFrame = depthFrame.Canny(200, 255);

            // Close gaps in edges using morphological filter
            var kernel = CvInvoke.GetStructuringElement(ElementShape.Ellipse, new Size(7, 7), new Point(-1, -1));
            CvInvoke.MorphologyEx(edgeDepthFrame, edgeDepthFrame, MorphOp.Close, kernel, new Point(-1, -1), 1, BorderType.Constant, new MCvScalar(0));

            return edgeDepthFrame;
        }

        /// <summary>
        /// Takes the depth image and finds the boundary of the 
        /// billard table.
        /// </summary>
        /// <param name="depthFrame">Depth image</param>
        /// <returns>Billard table bounding rectangle</returns>
        private RotatedRect FindTableBounds(Image<Gray, byte> depthFrame)
        {
            // Find edges in the image
            using (var edgeDepthFrame = FindEdges(depthFrame))
            {
                // Find contours in the edge image
                var contours = new VectorOfVectorOfPoint();
                var hierarchy = new Mat();
                CvInvoke.FindContours(edgeDepthFrame, contours, hierarchy, RetrType.External, ChainApproxMethod.ChainApproxSimple);

                // Find rectangle of largest contour
                var largestContour = contours.FindLargestItem();
                return CvInvoke.MinAreaRect(largestContour);
            }
        }

        /// <summary>
        /// Finds the bounding rectangle of the playfield area
        /// </summary>
        /// <param name="depthFrame">Depth image</param>
        /// <param name="tableBounds">Previously determined bounding rectangle of the table</param>
        /// <returns>Bounding rectangle of the playfield area</returns>
        private RotatedRect FindPlayfieldBounds(Image<Gray, byte> depthFrame, RotatedRect tableBounds)
        {
            var edges = tableBounds.GetEdges();
            var tableCenter = tableBounds.Center.ToInt();
            var boundPoints = new List<PointF>();

            foreach (var edge in edges)
            {
                // For each edge of the table, take a few sample points, and from
                // those points walk towards the center of the table. Where the depth value
                // increases significantly, that's where the edge of the playfield is. Save
                // this point to the list of bound points.

                foreach (var sampleDist in config.DepthSampleDistances)
                {
                    var samplePoint = edge.PointOnLine(sampleDist).ToInt()
                        .MoveTowards(tableCenter, 10);

                    var baseDepth = 0.0;
                    Bresenham.Walk(samplePoint, tableCenter, pt =>
                    {
                        var depthValue = depthFrame[pt].Intensity;

                        // No base depth value yet?
                        if (baseDepth == 0 && depthValue != 0)
                        {
                            baseDepth = depthValue;
                            return true; // Continue iterating
                        }

                        // Found the edge?
                        if (depthValue - baseDepth > config.DepthSampleThreshold)
                        {
                            boundPoints.Add(pt);
                            return false; // We're finished
                        }

                        // Nothing found, go on...
                        return true;
                    });
                }
            }

            // Find the rectangle around the list of bounding points we found!
            return CvInvoke.MinAreaRect(boundPoints.ToArray());
        }

        /// <summary>
        /// Computes a Matrix that, when applied to an image frame, transforms the 
        /// image in such a way that the playfield completely fills the frame
        /// as an axis aligned rectangle
        /// </summary>
        /// <remarks>
        /// WARNING: Assumes that playfieldBounds are ordered clockwise, starting at the top left corner
        /// </remarks>
        /// <param name="playfieldBounds">Vertices of the rectangle that make up the playfield</param>
        /// <param name="dstWidth">Width of the destination image</param>
        /// <param name="dstHeight">Height of the destination image</param>
        /// <returns>The transformation matrix</returns>
        private Mat ComputePlayfieldTransform(PointF[] playfieldBounds, float dstWidth, float dstHeight)
        {
            var dstVerts = new PointF[]
            {
                new PointF(0, 0), // Top left corner
                new PointF(dstWidth - 1, 0), // Top right corner
                new PointF(dstWidth - 1, dstHeight - 1), // Bottom right corner
                new PointF(0, dstHeight - 1) // Bottom left corner
            };

            return CvInvoke.GetPerspectiveTransform(playfieldBounds, dstVerts);
        }

        /// <summary>
        /// Orders the points clockwise around a center point,
        /// starting at the top left corner
        /// </summary>
        /// <param name="points">Array of points to sort</param>
        /// <param name="center">Center point</param>
        /// <returns>Points ordered clockwise</returns>
        private PointF[] OrderPointsClockwise(PointF[] points, PointF center)
        {
            // TODO: Can we do this more efficiently (i.e. without the Atan2?)
            return points.OrderBy(p => Math.Atan2(p.Y - center.Y, p.X - center.X)).ToArray();
        }
    }
}
