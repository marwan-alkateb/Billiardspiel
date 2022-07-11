using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using Display.UI;
using Display.UI.DynamicUI;
using Display.UI.StaticUI;
using Tracking;
using Tracking.Intern.Receiver;
using Tracking.Model;
using UnityEngine;

namespace Display.Logic
{
    /// <summary>
    /// The LogicController manages the <see cref="LogicComponent"/>s.
    /// </summary>
    public class LogicController : TrackingBehaviour
    {
        /// <summary>
        /// The <see cref="StaticUIController"/> of the display component.
        /// </summary>
        public StaticUIController staticUIController;
        
        /// <summary>
        /// The <see cref="DynamicUIController"/> of the display component.
        /// </summary>
        public DynamicUIController dynamicUIController;
        
        /// <summary>
        /// The <see cref="LogicComponent"/>s to be managed by the LogicController.
        /// </summary>
        private Dictionary<string, LogicComponent> _logicComponents;
        
        /// <summary>
        /// The currently active <see cref="LogicComponent"/>s.
        /// </summary>
        private Dictionary<string, LogicComponent> _activeComponents;

        /// <summary>
        /// This function iterates through all the child's of its transform and adds
        /// the <see cref="LogicComponent"/>s it finds to <see cref="_logicComponents"/>.
        /// If the there are multiple with the same name only the first one will be added.
        /// Additionally it initializes the <see cref="LogicComponent"/>s and activates them
        /// if they are active in the scene hierarchy.
        /// </summary>
        private new void Start()
        {
            base.Start();
            _logicComponents = new Dictionary<string, LogicComponent>();
            _activeComponents = new Dictionary<string, LogicComponent>();

            foreach (Transform child in transform)
            {
                LogicComponent logicComponent = child.GetComponent<LogicComponent>();

                if (!_logicComponents.ContainsKey(logicComponent.name))
                {
                    logicComponent.InitComponent(this, dynamicUIController);
                    _logicComponents.Add(logicComponent.name, logicComponent);
                    
                    if(logicComponent.transform.gameObject.activeInHierarchy)
                    {
                        _activeComponents.Add(logicComponent.name, logicComponent);
                        logicComponent.OnActivate();
                    }
                    else
                    {
                        dynamicUIController.DeactivateByLogicComponent(logicComponent.name);
                    }
                }
                else
                {
                    Debug.LogError("Multiple Logic Components with the same name!", this);
                }
            }
        }

        /// <summary>
        /// Transmits the tracking data to all active <see cref="LogicComponent"/>s. 
        /// </summary>
        /// <param name="balls">An array of balls that were received.</param>
        /// <param name="ballCollisions">An array of ball collisions that were received.</param>
        /// <param name="pockets">An array of pocket events that were received.</param>
        /// <param name="wallCollisions">An array of wall collisions that were received.</param>
        protected override void TrackingUpdate(Tracking.Model.Ball[] balls, BallCollisionEvent[] ballCollisions, PocketEvent[] pockets, WallCollisionEvent[] wallCollisions)
        {
            Dictionary<BallType, Vector2> denormalizedBalls = balls.ToDictionary(ball => ball.Type, ball => UICanvas.NormalizedCoordsToCanvas(new Vector2(ball.Position.X, ball.Position.Y)));

            foreach (KeyValuePair<string, LogicComponent> component in _activeComponents)
            {
                component.Value.LogicUpdate(balls, denormalizedBalls, ballCollisions, pockets, wallCollisions);
            }
        }

        /// <summary>
        /// Activates a <see cref="LogicComponent"/> based on the given name.
        /// </summary>
        /// <param name="logicName">The name of the <see cref="LogicComponent"/> to be activated.</param>
        public void ActivateLogic(string logicName)
        {
            if (!_logicComponents.ContainsKey(logicName)) return;
            StartCoroutine(Waiter(logicName));

        }

        /// <summary>
        /// Wait few seconds after resetting balls to prevent foul in 9Ball.
        /// </summary>
        /// <param name="logicName">The name of the <see cref="LogicComponent"/> to be activated.</param>
        IEnumerator Waiter(string logicName)
        {
            GameObject.Find("Balls").GetComponent<ResetBalls>().Reset(logicName);

            yield return new WaitForSeconds(0.250f);

            _activeComponents.Add(logicName, _logicComponents[logicName]);
            _logicComponents[logicName].transform.gameObject.SetActive(true);
            dynamicUIController.ActivateByLogicComponent(logicName);
            _logicComponents[logicName].OnActivate();
        }
        
        /// <summary>
        /// Deactivates a <see cref="LogicComponent"/> based on the given name.
        /// </summary>
        /// <param name="logicName">The name of the <see cref="LogicComponent"/> to be deactivated.</param>
        public void DeactivateLogic(string logicName)
        {
            if (!_logicComponents.ContainsKey(logicName)) return;
            _activeComponents.Remove(logicName);
            _logicComponents[logicName].transform.gameObject.SetActive(false);
            dynamicUIController.DeactivateByLogicComponent(logicName);
            _logicComponents[logicName].OnDeactivate();

            GameObject.Find("Balls").GetComponent<ResetBalls>().Reset(logicName);

        }
    }
}
