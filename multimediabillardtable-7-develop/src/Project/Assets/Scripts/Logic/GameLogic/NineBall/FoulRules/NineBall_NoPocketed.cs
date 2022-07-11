using Logic.GameLogic.Consequences;

namespace Logic.GameLogic.NineBall.FoulRules
{
    /// <summary>
    /// This class implements the IFoulRule interface for the foul of no balls being pocketed.
    /// </summary>
    public class NineBall_NoPocketed : IFoulRule
    {
        /// <summary>
        /// This is the necessary implementation of the IFoulRule interface.
        /// In this case, it checks if anything was pocketed.
        /// </summary>
        /// <param name="gameState"></param>
        /// <returns>
        /// A player change consequence, if the foul was detected or null, if the foul was not detected 
        /// </returns>
        public Consequence CheckRule(GameState gameState)
        {
            if (gameState.GetPocketedEvents().Length == 0)
            {
                UnityEngine.Debug.Log("9Ball: No ball was pocketed!");
                    
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