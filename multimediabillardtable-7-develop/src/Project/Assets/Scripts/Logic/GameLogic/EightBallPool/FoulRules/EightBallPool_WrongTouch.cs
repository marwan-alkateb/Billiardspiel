using Logic.GameLogic.Consequences;
using Tracking;
using Tracking.Model;

namespace Logic.GameLogic.EightBallPool.FoulRules
{
    /// <summary>
    /// This class implements the IFoulRule interface for the foul of a ball that was wrongly touched.
    /// </summary>
    public class EightBallPool_WrongTouch : IFoulRule
    {
        /// <summary>
        /// This is the necessary implementation of the IFoulRule interface.
        /// In this case, it checks if the white ball hit a wrong ball first.
        /// a wrong hit is considered to be a collision between the white ball and a ball which the current player
        /// is not allowed to hit.
        /// </summary>
        /// <param name="gameState"></param>
        /// <returns>
        /// A BallInHand consequence, if the foul was detected or null, if the foul was not detected 
        /// </returns>
        public Consequence CheckRule(GameState gameState)
        {
            var correct = false;
            
            if (gameState.GetBallCollisions().Length > 0)
            {
                if (gameState.GetCurrentPlayer().GetBallGroup() == Ball.BallGroup.NotSet)
                { 
                    if(!gameState.GetBallCollisions()[0].Contains(BallTypes.FullBlack))
                    {
                        correct = true;
                    }
                }
                else
                {
                    //collisionEvent[0] is the first collision of the turn
                    correct = CheckIfTurnWasCorrect(gameState, gameState.GetBallCollisions()[0]);
                }
            }
            else if(gameState.GetBallCollisions().Length  == 0)
            {
                //Case: No Ball was touched, it doesn´t violates this rule. 
                correct = true;
            }

            if (!correct)
            {
                UnityEngine.Debug.Log("WrongTouch ");
            }
            
            return correct ? null : new BallInHand();
        }

        /// <summary>
        /// This method checks if the turn was correct by checking if the group of the collider ball equals the group
        /// of the balls that the current player is allowed to hit.
        /// </summary>
        /// <param name="gameState">
        /// The current state of the game.
        /// </param>
        /// <param name="collisionEvent">
        /// A collision event, which contains 2 balls.
        /// This should be the first collision of the turn i.e. the first collision of the cue ball.
        /// </param>
        /// <returns>
        /// Returns true if the turn was correct otherwise false.
        /// </returns>
        private bool CheckIfTurnWasCorrect(GameState gameState, BallCollisionEvent collisionEvent)
        {
            Logic.GameLogic.Ball.BallGroup firstBallTouched;
            // Check for cue ball
            if (collisionEvent.BallA.Equals(BallTypes.FullWhite))
            {
                firstBallTouched = gameState.GetBallHandler().GetBall(collisionEvent.BallB).GetGroup();
            }
            else if (collisionEvent.BallB.Equals(BallTypes.FullWhite))
            {
                firstBallTouched = gameState.GetBallHandler().GetBall(collisionEvent.BallA).GetGroup();
            }
            else
            {
                UnityEngine.Debug.Log("The first ball that hits another one wasn´t the Cue Ball");
                return false;
            }
            
            // Check for current players group
            if (firstBallTouched.Equals(gameState.GetCurrentPlayer().GetBallGroup()))
            {
                return true;
            }

            // Check for black ball if allowed
            if (firstBallTouched.Equals(Logic.GameLogic.Ball.BallGroup.Black) && gameState.GetCurrentPlayer().IsBlackAllowed())
            {
                return true;
            }
            
            return false;
        }

        /// <summary>
        /// The explanation of this foul.
        /// </summary>
        /// <returns>
        /// A string with the explanation of this foul.
        /// </returns>
        public override string ToString()
        {
            return "Wrong ball was touched!";
        }
    }
}
