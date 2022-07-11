using Display.UI.StaticUI;
using Logic.GameLogic.Util;
using UnityEngine;

namespace StaticUI
{
    public class StartMenu : StaticUIComponent
    {
        // public AudioSource buttonclick;

        
        /// <summary>
        /// setting the playerref. game-mode to MainMenu
        /// </summary>  
        public void Start()
        {
            PlayerPrefs.SetInt("game-mode", (int)GameMode.MainMenu);
            Debug.Log("StartMenu constructed, resetting game-mode");
        }

        
        /// <summary>
        /// The PlayGame method starts a routine NameInput and saves GameMode (Enum) in the PlayerPref 
        /// </summary>

        private GameMode gameMode;
        public void PlayGame()
        {
            gameMode = GameMode.EightBall;
            PlayerPrefs.SetInt("game-mode", (int)GameMode.EightBall);
            StaticUIController.SwitchActiveUI("NameInputMenu");
        }

        /// <summary>
        /// The PlayNineBall method starts a routine NameInput and saves GameMode (Enum) in the PlayerPref
        /// </summary>
        public void PlayNineBall()
        {
            gameMode = GameMode.NineBall;
            StaticUIController.SwitchActiveUI("NameInputMenu");
            PlayerPrefs.SetInt("game-mode", (int)GameMode.NineBall);
        }

        /// <summary>
        /// The PlayTrickshot method plays the sound which is stored in the audiosource and starts a routine which waits for the sound the end 
        /// </summary>
        public void PlayTrickshot()
        {
            PlayerPrefs.SetInt("game-mode", (int)GameMode.Trickshot);
            StaticUIController.SwitchActiveUI("TrickshotMenu");
        }

        /// <summary>
        /// The QuitGame method plays the sound which is stored in the audiosource and starts a routine which waits for the sound the end 
        /// </summary>
        public void QuitGame()
        {
            // buttonclick.Play();
            Debug.Log("QUIT");
            Application.Quit();
        }

        /// <summary>
        /// The RulesMenu method shows the rules and open three tabs for further information
        /// </summary>
        public void ShowRules()
        {
            // buttonclick.Play();
            StaticUIController.SwitchActiveUI("RulesMenu");
        }

        /// <summary>
        /// The PlayTrickshot method plays the sound which is stored in the audiosource and starts a routine which waits for the sound the end 
        /// </summary>
        public void SettingsMenuClicked()
        {
            // buttonclick.Play();
            StaticUIController.SwitchActiveUI("SettingsMenu");
        }

        public GameMode GetGameMode()
        {
            return this.gameMode;
        }
    }
}
