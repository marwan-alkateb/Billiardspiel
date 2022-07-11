using Display.UI.StaticUI;
using UnityEngine.UI;

namespace StaticUI
{
    /// <summary>
    /// This class is for demonstration purposes only. If its not used it may be removed.
    /// </summary>
    public class DemoStaticUISettings : StaticUIComponent
    {
        public void OnBackButtonClick()
        {
            StaticUIController.SwitchActiveUI("DemoStaticUIBase");
        }

        public void OnPositionToggle(Toggle change)
        {
            if (change.isOn)
            {
                StaticUIController.logicController.ActivateLogic("DemoLogicText");
            }
            else
            {
                StaticUIController.logicController.DeactivateLogic("DemoLogicText");
            }
        }
        
        public void OnLineToggle(Toggle change)
        {
            if (change.isOn)
            {
                StaticUIController.logicController.ActivateLogic("DemoLogicLine");
            }
            else
            {
                StaticUIController.logicController.DeactivateLogic("DemoLogicLine");
            }
        }
        
        public void OnSpriteToggle(Toggle change)
        {
            if (change.isOn)
            {
                StaticUIController.logicController.ActivateLogic("DemoLogicSprite");
            }
            else
            {
                StaticUIController.logicController.DeactivateLogic("DemoLogicSprite");
            }
        }
    }
}
