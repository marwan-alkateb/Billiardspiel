namespace Tracking.Model
{
    /// <summary>
    /// Describes where a ball was pocketed on the billard table.
    /// </summary>
    public struct PocketEvent
    {
        /// <summary>
        /// The pocketed ball
        /// </summary>
        public BallType Ball { get; set; }
        
        /// <summary>
        /// The pocket the ball fell into
        /// </summary>
        public PocketType Pocket { get; set; }
    }
}