using System.Drawing;

namespace Tracking.Model
{
    /// <summary>
    /// Describes the state of a single billard ball
    /// </summary>
    public class Ball
    {
        /// <summary>
        /// The type of this ball
        /// </summary>
        public BallType Type { get; set; }

        /// <summary>
        /// The raw pixel-position of this ball
        /// </summary>
        public PointF RawPosition { get; set; }

        /// <summary>
        /// The normalized table-space position of this ball.
        /// X and Y are in range [0..1]
        /// </summary>
        public PointF Position { get; set; }

        /// <summary>
        /// The velocity vector of this ball, also in normalized
        /// table-space coordinates.
        /// </summary>
        public PointF Velocity { get; set; }

        /// <summary>
        /// Whether this ball is currently on the table.
        /// </summary>
        public bool OnTable { get; set; }
    }
}