using UnityEngine;

namespace Display.UI.StaticUI
{
    /// <summary>
    /// A abstract class that needs to be implemented for each StaticUI.
    /// </summary>
    public abstract class StaticUIComponent : MonoBehaviour
    {
        /// <summary>
        /// Holds the <see cref="StaticUIController"/>.
        /// </summary>
        protected StaticUIController StaticUIController;

        /// <summary>
        /// Initializes the <see cref="StaticUIComponent"/>.
        /// </summary>
        /// <param name="staticUIController">The <see cref="StaticUIController"/> of the display.</param>
        public void InitComponent(StaticUIController staticUIController)
        {
            StaticUIController = staticUIController;
            Init();
        }

        /// <summary>
        /// A abstract function with the purpose of initializing the component.
        /// </summary>
        protected virtual void Init() {}
        
        /// <summary>
        /// This function gets called by the <see cref="StaticUIController"/> when the component is activated.
        /// </summary>
        public virtual void OnActivate() {}

        /// <summary>
        /// This function gets called by the <see cref="StaticUIController"/> when the component is deactivated.
        /// </summary>
        public virtual void OnDeactivate() {}
    }
}
