using UnityEngine;

namespace Tracking.Intern.Sender
{
    /// <summary>
    /// This class contains the event functions for the table field
    /// </summary>
    /// <remarks>
    /// Add this script to the appropriate table field trigger
    /// </remarks>
    public class DirectTrackTableField : MonoBehaviour
    {
        /// <summary>
        /// Sets the member variable onTable of the ball to true if it hits the table 
        /// </summary>
        private void OnTriggerEnter(Collider trigger)
        {
            var ball = trigger.gameObject.GetComponent<DirectTrackBall>();

            if (ball != null)
                ball.onTable = true;
        }

        /// <summary>
        /// Sets the member variable onTable of the ball to true if it stays on the table field
        /// </summary>
        private void OnTriggerStay(Collider trigger)
        {
            var ball = trigger.gameObject.GetComponent<DirectTrackBall>();

            if (ball != null)
                ball.onTable = true;
        }

        /// <summary>
        /// Sets the member variable onTable of the ball to false if it leaves the table field
        /// </summary>
        private void OnTriggerExit(Collider trigger)
        {
            var ball = trigger.gameObject.GetComponent<DirectTrackBall>();

            if (ball != null)
                ball.onTable = false;
        }
    }
}