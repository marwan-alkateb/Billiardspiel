using Assets.Scripts;
using Logic.GameLogic.Consequences;
using Tracking;
using Tracking.Model;

namespace Logic.GameLogic.NineBall.FoulRules
{
    /// <summary>
    /// This class implements the IFoulRule interface for the foul of balls being wrongly touched.
    /// </summary>
    public class NineBall_WrongTouch : IFoulRule
    {
        /// <summary>
        /// This is the necessary implementation of the IFoulRule interface.
        /// In this case, it checks if the white ball hit a wrong ball first.
        /// a wrong hit is considered to be a collision between the white ball and a ball that has a greater number
        /// than the current ball that needs to be hit.
        /// </summary>
        /// <param name="gameState"></param>
        /// <returns>
        /// A BallInHand consequence, if the foul was detected or null, if the foul was not detected 
        /// </returns>
        public Consequence CheckRule(GameState gameState)
        {
            // No need to check this rule if no ball was hit
            if (gameState.GetBallCollisions().Length == 0 || !gameState.DidCueBallCollide())
            {
                //UnityEngine.Debug.Log("9Ball: No ball was hit!");
                return null;
            }
            var sortedBallsOnTable = gameState.GetBallHandler().GetInGameBalls();

            Logic.GameLogic.Ball smallestBall = sortedBallsOnTable[1];

            BallType collisionBallType = gameState.GetBallCollisions()[0].BallA == BallTypes.FullWhite
                ? gameState.GetBallCollisions()[0].BallB
                : gameState.GetBallCollisions()[0].BallA;

            for (int i = 1; i < sortedBallsOnTable.LongLength; i++)
            {
                if ((sortedBallsOnTable[i].GetBallType() < smallestBall.GetBallType()))
                {
                    smallestBall = sortedBallsOnTable[i];
                }
            }

            // Case: Correct Ball was pocketed -> is no longer listed in sortedBallOnTable
            // Bacause of that it compares if the collision ball is smaller than the balls on the table
            // => correct ball was pocketed and no consequence will be executed 
            if(smallestBall.GetBallType() > collisionBallType)
            {
                return null;
            }

            return smallestBall.GetBallType() == collisionBallType ? null : new BallInHand();
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