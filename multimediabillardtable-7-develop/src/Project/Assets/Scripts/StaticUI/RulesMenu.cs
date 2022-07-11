using Display.UI.StaticUI;
using UnityEngine;

namespace StaticUI
{
    public class RulesMenu : StaticUIComponent
    {
        /// <summary>
        /// On-Click handler for the back button to return to menu
        /// </summary>
        public void BackButtonClicked()
        {
            StaticUIController.SwitchBack();
        }

        public void EightBallButtonClicked()
        {
            StaticUIController.SwitchActiveUI("EightBallRulesMenu");
        }

        public void NineBallButtonClicked()
        {
            StaticUIController.SwitchActiveUI("NineBallRulesMenu");
        }

        public void TrickshotButtonClicked()
        {
            StaticUIController.SwitchActiveUI("TrickshotRulesMenu");
        }
    }
}


