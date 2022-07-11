using Display.UI.StaticUI;
using Logic.GameLogic.Util;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinningScreen : StaticUIComponent
{
    /// <summary>
    /// TextMeshProGUI of the first player.
    /// </summary>
    [SerializeField] private TextMeshProUGUI firstPlayer;

    /// <summary>
    /// TextMeshProGUI of the first players team.
    /// </summary>
    [SerializeField] private TextMeshProUGUI firstPlayerTeam;

    /// <summary>
    /// TextMeshProGUI of the second player.
    /// </summary>
    [SerializeField] private TextMeshProUGUI secondPlayer;

    /// <summary>
    /// TextMeshProGUI of the second players team.
    /// </summary>
    [SerializeField] private TextMeshProUGUI secondPlayerTeam;

    /// <summary>
    /// TextMeshProGUI for displaying the current foul rule.
    /// </summary>
    [SerializeField] private TextMeshProUGUI foulRule;

    public void PlayAgain()
    {
        PlayAgainResetText();
        StaticUIController.SwitchBack();

        GameMode gameMode = (GameMode)PlayerPrefs.GetInt("game-mode");
        ResetLogicAndBalls(gameMode.ToString()+ "Logic");


    }

    public void ResetLogicAndBalls(string gameLogic)
    {
        GameObject.Find("Balls").GetComponent<ResetBalls>().Reset(gameLogic);
        StaticUIController.logicController.DeactivateLogic(gameLogic);
        StaticUIController.logicController.ActivateLogic(gameLogic);
    }

    public void BackToMenu()
    {
        GameMode gameMode = (GameMode)PlayerPrefs.GetInt("game-mode");
        if (gameMode == GameMode.Trickshot)
        {
            StaticUIController.SwitchActiveUI("TrickshotMenu");
        }
        else
        {
            StaticUIController.SwitchActiveUI("NameInputMenu");

            if (gameMode == GameMode.EightBall)
            {
                StaticUIController.logicController.DeactivateLogic("EightBallLogic");
            }
            else if (gameMode == GameMode.NineBall)
            {
                StaticUIController.logicController.DeactivateLogic("NineBallLogic");
            }

        }

        PlayAgainResetText();
        StaticUIController.SwitchActiveUI("StartMenu");
    }


    public void PlayAgainResetText()
    {
        firstPlayer.text = "";
        secondPlayer.text = "";
        //firstPlayerTeam.text = "";
        //secondPlayerTeam.text = "";
        foulRule.text = "";
    }



}
