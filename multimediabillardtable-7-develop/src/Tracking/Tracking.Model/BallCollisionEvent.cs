using System;

namespace Tracking.Model
{
    /// <summary>
    /// Describes a collision between two balls
    /// </summary>
    public struct BallCollisionEvent : IEquatable<BallCollisionEvent>
    {
        /// <summary>
        /// The first ball involved in the collision
        /// </summary>
        public BallType BallA { get; set; }

        /// <summary>
        /// The second ball involved in the collision
        /// </summary>
        public BallType BallB { get; set; }

        /// <summary>
        /// Checks whether all of the balls were part of this collision
        /// </summary>
        /// <param name="balls">The balls to check</param>
        /// <returns>Whether they were part of the collision</returns>
        public bool Contains(params BallType[] balls)
        {
            if (balls.Length > 2) return false;
            
            foreach (var ball in balls)
                if (ball != BallA && ball != BallB)
                    return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            return obj is BallCollisionEvent @event && Equals(@event);
        }

        public bool Equals(BallCollisionEvent other)
        {
            return BallA == other.BallA &&
                   BallB == other.BallB;
        }

        public override int GetHashCode()
        {
            int hashCode = -1716890711;
            hashCode = hashCode * -1521134295 + BallA.GetHashCode();
            hashCode = hashCode * -1521134295 + BallB.GetHashCode();
            return hashCode;
        }
    }
}