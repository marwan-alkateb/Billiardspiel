using System.Collections.Generic;
using Display.Logic;
using UnityEngine;

namespace Display.UI.StaticUI
{
    /// <summary>
    /// This class manages the <see cref="StaticUIComponent"/>s.
    /// </summary>
    public class StaticUIController : MonoBehaviour
    {
        public LogicController logicController;
        
        /// <summary>
        /// A dictionary of all the <see cref="StaticUIComponent"/>s in the display component.
        /// </summary>
        private Dictionary<string, StaticUIComponent> _staticUiComponents;
        
        /// <summary>
        /// The currently active <see cref="StaticUIComponent"/>.
        /// </summary>
        private StaticUIComponent _activeComponent;

        private Stack<string> _switchHistory;

        /// <summary>
        /// This function iterates through all the child's of its transform and adds
        /// the <see cref="StaticUIComponent"/>s it finds to <see cref="_staticUiComponents"/>.
        /// If the there are multiple with the same name only the first one will be added.
        /// Additionally it initializes the <see cref="StaticUIComponent"/>s.
        /// </summary>
        private void Start()
        {
            _staticUiComponents = new Dictionary<string, StaticUIComponent>();
            _switchHistory = new Stack<string>();
        
            foreach (Transform child in transform)
            {
                StaticUIComponent staticUIComponent = child.GetComponent<StaticUIComponent>();
                if (!_staticUiComponents.ContainsKey(staticUIComponent.name))
                {
                    staticUIComponent.InitComponent(this);
                    _staticUiComponents.Add(staticUIComponent.name, staticUIComponent);

                    if (staticUIComponent.gameObject.activeInHierarchy && _activeComponent == null)
                    {
                        _activeComponent = staticUIComponent;
                        staticUIComponent.OnActivate();                        
                    }

                }
                else
                {
                    Debug.LogError("Multiple StaticUIComponents with the same name!", this);
                }
            }
        }

        /// <summary>
        /// Activates a <see cref="StaticUIComponent"/> based on a name.
        /// </summary>
        /// <param name="staticUIName">The name of the <see cref="StaticUIComponent"/>.</param>
        private void ActivateUI(string staticUIName)
        {
            _staticUiComponents[staticUIName].gameObject.SetActive(true);
            _staticUiComponents[staticUIName].OnActivate();                        
        }
    
        /// <summary>
        /// Deactivates the currently active <see cref="StaticUIComponent"/>.
        /// </summary>
        private void DeactivateUI()
        {
            if (_activeComponent == null) return;
            _staticUiComponents[_activeComponent.name].gameObject.SetActive(false);
            _staticUiComponents[_activeComponent.name].OnDeactivate();
        }
    
        /// <summary>
        /// Switches the active <see cref="StaticUIComponent"/> to the one that's passed as the parameter.
        /// </summary>
        /// <param name="staticUIName">The name of the <see cref="StaticUIComponent"/> to switch to.</param>
        public void SwitchActiveUI(string staticUIName)
        {
            if (_staticUiComponents.ContainsKey(staticUIName))
            {
                if(_activeComponent != null)
                    _switchHistory.Push(_activeComponent.name);

                DeactivateUI();
                ActivateUI(staticUIName);
                _activeComponent = _staticUiComponents[staticUIName];
            }
            else
            {
                Debug.LogError("Could not find a StaticUIComponent with the name: " + staticUIName, this);
            }
        }

        /// <summary>
        /// This function switches the active StaticUI to the previous one if existent.
        /// </summary>
        public void SwitchBack()
        {
            if (_switchHistory.Count > 0)
            {
                DeactivateUI();
                _activeComponent = _staticUiComponents[_switchHistory.Pop()];
                ActivateUI(_activeComponent.name);
            }
        }
    }
}
