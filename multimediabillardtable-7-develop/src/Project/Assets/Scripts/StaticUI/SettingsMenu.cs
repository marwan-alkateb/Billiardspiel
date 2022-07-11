using Display.UI.StaticUI;
using UnityEngine;

namespace StaticUI
{
    public class SettingsMenu : StaticUIComponent
    {
        /// <summary>
        /// On Click handler for the back button
        /// </summary>
        public void BackButtonClicked()
        {
           // StaticUIController.SwitchActiveUI("StartMenu");
           StaticUIController.SwitchBack();

        }
        private void Update()
        {

            if (Input.GetKeyDown(KeyCode.S))
            {

                StaticUIController.SwitchBack();

            }
        }
    }
}
