using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using Tracking.Core.Config;
using Tracking.Core.Debug;
using Tracking.Core.Maths;
using Tracking.Core.Processing;
using Tracking.Core.Utils;
using Tracking.Core.Video;
using Tracking.Model;

namespace Tracking.Core
{
    /// <summary>
    /// Provides the main entry point for the Tracking system.
    /// It sets up and manages the video source, configuration,
    /// calibration and tracking.
    /// </summary>
    public class TrackingEngine : IFrameHandler, ICoordinateTransformer
    {
        public event EventHandler<Frame> FrameAvailable;
        public event EventHandler CalibrationCompleted;

        public bool IsCalibrated => calibrationHandler?.Result.HasData == true;

        private const string ConfigFileName = "config.json";
        private const string CalibrationResultsFileName = "calibration.json";

        private IVideoSource currentVideoSource;
        private IFrameHandler currentChildHandler;

        private readonly CalibrationHandler calibrationHandler;
        private readonly TrackingHandler trackingHandler;
        private readonly BallClassifier ballClassifier;

        public TrackingEngine()
        {
            currentVideoSource = new KinectVideoSource
            {
                FrameHandler = this
            };

            var config = JsonFileIO.Load<ConfigFile>(ConfigFileName);
            trackingHandler = new TrackingHandler(config.Tracking, this);
            calibrationHandler = new CalibrationHandler(config.Calibration);
            calibrationHandler.CalibrationCompleted += CalibrationHandler_CalibrationCompleted;
            ballClassifier = new BallClassifier(config.Classifier);

            var calibrationData = JsonFileIO.Load<CalibrationResult>(CalibrationResultsFileName);
            if (calibrationData != null)
            {
                calibrationHandler.Result.DepthBaseframe = calibrationData.DepthBaseframe;
                calibrationHandler.Result.PlayfieldTransform = calibrationData.PlayfieldTransform;
                currentChildHandler = trackingHandler;
            }
            else
            {
                currentChildHandler = calibrationHandler;
            }
        }

        /// <summary>
        /// Resets the tracking system
        /// </summary>
        public void Reset()
        {
            trackingHandler.Reset();
            ballClassifier.Reset();
        }

        /// <summary>
        /// Start the engine
        /// </summary>
        public void Open()
        {
            currentVideoSource.Open();
        }

        /// <summary>
        /// Shut down the engine
        /// </summary>
        public void Close()
        {
            currentVideoSource.Close();
        }

        /// <summary>
        /// Begin calibrating
        /// </summary>
        public void Calibrate()
        {
            calibrationHandler.Reset();
            currentChildHandler = calibrationHandler;
        }

        private void CalibrationHandler_CalibrationCompleted(object sender, EventArgs e)
        {
            currentChildHandler = trackingHandler;
            JsonFileIO.Store(CalibrationResultsFileName, calibrationHandler.Result);
            CalibrationCompleted.Invoke(this, null);
        }

        public void HandleFrame(VideoFrame frame)
        {
            if (calibrationHandler.Result.HasData)
            {
                CvInvoke.WarpPerspective(frame.Depth, frame.Depth, calibrationHandler.Result.PlayfieldTransform, frame.Depth.Size);
                CvInvoke.WarpPerspective(frame.Color, frame.Color, calibrationHandler.Result.PlayfieldTransform, frame.Color.Size);
                ImageUtil.InvertImage(frame.Depth.Mat);

                using (var baseImage = calibrationHandler.Result.DepthBaseframe.ToImage<Gray, byte>())
                    frame.Depth -= baseImage;

                FrameTracer.Trace("Baseframe", calibrationHandler.Result.DepthBaseframe);
            }

            FrameTracer.Trace("Raw depth", frame.Depth.Mat);
            FrameTracer.Trace("Raw color", frame.Color.Mat);
            FrameTracer.Trace("Original color", frame.ColorOriginal.Mat);

            currentChildHandler?.HandleFrame(frame);
            ballClassifier.Update(trackingHandler.TrackedCircles, frame.Color.Size);

            if (FrameTracer.IsEnabled)
            {
                foreach (var ball in ballClassifier.Balls)
                {
                    if (!ball.OnTable) continue;
                    frame.Color.Draw(ball.Type.ToString(), ball.RawPosition.ToInt(), FontFace.HersheyPlain, 0.55f, new Bgra(255, 100, 0, 255));
                }
                FrameTracer.Trace("Classifier result", frame.Color.Mat);
            }

            var currentFrame = ConstructFrame();
            FrameAvailable.Invoke(this, currentFrame);
        }

        /// <summary>
        /// Creates a new frame instance from
        /// the current state.
        /// </summary>
        /// <returns>Frame instance</returns>
        private Frame ConstructFrame()
        {
            return new Frame
            {
                Balls = ballClassifier.Balls,
                CueStick = trackingHandler.CueStick
            };
        }

        // This implements the interface
        public PointF TransformPlayfieldToRaw(PointF point)
        {
            var transform = calibrationHandler.Result.PlayfieldTransform;
            var inverseTransform = new Mat();
            CvInvoke.Invert(transform, inverseTransform, DecompMethod.Cholesky);
            return CvInvoke.PerspectiveTransform(new PointF[] { point }, inverseTransform)[0].Mul(1 / (float)currentVideoSource.ColorFrameScale);
        }
    }
}
