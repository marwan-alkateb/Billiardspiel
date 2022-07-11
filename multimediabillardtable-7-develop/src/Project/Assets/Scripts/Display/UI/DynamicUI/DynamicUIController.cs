using System.Collections.Generic;
using Display.Logic;
using UnityEngine;

namespace Display.UI.DynamicUI
{
    /// <summary>
    /// This class manages the <see cref="DynamicUIElement"/>s of the display component.
    /// </summary>
    public class DynamicUIController : MonoBehaviour
    {
        /// <summary>
        /// A dictionary that contains the <see cref="DynamicUIElement"/>s and their ids.
        /// </summary>
        private Dictionary<int, DynamicUIElement> _dynamicUiElements = new Dictionary<int, DynamicUIElement>();
        
        /// <summary>
        /// A dictionary that maps the ids of <see cref="DynamicUIElement"/>s
        /// to the <see cref="LogicComponent"/> it belongs to.
        /// </summary>
        private Dictionary<string, List<int>> _logicDynamicMapping = new Dictionary<string, List<int>>();

        /// <summary>
        /// A static int that represents the current unique id.
        /// </summary>
        private static int _currentId;

        /// <summary>
        /// This function registers a <see cref="DynamicUIElement"/> to the controller and sets its parent transform
        /// in the scene hierarchy.
        /// Additionally it activates the <see cref="DynamicUIElement"/> and adds it to <see cref="_dynamicUiElements"/>
        /// and maps it to a <see cref="LogicComponent"/> in <see cref="_logicDynamicMapping"/>.
        /// </summary>
        /// <param name="element">The <see cref="DynamicUIElement"/>.</param>
        /// <param name="logicComponentName">The name of the <see cref="LogicComponent"/> the <see cref="DynamicUIElement"/> belongs to.</param>
        public void RegisterDynamicUIElement(DynamicUIElement element, string logicComponentName)
        {
            if (element.ID >= 0 && _dynamicUiElements.ContainsKey(element.ID))
            {
                Debug.LogError("Element already registered!", this);
                return;
            }

            element.ID = _currentId++;

            if (!_logicDynamicMapping.ContainsKey(logicComponentName))
                _logicDynamicMapping.Add(logicComponentName, new List<int>());
            
            _logicDynamicMapping[logicComponentName].Add(element.ID);
            
            // safe relevant information of transform
            Vector3 elementPosition = element.GameObject.transform.position;
            Quaternion elementRotation = element.GameObject.transform.rotation;
            Vector3 elementLocalScale = element.GameObject.transform.localScale;
            
            // setting the parent of a transform will automatically change the transform itself
            element.GameObject.transform.SetParent(transform);
            
            // restore transform information
            element.GameObject.transform.localPosition = elementPosition;
            element.GameObject.transform.localRotation = elementRotation;
            element.GameObject.transform.localScale = elementLocalScale;
            
            // Instantiate(element.GameObject);
            _dynamicUiElements.Add(element.ID, element);
            
            element.GameObject.SetActive(true);
        }

        /// <summary>
        /// Destroys the <see cref="GameObject"/> of the <see cref="DynamicUIElement"/> and therefore removes it
        /// from the scene hierarchy. It also removes the corresponding entries in <see cref="_dynamicUiElements"/> and <see cref="_logicDynamicMapping"/>.
        /// </summary>
        /// <remarks>
        /// After calling this function you should not use your <see cref="DynamicUIElement"/> anymore in your code.
        /// </remarks>
        /// <param name="element">The <see cref="DynamicUIElement"/> to be destroyed.</param>
        public void DestroyDynamicUIElement(DynamicUIElement element)
        {
            if(!_dynamicUiElements.ContainsKey(element.ID))
            {
                Debug.LogError("Element with ID " + element.ID + " not found!", this);
                return;
            }

            Destroy(_dynamicUiElements[element.ID].GameObject);
            _dynamicUiElements[element.ID] = null;
            _dynamicUiElements.Remove(element.ID);

            foreach (var mapping in _logicDynamicMapping)
            {
                mapping.Value.Remove(element.ID);
            }
        }

        /// <summary>
        /// Activates all <see cref="DynamicUIElement"/>s that belong to the given <see cref="LogicComponent"/>.
        /// </summary>
        /// <param name="logicComponentName">The name of the <see cref="LogicComponent"/>.</param>
        public void ActivateByLogicComponent(string logicComponentName)
        {
            if(!_logicDynamicMapping.ContainsKey(logicComponentName)) return;
            foreach (int id in _logicDynamicMapping[logicComponentName])
            {
                ActivateDynamicUIElement(_dynamicUiElements[id]);
            }
        }
        
        /// <summary>
        /// Deactivates all <see cref="DynamicUIElement"/>s that belong to the given <see cref="LogicComponent"/>.
        /// </summary>
        /// <param name="logicComponentName">The name of the <see cref="LogicComponent"/>.</param>
        public void DeactivateByLogicComponent(string logicComponentName)
        {
            if(!_logicDynamicMapping.ContainsKey(logicComponentName)) return;
            foreach (int id in _logicDynamicMapping[logicComponentName])
            {
                DeactivateDynamicUIElement(_dynamicUiElements[id]);
            }
        }
        
        /// <summary>
        /// Activates an already existing <see cref="DynamicUIElement"/>.
        /// </summary>
        /// <param name="element">The <see cref="DynamicUIElement"/> to be activated.</param>
        public void ActivateDynamicUIElement(DynamicUIElement element)
        {
            _dynamicUiElements[element.ID].GameObject.SetActive(true);
        }
        
        /// <summary>
        /// Deactivates an already existing <see cref="DynamicUIElement"/>.
        /// </summary>
        /// <param name="element">The <see cref="DynamicUIElement"/> to be deactivated.</param>
        public void DeactivateDynamicUIElement(DynamicUIElement element)
        {
            _dynamicUiElements[element.ID].GameObject.SetActive(false);
        }
    }
}
