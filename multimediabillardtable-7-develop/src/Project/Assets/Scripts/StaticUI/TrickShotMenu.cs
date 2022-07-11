using Display.UI.StaticUI;
using TMPro;
using UnityEngine;

namespace StaticUI
{
    public class TrickShotMenu : StaticUIComponent
    { 
        ///<summary>
        /// gets initialized with the input from the hits available input field from the UI
        ///</summary> 
        [SerializeField] TMP_InputField hitsAvailable;
        ///<summary>
        /// gets initialized with the input from the number of balls input field from the UI
        ///</summary>
        [SerializeField] TMP_InputField ballQuantity;
        ///<summary>
        /// gets initialized with the input from the select pocket from the UI
        ///</summary> 
        [SerializeField] TMP_Dropdown selectedPocket;

        [SerializeField] public Transform TrickshotCanvas;

        ///<summary>
        /// Cecks if all parameters are correct and if so loads the trickshot game scene
        ///</summary>
        public void PlayGame()
        {
            bool isCorrect = SetGameParameters();

            if (isCorrect)
            {
                StaticUIController.SwitchActiveUI("GameOverlay");
                StaticUIController.logicController.ActivateLogic("TrickshotLogic");
                			    
            }
            else
            {
                ///////!!!! THE FOLLOWING IS NOT TO BE USED WHEN INTEGRATING WITH PROJECTS
                OkPopup okPopup = PopupController.Instance.CreateOkPopup();
                // messageBox.SetMessage("Please enter numbers only");
                // You can choose if you want to enable the buttons(with custom text) or not
                okPopup.Init(TrickshotCanvas, "Please Enter only numbers!", "ok", () => { Debug.Log("Handling ok response..."); });

            }
        }

        ///<summary>
        /// Sets the game parameters with the input from the UI, returns true if the user input is correct.
        /// Shows popup if the userinput is not correct (e.g. letters)
        ///</summary>
        public bool SetGameParameters()
        {
            bool intHitNum = true;
            bool intBallNum = true;

            string hits = hitsAvailable.text;

            try
            {
                if (int.TryParse(hits, out int int_hits) && int_hits >= 1)
                {
                    PlayerPrefs.SetInt("hitsAvailable", int_hits);
                }
                else
                {
                    intHitNum = false;
                    UnityEngine.Debug.LogWarning("Hits input is not a number");
                }

                string balls = ballQuantity.text;

                if (int.TryParse(balls, out int int_balls) && int_balls >= 1)
                {
                    PlayerPrefs.SetInt("ballQuantity", int_balls);
                }
                else
                {
                    intBallNum = false;
                    UnityEngine.Debug.LogWarning("Balls input is not a number");
                }
            }
            catch (System.Exception e)
            {
                UnityEngine.Debug.LogErrorFormat("String to Integer Exception - error:", e);
            }

            int pocketDropdownIndex = selectedPocket.value;
            PlayerPrefs.SetInt("selectedPocket", pocketDropdownIndex);

            return intHitNum && intBallNum ? true : false;
        }
        
        /// <summary>
        /// Function for changing the UI to the Settingsmenu 
        /// </summary>
        public void SettingsMenuClicked()
        {
            // buttonclick.Play();
            StaticUIController.SwitchActiveUI("SettingsMenu");
        }
    }
}
