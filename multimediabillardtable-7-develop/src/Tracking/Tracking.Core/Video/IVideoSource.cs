using Emgu.CV;
using Emgu.CV.Structure;

namespace Tracking.Core.Video
{
    /// <summary>
    /// IVideoSource is an abstraction layer to various Depth-enabled
    /// video devices such as a Microsoft Kinect camera.
    /// </summary>
    internal interface IVideoSource
    {
        /// <summary>
        /// The latest color image
        /// </summary>
        Image<Bgra, byte> ColorFrame { get; }

        /// <summary>
        /// The latest color image, unprocessed
        /// </summary>
        Image<Bgra, byte> ColorFrameOriginal { get; }

        /// <summary>
        /// Scale factor between <see cref="ColorFrame"/>/<see cref="DepthFrame"/>
        /// and <see cref="ColorFrameOriginal"/>
        /// </summary>
        double ColorFrameScale { get; }

        /// <summary>
        /// The latest depth image
        /// </summary>
        Image<Gray, byte> DepthFrame { get; }

        /// <summary>
        /// The callback for when new frames arrive
        /// </summary>
        IFrameHandler FrameHandler { get; set; }

        /// <summary>
        /// Initializes the video source and starts reading data
        /// </summary>
        void Open();

        /// <summary>
        /// Stops reading data and releases the resources allocated for the video source
        /// </summary>
        void Close();
    }
}
