using System.Drawing;

namespace Tracking.Model
{
    /// <summary>
    /// Describes the state of the cue stick
    /// </summary>
    public class CueStick
    {
        /// <summary>
        /// Source point of the line that describes the cue stick
        /// </summary>
        public PointF SrcPoint { get; set; }

        /// <summary>
        /// Point of the line that describes the cue stick where it 
        /// is closes t to the table
        /// </summary>
        public PointF TablePoint { get; set; }

        /// <summary>
        /// Whether the cue stick is in frame
        /// </summary>
        public bool IsVisible { get; set; }

        /// <summary>
        /// Whether the cue stick has touched the table at <see cref="TablePoint"/>
        /// </summary>
        public bool HasTouched { get; set; }
    }
}
