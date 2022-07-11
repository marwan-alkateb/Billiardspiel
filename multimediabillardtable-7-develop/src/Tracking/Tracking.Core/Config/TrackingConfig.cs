namespace Tracking.Core.Config
{
    internal class TrackingConfig
    {
        /// <summary>
        /// Scale of the per-frame heightmap in relation to the
        /// size of the frame, in range [0;1]
        /// </summary>
        public float HeightmapScale { get; set; }

        /// <summary>
        /// How much influence the heightmap correction system should
        /// have on the final frame, in range [0;1]
        /// </summary>
        public float HeightmapIntensity { get; set; }

        /// <summary>
        /// Kernel size of the Median blur used for generating the heightmap
        /// </summary>
        public int HeightmapMedianSize { get; set; }

        /// <summary>
        /// Kernel size of the Gaussian blur used for the high pass filter
        /// </summary>
        public int HighPassGaussSize { get; set; }

        /// <summary>
        /// Kernel Size of the Bilateral filter used for denoising.
        /// Higher values result in a cleaner depth image, but for real-time
        /// performance, it should be kept below 5
        /// </summary>
        public int BilateralSize { get; set; }

        /// <summary>
        /// Which depth value above the table (in range [0;255]) should count as a ball.
        /// </summary>
        public int BallDepthThreshold { get; set; }

        /// <summary>
        /// How much of a circle a detected blob has to be for it to be counted
        /// as a ball, in range [0;1]
        /// </summary>
        public float BallCircularityThreshold { get; set; }

        /// <summary>
        /// How much area a detected blob must have for it to be counted as a ball,
        /// in square pixels.
        /// </summary>
        public float BallAreaThreshold { get; set; }

        /// <summary>
        /// As we cannot and should not infer ball size from the depth frame, since they
        /// are all the same size, we define a static radius for each detected ball here.
        /// </summary>
        public float BallRadius { get; set; }

        /// <summary>
        /// The angle between color extraction rays, in radians
        /// </summary>
        public float ExtractionAngle { get; set; }

        /// <summary>
        /// The maximum length for color extraction rays, in pixels
        /// </summary>
        public int ExtractionRadius { get; set; }

        /// <summary>
        /// If the difference between the average and current ray hues exceeds this
        /// threshold, the ray stops. This is to determine ball borders.
        /// </summary>
        public float ExtractionHueThreshold { get; set; }

        /// <summary>
        /// Same as <see cref="ExtractionHueThreshold"/>, but for the Saturation component of the HSV color
        /// </summary>
        public float ExtractionSaturationThreshold { get; set; }

        /// <summary>
        /// Same as <see cref="ExtractionHueThreshold"/>, but for the Value component of the HSV color
        /// </summary>
        public float ExtractionValueThreshold { get; set; }
    }
}
