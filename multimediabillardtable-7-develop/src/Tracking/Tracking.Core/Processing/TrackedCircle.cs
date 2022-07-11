using Emgu.CV.Structure;
using System.Drawing;
using Tracking.Core.Maths;

namespace Tracking.Core.Processing
{
    /// <summary>
    /// Describes the internal state of a single tracked circle,
    /// as used by the Tracking Algorithm.
    /// </summary>
    internal class TrackedCircle
    {
        /// <summary>
        /// Unique ID for identifying the circle
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Whether the circle can be seen in the current frame
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// For how many frames the circle is known
        /// </summary>
        public int ActiveFrames { get; set; }

        /// <summary>
        /// Center point of the circle in the current frame
        /// </summary>
        public PointF CurrentCenter { get; set; }

        /// <summary>
        /// Center point of the circle in the previous frame
        /// </summary>
        public PointF PreviousCenter { get; set; }

        /// <summary>
        /// Average color of the circle in the current frame
        /// </summary>
        public Bgra CurrentColor { get; set; }

        /// <summary>
        /// Rolling average buffer for keeping track of how
        /// many pixels within the circle are non-white pixels.
        /// This is useful later on to discern half from full balls.
        /// </summary>
        public RingBuffer<float> ColorPercentageBuffer { get; } = new RingBuffer<float>(30);

        /// <summary>
        /// Rolling average buffer for colors
        /// </summary>
        public RingBuffer<Bgra> ColorBuffer { get; } = new RingBuffer<Bgra>(30);

        /// <summary>
        /// Rolling average buffer for positions
        /// </summary>
        public RingBuffer<PointF> PositionBuffer { get; } = new RingBuffer<PointF>(3);

        /// <summary>
        /// Rolling average buffer for velocities
        /// </summary>
        public RingBuffer<PointF> VelocityBuffer { get; } = new RingBuffer<PointF>(4);

        /// <summary>
        /// Converts this circle to a Emgu CircleF of given radius
        /// </summary>
        /// <param name="radius">Circle radius</param>
        /// <returns>Emgu CircleF instance</returns>
        public CircleF ToCircleF(float radius) => new CircleF(PositionBuffer.Average(), radius);

        internal bool used = false;
    }
}
