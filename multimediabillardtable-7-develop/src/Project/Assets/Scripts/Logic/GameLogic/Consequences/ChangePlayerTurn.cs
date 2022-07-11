using Logic.GameLogic.Util;
using System.Collections;
using UnityEngine;

namespace Logic.GameLogic.Consequences
{
    /// <summary>
    /// This class implements the foul consequence, that changes the turns of the players.
    /// </summary>
    public class ChangePlayerTurn : Consequence
    {
        public GameObject switchSound;
        public AudioSource mySource;

        public ChangePlayerTurn()
        {
            Severity = SeverityUtil.Severity.Low;
        }
     
        /// <summary>
        /// This function sets the current player to the opponent player. The new current players name color will be set
        /// to green and the previous players name color to white.  
        /// </summary>
        /// <param name="gameState">
        /// is the current State of the Game
        /// </param>
        public override void ExecuteConsequence(GameState gameState)
        {
            UnityEngine.Debug.Log("Switching Team");

            //GetComponent<AudioManager>().Play("SwitchPlayer");
            switchSound = GameObject.Find("Speaker");
            mySource = switchSound.GetComponent<AudioSource>();
            mySource.Play();
            //switchSound.AudioSource.Play();
            
            gameState.SwitchCurrentPlayer();
            gameState.CurrentPlayerColor = new Color32(0xFE, 0x61, 0x00, 0xFF);
            gameState.OtherPlayerColor = new Color32(0xB3, 0xDA, 0xDE, 0xFF);
        }
    }
}