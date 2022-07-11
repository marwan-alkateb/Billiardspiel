using Logic.GameLogic.Consequences;

namespace Logic.GameLogic.TrickshotMode.FoulRules
{
    /// <summary>
    /// This class implements the IFoulRule interface for the event when the player achieved its goal.
    /// </summary>
    public class TrickshotMode_PocketedAllReqiredBalls : IFoulRule
    {
        public Consequence CheckRule(GameState gameState)
        {
            if(gameState.GetQuantityToBePocketed() == 0)
            {
                UnityEngine.Debug.Log("All required balls pocketed");

                return new EndGame(true);
            }
            return null;
        }
    }
}
