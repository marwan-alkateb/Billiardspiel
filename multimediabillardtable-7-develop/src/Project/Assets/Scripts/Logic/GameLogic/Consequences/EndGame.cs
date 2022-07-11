using Display.UI.StaticUI;
using Logic.GameLogic.Util;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Logic.GameLogic.Consequences
{
    /// <summary>
    /// This class implements the foul consequence that ends the Game.
    /// </summary>
    public class EndGame :  Consequence
    {
        /// <summary>
        /// True if the current player was allowed to pocket the black ball.
        /// </summary>
        private readonly bool _legalMove;

        private StaticUIController staticUIController;

        /// <summary>
        /// This is the Constructor, which sets the severity of of this consequence to critical.
        /// </summary>
        public EndGame(bool legalMove = false)
        {
            Severity = SeverityUtil.Severity.Critical;
            _legalMove = legalMove;
            staticUIController = GameObject.Find("StaticUI").GetComponent<StaticUIController>();
        }

        /// <summary>
        /// This method executes the actual consequence.
        /// The "EndDisplay" GameObject is searched in the current scene and gets the name of the winner.
        /// It will then be set to active, so it will be visible on the screen.
        /// </summary>
        /// <param name="gameState">
        /// The current state of the Game
        /// </param>
        public override void ExecuteConsequence(GameState gameState)
        {
            UnityEngine.Debug.Log("Game is over");

            GameMode gameMode = (GameMode)PlayerPrefs.GetInt("game-mode");
            if (gameMode == GameMode.EightBall || gameMode == GameMode.NineBall)
            {

                staticUIController.SwitchActiveUI("WinningScreen");

                var text = GameObject.Find("PlayerWinText").GetComponent<TextMeshProUGUI>(); //Winner

                if (gameState.GetTwoPlayerGame() == true)
                {
                    Player winner = _legalMove
            ? gameState.GetCurrentPlayer()
            : gameState.GetOtherPlayer();
                    text.SetText(winner.GetName() + " Won the Game!");
                }
                else
                {
                    if (_legalMove == true)
                    {
                        text.SetText("You won the Game!");
                    }
                    else
                    {
                        text.SetText("You lost the Game!");
                    }
                }

            }
            /*var rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
            foreach (var rootGameObject in rootGameObjects)
            {
                if (rootGameObject.gameObject.name == "End Display")
                {
                    var endDisplay = rootGameObject.gameObject;
                    endDisplay.SetActive(true);
                    var text = endDisplay.transform.Find("Winner").GetComponent<TextMeshProUGUI>();

                    if (gameState.GetTwoPlayerGame() == true)
                    {
                        Player winner = _legalMove
                ? gameState.GetCurrentPlayer()
                : gameState.GetOtherPlayer();
                        text.SetText(winner.GetName() + " Won the Game!");
                    }
                    else
                    {
                        if (_legalMove == true)
                        {
                            text.SetText("You won the Game!");
                        }
                        else
                        {
                            text.SetText("You lost the Game!");
                        }
                    }
                }    
            }*/
        }
    }
}