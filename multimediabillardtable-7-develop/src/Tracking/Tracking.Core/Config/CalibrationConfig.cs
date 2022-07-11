namespace Tracking.Core.Config
{
    internal class CalibrationConfig
    {
        /// <summary>
        /// For how many frames the calibration process should run.
        /// The higher this number, the higher the calibration quality since more
        /// results from more frames are averaged together
        /// </summary>
        public int NumFrames { get; set; }

        /// <summary>
        /// Positions on the table edges in range [0;1] from where to start sampling
        /// the depth buffer to find the playfield
        /// </summary>
        public float[] DepthSampleDistances { get; set; }

        /// <summary>
        /// How much the depth value has to change from one sample point to another,
        /// for it to be counted as the playfield edge. This value is in range [0;255], not meters
        /// </summary>
        public float DepthSampleThreshold { get; set; }

        /// <summary>
        /// How large the found playfield rectangle has to be for it to be counted
        /// as the playfield rectangle. Value is in square pixels.
        /// </summary>
        public float MinPlayfieldArea { get; set; }
    }
}
