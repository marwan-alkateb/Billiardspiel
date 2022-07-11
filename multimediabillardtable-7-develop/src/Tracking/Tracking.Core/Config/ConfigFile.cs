namespace Tracking.Core.Config
{
    internal class ConfigFile
    {
        public CalibrationConfig Calibration { get; set; }

        public TrackingConfig Tracking { get; set; }

        public ClassifierConfig Classifier { get; set; }
    }
}
