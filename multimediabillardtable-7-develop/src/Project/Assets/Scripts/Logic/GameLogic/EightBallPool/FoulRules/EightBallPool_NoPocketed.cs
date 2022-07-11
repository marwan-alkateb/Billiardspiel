using Logic.GameLogic.Consequences;

namespace Logic.GameLogic.EightBallPool.FoulRules
{
    /// <summary>
    /// This class implements the IFoulRule interface for the foul of no balls being pocketed.
    /// </summary>
    public class EightBallPool_NoPocketed : IFoulRule
    {
        /// <summary>
        /// This is the necessary implementation of the IFoulRule interface.
        /// In this case, it checks if anything was pocketed and if nothing was pocketed,
        /// </summary>
        /// <param name="gameState"></param>
        /// <returns>
        /// null if any ball was pocketed.
        /// A player change consequence, if no ball was pocketed.
        /// </returns>
        public Consequence CheckRule(GameState gameState)
        {
            if (gameState.GetPocketedEvents().Length  == 0)
            {
                UnityEngine.Debug.Log("No ball was pocketed!");
                return new ChangePlayerTurn();
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
            return "No ball was pocketed!";
        }
    }
} 