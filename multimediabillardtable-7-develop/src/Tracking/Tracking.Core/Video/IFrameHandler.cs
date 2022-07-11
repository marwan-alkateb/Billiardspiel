namespace Tracking.Core.Video
{
    internal interface IFrameHandler
    {
        /// <summary>
        /// Called when new color and depth data is available.
        /// </summary>
        /// <param name="frame">The frame</param>
        void HandleFrame(VideoFrame frame);
    }
}
