
﻿using System;
using Logic.GameLogic.EightBallPool.FoulRules;
using UnityEngine;

namespace Logic.GameLogic.EightBallPool
{
    /// <summary>
    /// This class is the implementation of the Game EightBallPool
    /// </summary>
    public class EightBallPool : Game
    {
        /// <summary>
        /// This method starts the game by initializing the rules of EightBallPool.
        /// </summary>
        public override void OnActivate()
        {
            FoulRules = new IFoulRule[]
            {
                new EightBallPool_NoTouch(), 
                new EightBallPool_WrongPocketed(),
                new EightBallPool_WrongTouch(),
                new EightBallPool_NoPocketed()
            };
            base.OnActivate();
            try
            {
                GameObject.Find("HitsAvailable").active = false;
                GameObject.Find("BallQuantity").active = false;
                GameObject.Find("NineBallIcon").active = false;

                GameObject.Find("FirstPlayerNumberBalls").active = true;
                GameObject.Find("SecondPlayerNumberBalls").active = true;
                GameObject.Find("FirstPlayerIcon").active = true;
                GameObject.Find("SecondPlayerIcon").active = true;
            }
            catch (Exception e)
            {
                Debug.Log(e.ToString());
            }


            GameState.SetTwoPlayerGame(true);
            GameState.GameOverlay.SetFirstplayerIcons("NotSet");
	    GameState.GameOverlay.SetNineBallIconActive(false);
            GameState.GameOverlay.SetFirstPlayerNumberBallsText(GameState.GetPlayerOne().GetRemainingBalls().Count, true);
            GameState.GameOverlay.SetSecondPlayerNumberBallsText(GameState.GetPlayerTwo().GetRemainingBalls().Count, true);
        }

        /// <summary>
        /// This method checks the last turn for fouls and completes the turn. The team assigning function will be called
        /// if the teams are not set and the current turn is not the break-shot.
        /// The turn will also be reset after it was finished.
        /// </summary>
        protected override void ProcessTurn()
        {
            base.ProcessTurn();
            if (GameState.GetPlayerOne().GetBallGroup().Equals(Logic.GameLogic.Ball.BallGroup.NotSet) && !GameState.IsBreakShot())
            {
                AssignGroups();
            }
            else
            {
                updatePlayerBallCount();
            }
            GameState.GetCurrentPlayer().SetLastPocketed(GameState.GetLastOfGroupPocketed());
            GameState.ResetAfterTurn();
        }

        /// <summary>
        /// This method iterates through all balls which are pocketed when there are no groups assigned. There will be no
        /// groups assigned if either no balls or at least two balls with different groups (i.e. full and half) were pocketed.
        /// If all pocketed balls are from the same group, then the current player will be assigned with the corresponding group.
        /// The opponent will be assigned to the other group.
        /// </summary>
        private void AssignGroups()
        {
            if (GameState.GetPocketedEvents().Length > 0)
            {
                var firstPocketedBallGroup = GameState.GetBallHandler().GetBall(GameState.GetPocketedEvents()[0].Ball).GetGroup();
                foreach (var pocketedEvent in GameState.GetPocketedEvents())
                {
                    var pocketedBallGroup = GameState.GetBallHandler().GetBall(pocketedEvent.Ball).GetGroup();

                    if (pocketedBallGroup != firstPocketedBallGroup)
                    {
                        GameState.GetCurrentPlayer().SetBallGroup(Logic.GameLogic.Ball.BallGroup.NotSet);
                        GameState.GetOtherPlayer().SetBallGroup(Logic.GameLogic.Ball.BallGroup.NotSet); 
                        UnityEngine.Debug.Log("initial Player 1 Group: " + GameState.GetCurrentPlayer().GetBallGroup() + 
                                              "\ninitial Player 2 Group: " + GameState.GetCurrentPlayer().GetBallGroup());
                        return;
                    }

                    if (pocketedBallGroup == Logic.GameLogic.Ball.BallGroup.Full)
                    {
                        GameState.GetCurrentPlayer().SetBallGroup(Logic.GameLogic.Ball.BallGroup.Full);
                        GameState.GetOtherPlayer().SetBallGroup(Logic.GameLogic.Ball.BallGroup.Half);
                    }

                    if (pocketedBallGroup == Logic.GameLogic.Ball.BallGroup.Half)
                    {
                        GameState.GetCurrentPlayer().SetBallGroup(Logic.GameLogic.Ball.BallGroup.Half);
                        GameState.GetOtherPlayer().SetBallGroup(Logic.GameLogic.Ball.BallGroup.Full);
                    }
                }

                AssignBalls();

                GameState.GameOverlay.SetFirstplayerIcons(GameState.GetPlayerOne().GetBallGroup().ToString());
            }
            
            UnityEngine.Debug.Log("initial Player 1 Group: " + GameState.GetCurrentPlayer().GetBallGroup() +
                                  "\ninitial Player 2 Group: " + GameState.GetCurrentPlayer().GetBallGroup());
        }

        /// <summary>
        /// This method initially assigns the players all of the balls that they have to pocket excluding the black ball.
        /// </summary>
        private void AssignBalls()
        {
            foreach (var ball in GameState.GetBallHandler().GetInGameBalls())
            {
                if (GameState.GetPlayerOne().GetBallGroup().Equals(ball.GetGroup()))
                {
                    GameState.GetPlayerOne().AddBall(ball);
                    GameState.GameOverlay.SetFirstPlayerNumberBallsText(GameState.GetPlayerOne().GetRemainingBalls().Count, false);
                }
                else if (GameState.GetPlayerTwo().GetBallGroup().Equals(ball.GetGroup()))
                {
                    GameState.GetPlayerTwo().AddBall(ball);
                    GameState.GameOverlay.SetSecondPlayerNumberBallsText(GameState.GetPlayerTwo().GetRemainingBalls().Count, false);
                }
            }
        }

        /// <summary>
        /// Updates counts for both players after each turn, except the break shot and when no balls are assigned.
        /// </summary>
        private void updatePlayerBallCount()
        {
            if(GameState.GetPocketedEvents().Length > 0)
            {
                GameState.GameOverlay.SetFirstPlayerNumberBallsText(GameState.GetPlayerOne().GetRemainingBalls().Count, false);
                GameState.GameOverlay.SetSecondPlayerNumberBallsText(GameState.GetPlayerTwo().GetRemainingBalls().Count, false);
            }
        }
    }
}
