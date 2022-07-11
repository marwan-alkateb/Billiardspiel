using Logic.GameLogic.Consequences;

namespace Logic.GameLogic.EightBallPool.FoulRules
{
    /// <summary>
    /// This class implements the IFoulRule interface for the foul of no balls being touched.
    /// </summary>
    public class EightBallPool_NoTouch : IFoulRule
    {
        /// <summary>
        /// This is the necessary implementation of the IFoulRule interface.
        /// In this case, it checks if no ball was touched.
        /// </summary>
        /// <param name="gameState"></param>
        /// <returns>
        /// A BallInHand consequence, if the foul was detected or null, if the foul was not detected 
        /// </returns>
        public Consequence CheckRule(GameState gameState)
        {
            if (gameState.GetBallCollisions().Length  == 0 || !gameState.DidCueBallCollide())
            {
                UnityEngine.Debug.Log("No ball was hit!");
                return new BallInHand();
            }
            return null;
        }
        
        /// <summary>
        /// The explanation of this foul.
        /// </summary>
        /// <returns>
        /// A string with the explanation of this foul.
        /// </returns>
        public override string ToString()
        {
            return "No ball was touched!";
        }
    }
}
