using Assets.Scripts.Tracking;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Tracking.API;
using Tracking.Intern.Sender;
using Tracking.Model;
using UnityEngine;
using UnityEngine.UI;

namespace Tracking
{
    /// <summary>
    /// With this enum you can set the tracking mode.
    /// </summary>
    public enum TrackingSenderMode
    {
        None,
        DirectTracking,
        CameraTracking
    }

    /// <summary>
    /// This class is used to collect all changes on the billiard table and depending on the tracking mode updating the
    /// <see cref="_currentFrame"/> attribute to contain these information's.
    /// </summary>
    public class TrackingSender : MonoBehaviour
    {
        /// <summary>
        /// <see cref="_ballCollisionEvents"/> contains a list of ball collision events
        /// </summary>
        private readonly List<BallCollisionEvent> _ballCollisionEvents = new List<BallCollisionEvent>(16);

        /// <summary>
        /// <see cref="_pocketEvents"/> contains a list of pocketing events
        /// </summary>
        private readonly List<PocketEvent> _pocketEvents = new List<PocketEvent>(16);

        /// <summary>
        /// <see cref="_wallCollisionEvents"/> contains a list of wall collision events
        /// </summary>
        private readonly List<WallCollisionEvent> _wallCollisionEvents = new List<WallCollisionEvent>(16);

        /// <summary>
        /// <see cref="_currentFrame"/> contains the most recent frame
        /// </summary>
        private Frame _currentFrame;

        /// <summary>
        /// <see cref="_rt"/> serves as storage for the depth-texture
        /// </summary>
        private RenderTexture _rt;

        /// <summary>
        /// <see cref="_tex"/> serves as storage during processing of the rendered depth-texture
        /// </summary>
        private Texture2D _tex;

        /// <summary>
        /// <see cref="_rect"/> serves as storage during processing of the rendered depth-texture
        /// </summary>
        private Rect _rect;

        /// <summary>
        /// <see cref="_depthByteData"/> contains the information of a rendered depth-texture and is used for creating a Tracking-Frame
        /// </summary>
        private byte[] _depthByteData;

        /// <summary>
        /// <see cref="directTrackGameElementsSettings"/> contains the most recent Frame
        /// </summary>
        public DirectTrackGameElements directTrackGameElementsSettings = new DirectTrackGameElements();

        /// <summary>
        /// <see cref="CameraTrackingSettings"/> contains the most recent Frame for the CameraTracking
        /// </summary>
        public CameraTracking CameraTrackingSettings = new CameraTracking();

        /// <summary>
        /// <see cref="mode"/> represents the to be tracked objects on the billiard table
        /// </summary>
        [Space(15)]
        public TrackingSenderMode mode = TrackingSenderMode.None;

        /// <summary>
        /// <see cref="Singleton"/> sets that there always can be only one class instance 
        /// </summary>
        public static TrackingSender Singleton { get; private set; }

        /// <summary>
        /// <see cref="Activated"/> represents the status of the Sender
        /// </summary>
        public bool Activated { get; private set; }


