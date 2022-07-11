using Logic.GameLogic.Consequences;

namespace Logic.GameLogic.NineBall.FoulRules
{
    /// <summary>
    /// This class implements the IFoulRule interface for the foul of no balls being touched.
    /// </summary>
    public class NineBall_NoTouch : IFoulRule
    {
        /// <summary>
        /// This is the necessary implementation of the IFoulRule interface.
        /// In this case, it checks if any ball was touched.
        /// </summary>
        /// <param name="gameState"></param>
        /// <returns>
        /// A BallInHand Consequence, if no ball was touched or null if there was no violation
        /// </returns>
        public Consequence CheckRule(GameState gameState)
        {
            if (gameState.GetBallCollisions().Length == 0 || !gameState.DidCueBallCollide())
            {
                UnityEngine.Debug.Log("9Ball: No ball was hit!");
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