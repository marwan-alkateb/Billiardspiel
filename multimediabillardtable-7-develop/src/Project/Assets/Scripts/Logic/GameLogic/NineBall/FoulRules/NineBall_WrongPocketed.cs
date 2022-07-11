using Assets.Scripts;
using Logic.GameLogic.Consequences;
using Tracking;
using Tracking.Model;
using UnityEngine;

namespace Logic.GameLogic.NineBall.FoulRules
{
    /// <summary>
    /// This class implements the IFoulRule interface for the foul of a ball was wrong pocketed.
    /// </summary>
    public class NineBall_WrongPocketed : IFoulRule
    {
        /// <summary>
        /// This is the necessary implementation of the IFoulRule interface.
        /// In this case, it checks if a wrong ball was pocketed in this round.
        /// If the nine ball is pocketed wrongly, it will be respawned.
        /// Also it checks, if the current player hit the right ball but pocketed the nine ball.
        /// In this case the current player won the game.
        /// </summary>
        /// <param name="gameState"></param>
        /// <returns>
        /// BallInHand if white ball is pocketed, EndGame if nine is pocketed in a legal way, otherwise null.
        /// </returns>
        public Consequence CheckRule(GameState gameState)
        {
            PocketEvent[] pockets = gameState.GetPocketedEvents();
            NineBall_WrongTouch wrongTouch = new NineBall_WrongTouch();
            Consequence result = null;
            bool whiteBallPocketed = false;
            bool nineBallPocketed = false;

            // No need to check this rule if no ball was hit
            if (gameState.GetBallCollisions().Length == 0 || !gameState.DidCueBallCollide())
            {
                //UnityEngine.Debug.Log("9Ball: No ball was hit!");
                return result;
            }

            foreach (PocketEvent pocket in pockets)
            {
                if (pocket.Ball == BallTypes.FullWhite)
                {
                    whiteBallPocketed = true;
                    result = new BallInHand();
                }
                if (pocket.Ball == BallTypes.HalfYellow)
                {
                    nineBallPocketed = true;
                }
                if (pocket.Ball == BallTypes.HalfYellow && wrongTouch.CheckRule(gameState) != null)
                {
                    RespawnNineBall();
                    result = new BallInHand();
                }
                // won
                if (pocket.Ball == BallTypes.HalfYellow && wrongTouch.CheckRule(gameState) == null)
                {
                    new ChangePlayerTurn().ExecuteConsequence(gameState);
                    result = new EndGame(true);
                }
                if (nineBallPocketed && whiteBallPocketed)
                {
                    RespawnNineBall();
                    result = new BallInHand();
                }
            }
            return result;
        }
        
        /// <summary>
        /// This method respawn ball nine on the table.
        /// </summary>
        void RespawnNineBall()
        {
            GameObject nineBall = GameObject.Find("halfYellow");
            nineBall.transform.position = new Vector3(0.728f, 0.94f, -0.015f);
        }
        
        /// <summary>
        /// The explanation of this foul.
        /// </summary>
        /// <returns>
        /// A string with the explanation of this foul.
        /// </returns>
        public override string ToString()
        {
            return "Wrong ball was pocketed!";
        }
    }
}