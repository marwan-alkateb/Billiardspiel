using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracking.Core.Video
{
    /// <summary>
    /// Describes a multi-component video frame from the camera
    /// <para>
    /// The <see cref="Depth"/> and <see cref="Color"/> images are the same size
    /// and they are in the same coordinate space. The <see cref="ColorOriginal"/> image
    /// however is NOT in the same space.
    /// </para>
    /// </summary>
    public class VideoFrame
    {
        /// <summary>
        /// The depth component
        /// </summary>
        public Image<Gray, byte> Depth { get; set; }

        /// <summary>
        /// The color component
        /// </summary>
        public Image<Bgra, byte> Color { get; set; }

        /// <summary>
        /// The unprocessed color component
        /// </summary>
        public Image<Bgra, byte> ColorOriginal { get; set; }

        /// <summary>
        /// Time difference between when Depth and Color were read,
        /// in milliseconds
        /// </summary>
        public double Divergence { get; set; }

        public VideoFrame(Image<Gray, byte> depth, Image<Bgra, byte> color, Image<Bgra, byte> colorOriginal, double divergence)
        {
            Depth = depth;
            Color = color;
            ColorOriginal = colorOriginal;
            Divergence = divergence;
        }
    }
}
