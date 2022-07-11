using Logic.GameLogic.Util;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Logic.GameLogic.TrickshotMode
{
    [Obsolete("Class is depricated, use the TrickShotMenu Class")]

    class StartTrickshotMode : MonoBehaviour
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
        private int tmpint;


        ///<summary>
        /// Loads the trickshot game scene
        ///</summary>
        public void PlayGame()
        {
            // global::GameLogic.GameSelection.GameSelect.GameModeInformation = GameMode.Trickshot;
            SetGameParameters();
            SceneManager.LoadScene("Game");
        }

        ///<summary>
        /// Sets the game parameters with the input from the UI but is never called ???????!
        ///</summary>
        public void SetGameParameters()
        {
            string hits = hitsAvailable.text;
            GameObject hintPopup = GameObject.Find("ModalPopupTrickshot");
            try
            {
                System.Int32 int_hits;
                if (int.TryParse(hits, out int_hits))
                {
                    hintPopup.SetActive(false);
                    PlayerPrefs.SetInt("hitsAvailable", int_hits);
                }
                else
                {
                    hintPopup.SetActive(true);
                }
                
                string balls = ballQuantity.text;

                System.Int32 int_balls;
                if (int.TryParse(balls, out int_balls))
                {
                    hintPopup.SetActive(false);
                    PlayerPrefs.SetInt("ballQuantity", int_balls);
                }
                else
                {
                    hintPopup.SetActive(true);
                }
            }
            catch (System.Exception)
            {
                UnityEngine.Debug.Log("String to Integer Exception !!");
            }

            int pocketDropdownIndex = selectedPocket.value;
            PlayerPrefs.SetInt("selectedPocket", pocketDropdownIndex);
            
            
            
        }
    }
}
