using System.Collections.Generic;

namespace Tracking.Model
{
    /// <summary>
    /// Holds the tracking result for a single frame
    /// </summary>
    public class Frame
    {
        public IList<Ball> Balls { get; set; }

        public IList<BallCollisionEvent> BallCollisions { get; set; }

        public IList<WallCollisionEvent> WallCollisions { get; set; }

        public IList<PocketEvent> PocketEvents { get; set; }

        public CueStick CueStick { get; set; }
    }
}