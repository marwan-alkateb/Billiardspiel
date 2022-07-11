using Tracking.Model;
using UnityEngine;

namespace Tracking.Intern.Sender
{
    /// <summary>
    /// This class contains the event functions for tracking wall collisions.
    /// </summary>

    public class DirectTrackWall : MonoBehaviour
    {
        /// <summary>
        /// Describes the ball type of the wall
        /// </summary>
        public WallType wallType;

        /// <summary>
        /// This function adds a wall collision event to the <see cref="TrackingSender"/> by calling
        /// <see cref="TrackingSender.RecordWallCollision"/> when a ball and a wall collide.
        /// </summary>
        private void OnCollisionEnter(Collision collision)
        {
            var otherBall = collision.gameObject.GetComponent<DirectTrackBall>();
            if (otherBall != null)
                TrackingSender.Singleton.RecordWallCollision(wallType, otherBall.ballType);
        }
    }
}