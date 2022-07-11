using System;
using Logic.GameLogic.NineBall.FoulRules;
using StaticUI;
using UnityEngine;
using UnityEngine.UI;

namespace Logic.GameLogic.NineBall
{
    public class NineBall : Game
    {
        /// <summary>
        /// This method start the game, by initializing the position of the Balls
        /// (which will be obsolete once 9Ball is implemented by the Simulation group)
        /// and initializing the rules of this game mode.
        /// </summary>
        public override void OnActivate()
        {
            //PositionBalls();
            FoulRules = new IFoulRule[]
            {
               new NineBall_NoPocketed(),
               new NineBall_NoTouch(),
               new NineBall_WrongPocketed(),
               new NineBall_WrongTouch(),
            };
            base.OnActivate();
            GameObject.Find("FirstPlayerNumberBalls").active = false; 
            GameObject.Find("SecondPlayerNumberBalls").active = false; 
            GameObject.Find("SecondPlayerIcon").active = false; 
            GameObject.Find("FirstPlayerIcon").active = false; 
            GameObject.Find("HitsAvailable").active = false; 
            GameObject.Find("BallQuantity").active = false; 
            GameObject.Find("NineBallIcon").active = true; 
            
            
            GameState.SetTwoPlayerGame(true);
            GameState.GameOverlay.SetFirstplayerIcons("NotSet");
        }

        /// <summary>
        /// This method checks the last turn for fouls and completes the turn. Also the turn will be reset after it
        /// was finished.
        /// </summary>
        protected override void ProcessTurn()
        {
            GameState.GameOverlay.SetNineBallIcon(GetNextBallToPocket().GetNumberOnBall());
            base.ProcessTurn();
            GameState.ResetAfterTurn();
        }

        /// <summary>
        /// Position Ball for Nine Ball, only 9 balls needed and positioned diffrently to 8Ball. All other ball are place out of the game.
        /// </summary>
        private void PositionBalls()
        {
            GameObject.Find("fullYellow").transform.position = new Vector3(0.597f, 0.94f, -0.015f); // 1
            GameObject.Find("fullBlue").transform.position = new Vector3(0.6655f, 0.94f, -0.0505f); // 2
            GameObject.Find("fullRed").transform.position = new Vector3(0.6655f, 0.94f, 0.0205f);   // 3
            GameObject.Find("fullPurple").transform.position = new Vector3(0.728f, 0.94f, 0.056f);  // 4
            GameObject.Find("halfYellow").transform.position = new Vector3(0.728f, 0.94f, -0.015f); // 9
            GameObject.Find("fullOrange").transform.position = new Vector3(0.728f, 0.94f, -0.086f); // 5 
            GameObject.Find("fullGreen").transform.position = new Vector3(0.791f, 0.94f, -0.0505f); // 6
            GameObject.Find("fullBrown").transform.position = new Vector3(0.791f, 0.94f, 0.0205f);  // 7
            GameObject.Find("fullBlack").transform.position = new Vector3(0.859f, 0.94f, -0.015f);  // 8

            // Ball not needed
            GameObject.Find("halfBlue").transform.position = new Vector3(-1.75f, 0.75f, -0.5f);
            GameObject.Find("halfRed").transform.position = new Vector3(-1.75f, 0.75f, -0.4f);
            GameObject.Find("halfPurple").transform.position = new Vector3(-1.75f, 0.75f, -0.3f);
            GameObject.Find("halfOrange").transform.position = new Vector3(-1.75f, 0.75f, -0.2f);
            GameObject.Find("halfGreen").transform.position = new Vector3(-1.75f, 0.75f, -0.1f);
            GameObject.Find("halfBrown").transform.position = new Vector3(-1.75f, 0.75f, 0.0f);

        }

        /// <summary>
        /// Returns the ball with the smallest number on it that is on the table
        /// </summary>
        /// <returns></returns>
        private Ball GetNextBallToPocket()
        {
            Ball[] balls = GameState.GetBallHandler().GetInGameBalls();
            Ball smallest = null;
            for (int i = 0; i < balls.Length; i++)
            {
                if (balls[i].GetNumberOnBall() != 0)
                {
                    if (smallest == null || balls[i].GetNumberOnBall() < smallest.GetNumberOnBall())
                    {
                        smallest = balls[i];
                    }
                }
            }
            //Debug.Log($"SMALLEST BALL: {smallest.GetNumberOnBall().ToString()}");

            return smallest;
        }

    }
}