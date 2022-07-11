using System;
using System.Collections.Generic;
using Display.UI.StaticUI;
using Logic.GameLogic.Util;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace StaticUI
{
    public class GameOverlay : StaticUIComponent
    {
        /// <summary>
        /// Constant for number of balls for the game type eightball.
        /// </summary>
        public const int NumberOfBallsForEightBall = 7;

        /// <summary>
        /// TextMeshProGUI of the first player.
        /// </summary>
        [SerializeField] private TextMeshProUGUI firstPlayer;

        /// <summary>
        /// Image of the first players ball-group icon.
        /// </summary>
        [SerializeField] private Image firstPlayerIcon;

        /// <summary>
        /// TextMeshProGUI of the number of balls of the first player.
        /// </summary>
        [SerializeField] private TextMeshProUGUI firstPlayerNumberBalls;

        /// <summary>
        /// TextMeshProGUI of the second player.
        /// </summary>
        [SerializeField] private TextMeshProUGUI secondPlayer;

        /// <summary>
        /// Image of the second players ball-group icon.
        /// </summary>
        [SerializeField] private Image secondPlayerIcon;

        /// <summary>
        /// Image of the ball that needs to be pocketed next.
        /// </summary>
        [SerializeField] private Image nineBallIcon;

        /// <summary>
        /// TextMeshProGUI of the number of balls of the second player.
        /// </summary>
        [SerializeField] private TextMeshProUGUI secondPlayerNumberBalls;


        /// <summary>
        /// TextMeshProGUI for displaying the current foul rule.
        /// </summary>
        [SerializeField] private TextMeshProUGUI foulRule;

        /// <summary>
        /// Icon used to display the full-colored ballgroup
        /// </summary>
        [SerializeField] private Sprite iconFull;

        /// <summary>
        /// Icon used to display the half-colored ballgroup
        /// </summary>
        [SerializeField] private Sprite iconHalf;

        /// <summary>
        /// All ball-icons for the balls from 1-9 for the NineBall gamemode
        /// </summary>
        [SerializeField] private List<Sprite> Icon9Ball;

        /// <summary>
        /// TextMeshProGUI for displaying the current number of balls in Trickshot mode.
        /// </summary>
        [SerializeField] private TextMeshProUGUI ballCountTrickshot;

        /// <summary>
        /// TextMeshProGUI for displaying the current number of hits in Trickshot mode.
        /// </summary>
        [SerializeField] private TextMeshProUGUI hitCountTrickshot;
        
        /// <summary>
        /// Property to display the text of the number of hits.
        /// </summary>
        public void SetHitCountTrickshot(int hitsAvailable, int shootCount)
        {
            hitCountTrickshot.text = string.Format("Hits: {0}/{1}", shootCount, hitsAvailable);
        }

        /// <summary>
        /// Property to display the text of the number of balls.
        /// </summary>
        public void SetBallCountTrickshot(int ballQuantity)
        {
            ballCountTrickshot.text = string.Format("Balls: {0}", ballQuantity);
        }

        /// <summary>
        /// Button to go back to the previous menu
        /// </summary>
        [SerializeField] private GameObject button;

        /// <summary>
        /// Property to access the text of the first player.
        /// </summary>
        public string FirstPlayerText
        {
            get => firstPlayer.text;

            set => firstPlayer.text = value;
        }

        /// <summary>
        /// Property to access the color of the first player.
        /// </summary>
        public Color FirstPlayerColor
        {
            get => firstPlayer.color;
            set => firstPlayer.color = value;
        }

        /// <summary>
        /// Property to set the text of the number of balls of the first player.
        /// </summary>
        public void SetFirstPlayerNumberBallsText(int balls, bool beforeGroupAssignment)
        {
            int remainingBalls = -1;
            if (beforeGroupAssignment)
            {
                remainingBalls = balls;
            }
            else
            {
                remainingBalls = NumberOfBallsForEightBall - balls;
            }

            firstPlayerNumberBalls.text = string.Format("{0} / {1}", remainingBalls, NumberOfBallsForEightBall);
        }


        /// <summary>
        /// Property to access the text of the second player.
        /// </summary>
        public string SecondPlayerText
        {
            get => secondPlayer.text;

            set => secondPlayer.text = value;
        }

        /// <summary>
        /// Property to access the color of the second player.
        /// </summary>
        public Color SecondPlayerColor
        {
            get => secondPlayer.color;
            set => secondPlayer.color = value;
        }

        /// <summary>
        /// Sets the icons for both teams (Player 2 gets the remaining color)
        /// </summary>
        /// <param name="group">"half" for the half-colored ballgroup and "full" for the full-colored ball group (anything else will deactivate the icons)</param>
        public void SetFirstplayerIcons(string group)
        {
            firstPlayerIcon.GetComponent<Image>().enabled = true;
            secondPlayerIcon.GetComponent<Image>().enabled = true;
            if (group == "Half")
            {
                firstPlayerIcon.sprite = iconHalf;
                secondPlayerIcon.sprite = iconFull;
            }
            else if (group == "Full")
            {
                firstPlayerIcon.sprite = iconFull;
                secondPlayerIcon.sprite = iconHalf;
            }
            else
            {
                firstPlayerIcon.GetComponent<Image>().enabled = false;
                secondPlayerIcon.GetComponent<Image>().enabled = false;
            }
        }

        /// <summary>
        /// Sets the icon of the next ball that needs to be pocketed
        /// </summary>
        /// <param name="ballNumber">number of the ball that needs to be pocketed</param>
        public void SetNineBallIcon(int ballNumber)
        {
            if (ballNumber >= 1 && ballNumber <= 9)
            {
                int i = ballNumber - 1;
                SetNineBallIconActive(true);
                nineBallIcon.sprite = Icon9Ball[i];
            }
        }
        
        /// <summary>
        /// Activates or deactivates the image of the NineBall-Icon
        /// </summary>
        public void SetNineBallIconActive(bool state)
        {
            nineBallIcon.GetComponent<Image>().enabled = state;
        }

        /// <summary>
        /// Property to set the text of the number of balls of the second player.
        /// </summary>
        public void SetSecondPlayerNumberBallsText(int balls, bool beforeGroupAssignment)
        {
            int remainingBalls = -1;
            if (beforeGroupAssignment)
            {
                remainingBalls = balls;
            }
            else
            {
                remainingBalls = NumberOfBallsForEightBall - balls;
            }

            secondPlayerNumberBalls.text = string.Format("{0} / {1}", remainingBalls, NumberOfBallsForEightBall);
        }

        /// <summary>
        /// Property to access the text of the foul rule TextMeshProGUI.
        /// </summary>
        public string FoulRuleText
        {
            get => foulRule.text;

            set => foulRule.text = value;
        }

        /// <summary>
        /// This function initializes the player names with the names saved in PlayerPrefs.
        /// This happens whenever the StaticUI gets activated.
        /// </summary>
        public override void OnActivate()
        {
            FirstPlayerText = PlayerPrefs.GetString("firstName");
            SecondPlayerText = PlayerPrefs.GetString("secondName");
            SetFirstplayerIcons("none");
            SetNineBallIcon(0);
        }

        private void Update()
        {

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log(button);
                button.SetActive(true);
            }



            if (Input.GetKeyDown(KeyCode.S))
            {

                StaticUIController.SwitchActiveUI("SettingsMenu");

            }
        }            

        public void ClickBackButton()
        {
            GameMode gameMode = (GameMode)PlayerPrefs.GetInt("game-mode");
            if (gameMode == GameMode.Trickshot)
            {
                StaticUIController.SwitchActiveUI("TrickshotMenu");
            }
            else
            {
                StaticUIController.SwitchActiveUI("NameInputMenu");

                if(gameMode == GameMode.EightBall)
                {
                    StaticUIController.logicController.DeactivateLogic("EightBallLogic");
                }else if(gameMode == GameMode.NineBall)
                {
                    StaticUIController.logicController.DeactivateLogic("NineBallLogic");
                }

            }

            button.SetActive(false);
        }
    }
}