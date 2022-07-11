using Assets.Scripts;
using System.Collections.Generic;
using Tracking;
using Tracking.Model;

namespace Logic.GameLogic.Util
{
    /// <summary>
    /// This class is used to ensure that there is only ball-object for each ball.
    /// </summary>
    public class BallHandler
    {
        /// <summary>
        /// This stores the 16 ball objects that will be used for the game-logic.
        /// </summary>
        private readonly Logic.GameLogic.Ball[] _balls = new Logic.GameLogic.Ball[16];

        /// <summary>
        /// The constructor that creates an object for each ball.
        /// </summary>
        public BallHandler()
        {
            for (int i = 0; i < _balls.Length; i++)
            {
                _balls[i] = new Ball(BallType.FromInt(i));
            }
        }

        // Getter/Setter methods

        public Logic.GameLogic.Ball[] GetBalls()
        {
            return _balls;
        }

        /// <summary>
        /// Returns the balls that are still in the game.
        /// </summary>
        /// <returns>
        /// An array with all balls that are still in the game.
        /// </returns>
        public Logic.GameLogic.Ball[] GetInGameBalls()
        {
            var balls = new List<Logic.GameLogic.Ball>();
            foreach (var ball in _balls)
            {
                if (ball.IsOnTable())
                {
                    balls.Add(ball);
                }
            }
            return balls.ToArray();
        }

        /// <summary>
        /// Returns a specific ball.
        /// </summary>
        /// <param name="type">
        /// The type of ball to be returned.
        /// </param>
        /// <returns>
        /// The ball object with the requested ball-type.
        /// </returns>
        public Logic.GameLogic.Ball GetBall(BallType type)
        {
            foreach (var ball in _balls)
            {
                if (ball.GetBallType().Equals(type))
                {
                    return ball;
                }
            }
            return null;
        }
    }
}