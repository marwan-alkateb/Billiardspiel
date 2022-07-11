using Logic.GameLogic.TrickshotMode.FoulRules;
using UnityEngine;

namespace Logic.GameLogic.TrickshotMode
{
    class TrickshotMode : Game
    {
        /// <summary>
        /// This method starts the game by initializing the rules.
        /// </summary>
        public override void OnActivate()
        {

            FoulRules = new IFoulRule[]
            {
                //initialize rules here
                new TrickshotMode_PocketedAllReqiredBalls(),
                new TrickshotMode_OutOfTurns(),
                new TrickshotMode_WrongPocket()
            };          
            
           
            base.OnActivate();
            GameObject.Find("FirstPlayerIcon").active = false;
            GameObject.Find("SecondPlayerIcon").active = false;
            GameObject.Find("FirstPlayerNumberBalls").active = false;
            GameObject.Find("SecondPlayerNumberBalls").active = false;
            GameObject.Find("NineBallIcon").active = false;
            GameObject.Find("HitsAvailable").active = true;
            GameObject.Find("BallQuantity").active = true;
            GameState.InitializeTrickshotVariables();
            GameState.SetTwoPlayerGame(false);

            GameState.GameOverlay.SetHitCountTrickshot(GameState.GetHitsAvailbale(), GameState.GetTouchControl().getShootCount());
            GameState.GameOverlay.SetBallCountTrickshot(GameState.GetQuantityToBePocketed());
        }

        /// <summary>
        /// Override player initialization because trickshot is single player mode
        /// </summary>
        protected override void InitializeCurrentPlayer() 
        {
            GameState.SetCurrentPlayer(new Player());

            GameState.GameOverlay.FirstPlayerText = "";
            GameState.GameOverlay.SecondPlayerText = "";
        }

        /// <summary>
        /// Processes turn by checking a hits and quantity that needs to be pocketed.
        /// </summary>
        protected override void ProcessTurn()
        {
            GameState.CheckHitsAvailable();
            GameState.ReduceQuantityToBePocketed();

            GameState.GameOverlay.SetHitCountTrickshot(GameState.GetHitsAvailbale(), GameState.GetTouchControl().getShootCount());
            GameState.GameOverlay.SetBallCountTrickshot(GameState.GetQuantityToBePocketed());

            base.ProcessTurn();

            GameState.ResetAfterTurn();
        }
    }
}
