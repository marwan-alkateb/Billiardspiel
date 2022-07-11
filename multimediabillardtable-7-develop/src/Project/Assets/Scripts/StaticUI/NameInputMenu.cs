using Display.UI.StaticUI;
using Logic.GameLogic.Util;
using TMPro;
using UnityEngine;

namespace StaticUI
{
    public class NameInputMenu : StaticUIComponent
    {
        /// <summary>
        /// <param name="firstname">the name that is stored, if no name is in the input for the first name</param>
        /// <param name="secondname">the name that is stored, if no name is in the input for the second name</param>
        /// <param name="firstNameInputField">the input field for the first player name</param>
        /// <param name="secondNameInputField">the input field for the second player name</param>
        /// <param name="buttonclick">contains the audiosource which is played if a button is clicked</param>
        /// </summary>
        public string firstname = "First Player";
        public string secondname = "Second Player";
        public TMP_InputField firstNameInputField;
        public TMP_InputField secondNameInputField;
        public AudioSource buttonclick;

        /// <summary>
        /// calls the storeName method, than starts playing the sound in the AudioSource Input and starts a Coroutine
        /// </summary>
        public void PlayGame()
        {
            StoreName();
            // buttonclick.Play();
            StaticUIController.SwitchActiveUI("GameOverlay");

            GameMode gameMode = (GameMode) PlayerPrefs.GetInt("game-mode");
            if (gameMode == GameMode.EightBall)
            {
                StaticUIController.logicController.ActivateLogic("EightBallLogic");
            } else if (gameMode == GameMode.NineBall)
            {
                StaticUIController.logicController.ActivateLogic("NineBallLogic");
            }
            
        }

        /// <summary>
        /// stores the input of the firstNameInputField and secondNameInputField in the PlayerPrefs. If the input fields are empty it uses the firstname and secondname string
        /// </summary>
        private void StoreName()
        {
            if (firstNameInputField.GetComponent<TMP_InputField>().text == "")
            {
                firstname = "First Player";
                PlayerPrefs.SetString("firstName", firstname);
            }
            else
            {
                firstname = firstNameInputField.GetComponent<TMP_InputField>().text;
                PlayerPrefs.SetString("firstName", firstname);
            }
            if (secondNameInputField.GetComponent<TMP_InputField>().text == "")
            {
                secondname = "Second Player";
                PlayerPrefs.SetString("secondName", secondname);
            }
            else
            {
                secondname = secondNameInputField.GetComponent<TMP_InputField>().text;
                PlayerPrefs.SetString("secondName", secondname);
            }
        }

        public void BackButtonClick()
        {
            // buttonclick.Play();
            StaticUIController.SwitchActiveUI("StartMenu");
        }

        /// <summary>
        /// The PlayTrickshot method plays the sound which is stored in the audiosource and starts a routine which waits for the sound the end 
        /// </summary>
        public void SettingsMenuClicked()
        {
            // buttonclick.Play();
            StaticUIController.SwitchActiveUI("SettingsMenu");
        }
    }
}
