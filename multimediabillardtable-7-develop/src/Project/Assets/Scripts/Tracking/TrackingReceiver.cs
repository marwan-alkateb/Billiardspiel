using Assets.Scripts.Tracking;
using System;
using System.Linq;
using Tracking.API;
using Tracking.Model;
using UnityEngine;

namespace Tracking
{
    
    /// <summary>
    /// This class receives a frame and triggers the <see cref="OnUpdate"/> event
    /// </summary>
    public class TrackingReceiver : MonoBehaviour
    {
        /// <summary>
        /// <see cref="Activated"/> represents the status of the Sender
        /// </summary>
        private bool Activated { get;  set; }
        
        /// <summary>
        /// <see cref="Singleton"/> sets that there always can be only one class instance
        /// </summary>
        public static TrackingReceiver Singleton { get; private set; }
        
        /// <summary>
        /// <see cref="OnUpdate"/> this event contains a list of actions
        /// </summary>
        internal event Action<BallCollisionEvent[], PocketEvent[], Ball[], WallCollisionEvent[]> OnUpdate;

        
        /// <summary>
        /// If the receiver is not activated, this function starts the receiving process and changes the
        /// <see cref="Activated"/> attribute to true.
        /// </summary>
        public void StartReceive()
        {
            if(Activated)
                return;
            
            Debug.Log("Started Receiving...");
            Activated = true;
        }
        
        /// <summary>
        /// If the receiver is activated, this function stops the receiving process and changes the
        /// <see cref="Activated"/> attribute to false.
        /// </summary>
        public void EndReceive()
        {
            if(!Activated)
                return;
            
            Debug.Log("Stopped Receiving...");
            Activated = false;
        }

        private void OnApplicationQuit()
        {
            TrackingInterface.Instance.Shutdown();
        }

        /// <summary>
        /// Checks if the TrackingReceiver is only initialized once and if so starts the receiving process.
        /// </summary>
        private void Start()
        {
            if (Singleton != null)
            {
                Debug.Log($"More than 1 TrackingReceiver script is active in this scene!");
            }
            Singleton = this;
            
            StartReceive();
        }
        
        /// <summary>
        /// If the receiver is activated, this function receives a frame from the API and then triggers the
        /// <see cref="OnUpdate"/> event which invokes all actionv from the frame.
        /// </summary>
        private void Update()
        {
            if (!Activated)
                return;

            Frame frame = TrackingInterface.Instance.LatestFrame;

            if (frame != null)
                OnUpdate?.Invoke(frame.BallCollisions.ToArray(), frame.PocketEvents.ToArray(), frame.Balls.ToArray(), frame.WallCollisions.ToArray());
        }
    }
}