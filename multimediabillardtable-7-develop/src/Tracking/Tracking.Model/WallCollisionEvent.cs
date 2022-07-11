using System;

namespace Tracking.Model
{
    /// <summary>
    /// Describes a collision between a ball and a wall
    /// </summary>
    public struct WallCollisionEvent
    {
        public BallType Ball { get; set; }
        
        public WallType Wall { get; set; }
    }
}