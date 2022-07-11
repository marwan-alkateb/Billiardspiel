using System;
using System.Collections.Generic;
using Assets.Scripts;
using Display.Logic;
using Logic.GameLogic;
using Logic.GameLogic.Consequences;
using StaticUI;
using Tracking;
using Tracking.Model;
using UnityEngine;

namespace Logic.GameLogic
{
    /// <summary>
    /// This parent class handles all procedures of a game which are not specific to 8Ball or 9Ball
    /// It inherits from the TrackingBehaviour class which is a unity Monobehaviour script.
    /// This class is essentially the interface to the Unity Engine.
    /// The Game is also initialised (started) in this class
    /// </summary>
    /// <remarks>
    /// Through this inheritance of TrackingBehaviour the reception of Tracking frames is handled.
    /// Every received frame triggers a run through all possible game processing options.
    /// Which processing is necessary at the moment is calculated in this class.
    /// </remarks>
    public abstract class Game : LogicComponent
    {
        /// <summary>
        /// This contains all relevant information of the current state of the game.
        /// </summary>
        protected GameState GameState;
        /// <summary>
        /// This contains all the foulrules to be checked for the selected game-mode.
        /// </summary>
        protected IFoulRule[] FoulRules;
        /// <summary>
        /// Marker for Storing the vital information if a turn is currently still being played.
        /// This is important, because up until the end of a turn tracking information gets saved to a buffer, 
        /// to be collectively read from after the turn ends.
        /// </summary>
        protected bool TurnIsOngoing;

        /// <summary>
        /// The <see cref="GameOverlay"/> used by the game
        /// </summary>
        [SerializeField] private GameOverlay gameOverlay;
        
        /// <summary>
        /// The <see cref="MoveWhiteBallOverlay"/> used by the game
        /// </summary>
        [SerializeField] private MoveWhiteBallOverlay moveWhiteBallOverlay;

        /// <summary>
        /// This method starts a game by initializing a new game state, the current player, the textArea of the GUI
        /// on which the results of a turn will be displayed and the receiving of tracking data
        /// </summary>
        public override void OnActivate()
        {
            GameState = new GameState(gameOverlay, moveWhiteBallOverlay, LogicController);
            
            TurnIsOngoing = false;
            
            InitializeCurrentPlayer();
        }

        /// <summary>
        /// This method initializes randomly which player makes the first turn at the beginning of Game.
        /// Also the name of this player will be assigned with the color green. 
        /// </summary>
        protected virtual void InitializeCurrentPlayer()
        {
            var rnd = new System.Random();

            GameState.SetCurrentPlayer(
                Convert.ToBoolean(rnd.Next(0, 2))
                    ? GameState.GetPlayerOne()
                    : GameState.GetPlayerTwo());

            GameState.CurrentPlayerColor = new Color32(0xFE, 0x61, 0x00, 0xFF);
            GameState.OtherPlayerColor = new Color32(0xB3, 0xDA, 0xDE, 0xFF);
        }

        /// <summary>
        /// Receives updates (tracking frames) from the tracking API and processes the incoming information.
        /// This is essentially a frame and the trigger for every processing of the game-logic.
        /// </summary>
        /// <remarks>
        /// If a turn is
        /// ...currently being played the tracking information of the frame is added to the game-state.
        /// ...finished in this exact frame (checked by executing BallsMoving()), ProcessTurn() is called.
        /// </remarks>
        public override void LogicUpdate(Tracking.Model.Ball[] tmpBalls, Dictionary<BallType, Vector2> denormalizedBallPositions, BallCollisionEvent[] ballCollisions, PocketEvent[] pocketEvents, WallCollisionEvent[] wallCollisions)
        {

            // Update balls
            foreach (var tmpBall in tmpBalls)
            {
                global::Logic.GameLogic.Ball ball = GameState.GetBallHandler().GetBall(tmpBall.Type);
                ball.SetPosition(tmpBall.Position);
                ball.SetIsOnTable(tmpBall.OnTable);
            }
            
            if (!GameState.GetFreeWhiteBall())
            {
                // Check if we should check for new events.
                // This ensures that ProcessTurn() is only called after some balls
                // were moving and stopped again.
                if (!TurnIsOngoing)
                {
                    if (BallsMoving())
                    {
                        TurnIsOngoing = true;
                    }
                }
                else 
                {
                    if (BallsMoving())
                    {
                        // Add new events to game-state
                        gameOverlay.FoulRuleText = " ";
                        // ResultDisplayText.text = " ";
                        foreach (BallCollisionEvent ballCollision in ballCollisions)
                        {
                            GameState.AddBallCollision(ballCollision);
                        }
                        foreach (PocketEvent pocketEvent in pocketEvents)
                        {
                            GameState.AddPocketEvent(pocketEvent);
                        }
                        foreach (WallCollisionEvent wallCollision in wallCollisions)
                        {
                            GameState.AddWallCollision(wallCollision);
                        }
                    }
                    else
                    {
                        ProcessTurn();
                    }
                }
            }
        }

