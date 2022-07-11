using Tracking.API;
using Tracking.Model;
using UnityEngine;

namespace Tracking.Intern.Receiver
{
    /// <summary>
    /// Classes derived from this interface will get updates from the <see cref="TrackingAPI"/>
    /// </summary>
    public abstract class TrackingBehaviour : MonoBehaviour
    {
        /// <summary>
        /// <see cref="Initialized"/> represents the status of the TrackingBehaviour.
        /// </summary>
        private bool Initialized { get; set; }

        /// <summary>
        /// This function checks if <see cref="TrackingReceiver"/> has an instance and sets <see cref="OnUpdate"/> as
        /// eventhandler for the <see cref="TrackingReceiver"/>.
        /// </summary>
        protected void Start()
        {
            if (TrackingReceiver.Singleton == null) return;

            TrackingReceiver.Singleton.OnUpdate += OnUpdate;
            Initialized = true;
        }
        
        /// <summary>
        /// This function checks if <see cref="TrackingReceiver"/> is initialized and has an instance the sets
        /// <see cref="OnUpdate"/> as event handler for the <see cref="TrackingReceiver"/>.
        /// </summary>
        private void Update()
        {
            if (Initialized || TrackingReceiver.Singleton == null)
                return;

            TrackingReceiver.Singleton.OnUpdate += OnUpdate;
            Initialized = true;
        }
        
        /// <summary>
        /// This function is used as event handler and sets all params for the abstract <see cref="TrackingUpdate"/>
        /// method.
        /// </summary>
        private void OnUpdate(BallCollisionEvent[] ballCollisions, PocketEvent[] pockets, Ball[] balls, WallCollisionEvent[] wallCollisions)
        {
            TrackingUpdate(balls, ballCollisions, pockets, wallCollisions);
        }

        /// <summary>
        ///     This function is called once for every frame that was tracked.
        /// </summary>
        /// <param name="balls">
        ///     A array of <see cref="BallType" /> that indicates which balls where tracked in this
        ///     frame."/>
        /// </param>
        /// <param name="ballCollisions">
        ///     An Array of <see cref="BallCollisionEvent" /> that contains every collision that happened in this
        ///     frame."/>
        /// </param>
        /// <param name="pockets">
        ///     An Array of <see cref="PocketEvent" /> that contains every pocketing that happened in this frame
        /// </param>
        /// <param name="wallCollisions">
        ///     An Array of <see cref="WallCollisionEvent" /> that contains every pocketing that happened in this frame
        /// </param>
        protected abstract void TrackingUpdate(Ball[] balls, BallCollisionEvent[] ballCollisions,
            PocketEvent[] pockets, WallCollisionEvent[] wallCollisions);
    }
}