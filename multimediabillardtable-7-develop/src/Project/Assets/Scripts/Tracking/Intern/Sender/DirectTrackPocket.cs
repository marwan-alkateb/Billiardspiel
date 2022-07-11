using Tracking.Model;
using UnityEngine;

namespace Tracking.Intern.Sender
{
    /// <summary>
    /// This class contains the event functions for tracking pocket events.
    /// </summary>
    /// <remarks>
    /// Add this script to all the pocket triggers in unity.
    /// </remarks>
    public class DirectTrackPocket : MonoBehaviour
    {
        /// <summary>
        /// Describes the PocketType of the pocket
        /// </summary>
        public PocketType pocketType;

        /// <summary>
        /// This function adds a pocket event to the <see cref="TrackingSender"/> by calling
        /// <see cref="TrackingSender.RecordPocketing"/> when a ball gets pocketed.
        /// </summary>
        private void OnTriggerEnter(Collider other)
        {
            var otherBall = other.gameObject.GetComponent<DirectTrackBall>();

            if (otherBall != null)
                TrackingSender.Singleton.RecordPocketing(pocketType, otherBall.ballType);
        }
    }
}