        /// <summary>
        /// This method updates which balls the players still have to pocket,
        /// checks the last turn for fouls and completes the turn. 
        /// </summary>
        protected virtual void ProcessTurn()
        {
            UpdatePlayerBalls();
            
            CheckLastTurnForFouls();

            TurnIsOngoing = false;
            GameState.SetBreakShot(false);
        }

        /// <summary>
        /// Removes all pocketed balls from the list of balls to be pocketed from the corresponding player.
        /// Also adds the black ball to the list if the last object ball was pocketed.
        /// </summary>
        private void UpdatePlayerBalls()
        {
            if (GameState.GetPlayerOne().GetBallGroup().Equals(global::Logic.GameLogic.Ball.BallGroup.NotSet))
            {
                return;
            }
            
            foreach (PocketEvent pocketEvent in GameState.GetPocketedEvents())
            {
                global::Logic.GameLogic.Ball ball = GameState.GetBallHandler().GetBall(pocketEvent.Ball);
                // ball of player one
                if (ball.GetGroup().Equals(GameState.GetPlayerOne().GetBallGroup()))
                {
                    GameState.GetPlayerOne().RemoveBall(ball);
                    // check if this was the last object ball
                    if (GameState.GetPlayerOne().GetRemainingBalls().Count == 0)
                    {
                        GameState.GetPlayerOne().AddBall(GameState.GetBallHandler().GetBall(BallTypes.FullBlack));
                        GameState.GetPlayerOne().SetBlackAllowed(true);
                    }
                }
                // ball of player two
                else if (ball.GetGroup().Equals(GameState.GetPlayerTwo().GetBallGroup()))
                {
                    GameState.GetPlayerTwo().RemoveBall(ball);
                    // check if this was the last object ball
                    if (GameState.GetPlayerTwo().GetRemainingBalls().Count == 0)
                    {
                        GameState.GetPlayerTwo().AddBall(GameState.GetBallHandler().GetBall(BallTypes.FullBlack));
                        GameState.GetPlayerTwo().SetBlackAllowed(true);
                    }
                }
                // black ball
                else if (ball.GetBallType().Equals(BallTypes.FullBlack))
                {
                    GameState.GetPlayerOne().RemoveBall(GameState.GetBallHandler().GetBall(BallTypes.FullBlack));
                    GameState.GetPlayerTwo().RemoveBall(GameState.GetBallHandler().GetBall(BallTypes.FullBlack));
                }
            }
        }

        /// <summary>
        /// Checks if any Balls on the table are moving or not.
        /// </summary>
        /// <returns>
        /// true if at least one ball is moving, false if no balls are moving.
        /// </returns>
        private bool BallsMoving()
        {
            foreach (var ball in GameState.GetBallHandler().GetBalls())
            {
                if (ball.IsMoving())
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// This method iterates over all foul rules and checks, if a foul occured.
        /// A Foul occured, if a foul rule returned a consequence which is not null.
        /// Then, it executes the severest Consequence. 
        /// </summary>
        private void CheckLastTurnForFouls()
        {
            UnityEngine.Debug.Log("Turn finished. Checking rules ");
            var foulRuleWithConsequenceList = new List<Tuple<IFoulRule, Consequence>>();

            foreach (IFoulRule foulRule in FoulRules)
            {
               var consequence = foulRule.CheckRule(GameState);
               if (consequence != null)
               {
                   foulRuleWithConsequenceList.Add(new Tuple<IFoulRule, Consequence>(foulRule, consequence));
               }
            }
            ExecuteSeverestConsequence(foulRuleWithConsequenceList);
        }

        /// <summary>
        /// This method executes the severest consequence given in the argument and prints the name of the 
        /// consequence to the GUI. If there are no consequences, it only prints an empty string to the GUI. 
        /// </summary>
        /// <param name="foulRuleWithConsequenceList">
        /// A list of tuples containing the foul rule as the first item and the consequence as the second item.
        /// the foul rule is used to be able to print the violation to the GUI.
        /// </param>
        private void ExecuteSeverestConsequence(List<Tuple<IFoulRule, Consequence>> foulRuleWithConsequenceList)
        {
            if (foulRuleWithConsequenceList.Count > 0)
            { 
                var severestConsequenceTuple = foulRuleWithConsequenceList[0];
                foreach (var tuple in foulRuleWithConsequenceList)
                {
                     if (tuple.Item2.GetSeverity() > severestConsequenceTuple.Item2.GetSeverity())
                     {
                          severestConsequenceTuple = tuple;
                     }
                }
                gameOverlay.FoulRuleText = severestConsequenceTuple.Item1.ToString();
                // ResultDisplayText.text = severestConsequenceTuple.Item1.ToString();
                severestConsequenceTuple.Item2.ExecuteConsequence(GameState);
            }
            else
            {
                gameOverlay.FoulRuleText = " ";
            }
        }

        public GameState GetGameState()
        {
            return GameState;
        }
    }
}
