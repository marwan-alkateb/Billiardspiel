using Emgu.CV;

namespace Tracking.Core.Processing
{
    internal class CalibrationResult
    {
        public Mat PlayfieldTransform { get; set; } = null;

        public Mat DepthBaseframe { get; set; } = null;

        public bool HasData => PlayfieldTransform != null && DepthBaseframe != null;
    }
}
