using System.Collections.Generic;
using Display.UI.DynamicUI;
using Display.UI.StaticUI;
using Tracking;
using Tracking.Model;
using UnityEngine;

namespace Display.Logic
{
    /// <summary>
    /// A abstract class that needs to be implemented for each LogicComponent.
    /// </summary>
    public abstract class LogicComponent : MonoBehaviour
    {
        /// <summary>
        /// The <see cref="StaticUIComponent"/>s the logic has access to.
        /// </summary>
        /// <remarks>
        /// Add the <see cref="StaticUIComponent"/>s in the inspector.
        /// </remarks>
        public StaticUIComponent[] staticUIComponents;

        /// <summary>
        /// The <see cref="LogicController"/> of the display component.
        /// </summary>
        protected LogicController LogicController;

        /// <summary>
        /// The <see cref="DynamicUIController"/> of the display component.
        /// </summary>
        protected DynamicUIController DynamicUIController;

        /// <summary>
        /// Initializes the LogicComponent.
        /// </summary>
        /// <param name="logicController">The <see cref="LogicController"/> of the display.</param>
        /// <param name="dynamicUIController">The <see cref="DynamicUIController"/> of the display.</param>
        public void InitComponent(LogicController logicController, DynamicUIController dynamicUIController)
        {
            LogicController = logicController;
            DynamicUIController = dynamicUIController;
            Init();
        }

        /// <summary>
        /// A virtual function with the purpose of initializing the component.
        /// </summary>
        protected virtual void Init() { }

        /// <summary>
        /// This function gets called by the <see cref="LogicController"/> when the component is activated.
        /// </summary>
        public virtual void OnActivate() { }

        /// <summary>
        /// This function gets called by the <see cref="LogicController"/> when the component is deactivated.
        /// </summary>
        public virtual void OnDeactivate() { }

        /// <summary>
        /// A function that gets called everytime new tracking data is available.
        /// This function only gets called if the component is active.
        /// </summary>
        /// <param name="balls">An array of balls that were received.</param>
        /// <param name="denormalizedBallPositions">A dictionary with denormalized positions and their corresponding <see cref="BallType"/>.</param>
        /// <param name="ballCollisionEvents">An array of ball collisions that were received.</param>
        /// <param name="pocketEvents">An array of pocket events that were received.</param>
        /// <param name="wallCollisionEvents">An array of wall collisions that were received.</param>
        public abstract void LogicUpdate(
            Tracking.Model.Ball[] balls,
            Dictionary<BallType, Vector2> denormalizedBallPositions,
            BallCollisionEvent[] ballCollisionEvents,
            PocketEvent[] pocketEvents,
            WallCollisionEvent[] wallCollisionEvents);
    }
}
