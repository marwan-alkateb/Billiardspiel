using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Microsoft.Kinect;
using System;
using System.Runtime.CompilerServices;
using Tracking.Core.Debug;
using Tracking.Core.Utils;

namespace Tracking.Core.Video
{
    /// <summary>
    /// An implementation for IVideoSource that supports the
    /// Microsoft Kinect camera
    /// </summary>
    internal class KinectVideoSource : IVideoSource
    {
        public Image<Bgra, byte> ColorFrameOriginal { get; private set; }

        public Image<Bgra, byte> ColorFrame { get; private set; }

        public Image<Gray, byte> DepthFrame { get; private set; }

        public IFrameHandler FrameHandler { get; set; }

        public double ColorFrameScale => FrameScale;

        // Kinect instances
        private KinectSensor sensor;
        private DepthFrameReader depthFrameReader;
        private ColorFrameReader colorFrameReader;

        // Frame storage
        private ushort[] depthData;
        private ColorSpacePoint[] depthDataMappedToColor;
        private CameraSpacePoint[] depthDataMappedToCamera;

        private double depthFrameTimestamp;
        private double colorFrameTimestamp;

        // Kinect-specific configuration
        private const double MaxDivergence = 2 * 6.25;
        private const double FrameScale = 0.25; // Make depth texture 0.25x the size of the color texture.
        private const float MinDepth = 1.75f; // Minimum depth cutoff (meters)
        private const float MaxDepth = 2.05f; // Maximum depth cutoff (meters)

        public void Open()
        {
            if (sensor != null)
                throw new InvalidOperationException("The source is already open");

            sensor = KinectSensor.GetDefault();
            sensor.Open();
            depthFrameReader = sensor.DepthFrameSource.OpenReader();
            colorFrameReader = sensor.ColorFrameSource.OpenReader();
            depthFrameReader.FrameArrived += DepthFrameReader_FrameArrived;
            colorFrameReader.FrameArrived += ColorFrameReader_FrameArrived;
        }

        public void Close()
        {
            if (sensor == null)
                throw new InvalidOperationException("The source is already closed");
            depthFrameReader.Dispose();
            colorFrameReader.Dispose();
            sensor.Close();
            sensor = null;
        }

        private void ColorFrameReader_FrameArrived(object sender, ColorFrameArrivedEventArgs e)
        {
            using (var frame = e.FrameReference.AcquireFrame())
            {
                if (frame == null) return;

                // Initialize the color frame
                var frameDesc = frame.FrameDescription;
                if (ColorFrame == null)
                {
                    ColorFrame = new Image<Bgra, byte>((int)(frameDesc.Width * FrameScale), (int)(frameDesc.Height * FrameScale));
                    ColorFrameOriginal = new Image<Bgra, byte>(frameDesc.Width, frameDesc.Height);
                }

                // Copy frame data
                var dstMat = ColorFrameOriginal.Mat;
                frame.CopyConvertedFrameDataToIntPtr(dstMat.DataPointer, (uint)(dstMat.ElementSize * dstMat.Width * dstMat.Height), ColorImageFormat.Bgra);

                // Resize frame
                CvInvoke.Resize(ColorFrameOriginal, ColorFrame, ColorFrame.Size, 0, 0, Inter.Linear);
                colorFrameTimestamp = frame.RelativeTime.TotalMilliseconds;

                // Invoke handler
                var divergence = colorFrameTimestamp - depthFrameTimestamp;
                if (DepthFrame != null && divergence < MaxDivergence)
                {
                    FrameHandler?.HandleFrame(new VideoFrame(DepthFrame, ColorFrame, ColorFrameOriginal, divergence));
                }
            }
        }

        private void DepthFrameReader_FrameArrived(object sender, DepthFrameArrivedEventArgs e)
        {
            using (var frame = e.FrameReference.AcquireFrame())
            {
                if (frame == null || ColorFrame == null) return;

                // Initialize the depth frame
                var frameDesc = frame.FrameDescription;
                if (DepthFrame == null)
                {
                    DepthFrame = new Image<Gray, byte>(ColorFrame.Width, ColorFrame.Height);

                    var arraySize = frameDesc.Width * frameDesc.Height;
                    depthData = new ushort[arraySize];
                    depthDataMappedToCamera = new CameraSpacePoint[arraySize];
                    depthDataMappedToColor = new ColorSpacePoint[arraySize];
                }

                // Copy frame data
                frame.CopyFrameDataToArray(depthData);

                // Create undistorted depth image
                UndistortDepthFrame(frameDesc.Width, frameDesc.Height);
                depthFrameTimestamp = frame.RelativeTime.TotalMilliseconds;
            }
        }

        /// <summary>
        /// Takes the raw depth frame and undistorts it into
        /// the same coordinate space as the color frame
        /// </summary>
        /// <param name="srcWidth">Width of the distorted input frame</param>
        /// <param name="srcHeight">Height of the distorted input frame</param>
        private unsafe void UndistortDepthFrame(int srcWidth, int srcHeight)
        {
            // Create coordinate mapping
            sensor.CoordinateMapper.MapDepthFrameToCameraSpace(depthData, depthDataMappedToCamera);
            sensor.CoordinateMapper.MapDepthFrameToColorSpace(depthData, depthDataMappedToColor);

            // Destination setup
            var dstWidth = DepthFrame.Width;
            var dstHeight = DepthFrame.Height;
            var dstPtr = (byte*)DepthFrame.Mat.DataPointer.ToPointer();
            MemoryUtil.ZeroMemory(dstPtr, dstWidth * dstHeight);

            // Create frame
            var offset = 0;
            for (int y = 0; y < srcHeight; y++)
            {
                for (int x = 0; x < srcWidth; x++)
                {
                    var colorPoint = depthDataMappedToColor[offset];
                    var cameraPoint = depthDataMappedToCamera[offset];
                    offset++;

                    var pixelX = (int)(colorPoint.X * FrameScale);
                    var pixelY = (int)(colorPoint.Y * FrameScale);

                    if (pixelX < 0 || pixelY < 0 || pixelX >= dstWidth || pixelY >= dstHeight)
                        continue;

                    var pixelZ = ScaleDepth(cameraPoint.Z);
                    *(dstPtr + pixelY * dstWidth + pixelX) = (byte)pixelZ;
                }
            }
        }

        /// <summary>
        /// This function converts a depth value (in meters) to a pixel value
        /// between 0 and 255 based on the MinDepth and MaxDepth scaling
        /// parameters
        /// </summary>
        /// <param name="depth">Depth in meters</param>
        /// <returns>The pixel value</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private float ScaleDepth(float depth)
        {
            depth -= MinDepth;
            depth = Math.Max(depth, 0);

            depth = (depth / (MaxDepth - MinDepth)) * 255;
            depth = depth > 255 ? 0 : depth;

            return depth;
        }
    }
}