        /// <summary>
        /// Starts the starting routine by changing <see cref="Activated"/> to true (if it is not active) and depending
        /// on which mode is set initializes the corresponding elements.
        /// </summary>
        public void StartSend()
        {
            if (Activated)
                return;

            _currentFrame = new Frame();

            // depth camera settings are set for both modes for debugging purposes
            CameraTrackingSettings.Intern = new CameraTrackingIntern();
            CameraTrackingSettings.Camera.depthTextureMode = DepthTextureMode.Depth;
            CameraTrackingSettings.Camera.targetTexture = CameraTrackingSettings.ColorTexture;
            CameraTrackingIntern.Tracking = CameraTrackingSettings;

            switch (mode)
            {
                case TrackingSenderMode.None: return;
                case TrackingSenderMode.DirectTracking:
                    {
                        directTrackGameElementsSettings.tableField.isTrigger = true;
                        directTrackGameElementsSettings.upperLeft.isTrigger = true;
                        directTrackGameElementsSettings.upperMid.isTrigger = true;
                        directTrackGameElementsSettings.upperRight.isTrigger = true;
                        directTrackGameElementsSettings.bottomLeft.isTrigger = true;
                        directTrackGameElementsSettings.bottomMid.isTrigger = true;
                        directTrackGameElementsSettings.bottomRight.isTrigger = true;
                    }
                    break;
                case TrackingSenderMode.CameraTracking:
                    {
                        TrackingInterface.Instance.Connect();
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            Debug.Log("Started Sending...");
            Activated = true;
        }

        /// <summary>
        /// If the sender is activated, this function stops the sending process and changes the <see cref="Activated"/>
        /// attribute to false.
        /// </summary>
        public void StopSend()
        {
            if (!Activated)
                return;

            Debug.Log("Stopped Sending...");
            Activated = false;
        }

        /// <summary>
        /// Initalizes instance of TrackingSender
        /// </summary>
        private void Awake()
        {
        }

        /// <summary>
        /// Checks if the TrackingSender is only initialized once and if so starts the sending process by calling the
        /// <see cref="StartSend"/> method.
        /// </summary>
        private void Start()
        {
            if (Singleton != null)
            {
                Debug.Log($"More than 1 TrackingSender script is active in this scene!");
            }

            StartSend();
            Singleton = this;
        }

        /// <summary>
        /// If the sender is activated, this function updates the the current frame receives or it from the TrackingApi
        /// depending on changes made in the scene and depending in which tracking mode is used.
        /// </summary>
        private void Update()
        {
            if (!Activated)
                return;

            switch (mode)
            {
                case TrackingSenderMode.DirectTracking:
                    _currentFrame.BallCollisions = _ballCollisionEvents.Distinct().ToArray();
                    _currentFrame.PocketEvents = _pocketEvents.ToArray();
                    _currentFrame.Balls = new Ball[16];
                    _currentFrame.WallCollisions = _wallCollisionEvents.ToArray(); // TODO placeholder for future wall collision events

                    for (int i = 0; i < directTrackGameElementsSettings.balls.Length; ++i)
                    {
                        GameObject ballGameObject = directTrackGameElementsSettings.balls[i];

                        Ball ball = new Ball();

                        DirectTrackBall trackBall = ballGameObject.gameObject.GetComponent<DirectTrackBall>();

                        ball.Type = trackBall.ballType;
                        ball.OnTable = trackBall.onTable;

                        Vector3 ballPosition = ballGameObject.gameObject.GetComponent<Rigidbody>().position;

                        ball.Position = new PointF { X = (ballPosition.x + 1.3f) / 2.6f, Y = (ballPosition.z + 0.665f) / 1.33f, };

                        _currentFrame.Balls[i] = ball;
                    }

                    TrackingInterface.Instance.LatestFrame = _currentFrame;

                    break;

                case TrackingSenderMode.CameraTracking:
                    {
                        _currentFrame = TrackingInterface.Instance.LatestFrame;
                    }
                    break;
            }

            _currentFrame = new Frame();

            if (mode == TrackingSenderMode.DirectTracking)
            {
                _ballCollisionEvents.Clear();
                _pocketEvents.Clear();
                _wallCollisionEvents.Clear();
            }
        }

        /// <summary>
        /// If the sender is activated, this function adds a ball collision event to <see cref="_ballCollisionEvents"/>.
        /// </summary>
        /// <param name="ballA"> represents a ball of the billiard game</param>
        /// <param name="ballB"> represents a ball of the billiard game</param>
        internal void RecordCollision(BallType ballA, BallType ballB)
        {
            if (!Activated)
                return;

            _ballCollisionEvents.Add(new BallCollisionEvent
            {
                BallA = ballA,
                BallB = ballB
            });
        }

        /// <summary>
        /// If the sender is activated, this function adds a pocketing event to <see cref="_pocketEvents"/>.
        /// </summary>
        /// <param name="pocket"> represents a pocket of the billiard table</param>
        /// <param name="ball"> represents a ball of the billiard game</param>
        internal void RecordPocketing(PocketType pocket, BallType ball)
        {
            if (!Activated)
                return;

            _pocketEvents.Add(new PocketEvent
            {
                Ball = ball,
                Pocket = pocket
            });
        }

        /// <summary>
        /// If the sender is activated, this function adds a wall collision event to <see cref="_wallCollisionEvents"/>.
        /// </summary>
        /// <param name="wall"> represents a wall of the billiard table</param>
        /// <param name="ball"> represents a ball of the billiard game</param>
        internal void RecordWallCollision(WallType wall, BallType ball)
        {
            if (!Activated)
                return;

            _wallCollisionEvents.Add(new WallCollisionEvent
            {
                Ball = ball,
                Wall = wall
            });
        }
    }
}