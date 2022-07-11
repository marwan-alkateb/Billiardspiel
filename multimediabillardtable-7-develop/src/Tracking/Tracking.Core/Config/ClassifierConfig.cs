using System.Collections.Generic;
using Tracking.Model;

namespace Tracking.Core.Config
{
    internal class ColorRange
    {
        public int HueCenter { get; set; }

        public int HueRange { get; set; }

        public int? ValueCenter { get; set; }

        public int? ValueRange { get; set; }
    }

    internal class ClassifierConfig
    {
        /// <summary>
        /// A mapping between the logical Ball Color and the 
        /// HSV color range that corresponds to it
        /// </summary>
        public IDictionary<BallColor, ColorRange> ColorRanges { get; set; }
    }
}
