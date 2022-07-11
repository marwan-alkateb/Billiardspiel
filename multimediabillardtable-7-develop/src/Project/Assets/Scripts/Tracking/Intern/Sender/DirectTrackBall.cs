using Tracking.Model;
using UnityEngine;

namespace Tracking.Intern.Sender
{
    /// <summary>
    /// This class contains the event functions for tracking ball collisions.
    /// </summary>
    /// <remarks>
    /// Add this script to all the balls in the unity scene.
    /// </remarks>
    public class DirectTrackBall : MonoBehaviour
    {
        /// <summary>
        /// Describes the ball#s color
        /// </summary>
        public BallColor ballColor;

        /// <summary>
        /// Describes whether the ball is full or half
        /// </summary>
        public BallTeam ballTeam;

        /// <summary>
        /// Describes the full type of this ball
        /// </summary>
        public BallType ballType => new BallType(ballColor, ballTeam);

        /// <summary>
        /// Describes if the ball is currently on the table
        /// </summary>
        public bool onTable;

        /// <summary>
        /// This function adds a ball collision event to the <see cref="TrackingSender"/> by calling
        /// <see cref="TrackingSender.RecordCollision"/> when two balls collide.
        /// </summary>
        private void OnCollisionEnter(Collision collision)
        {
            var otherBall = collision.gameObject.GetComponent<DirectTrackBall>();

            if (otherBall != null)
                TrackingSender.Singleton.RecordCollision(ballType, otherBall.ballType);
        }
    }
}