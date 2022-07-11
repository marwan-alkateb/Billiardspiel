using Logic.GameLogic.Consequences;

namespace Logic.GameLogic.TrickshotMode.FoulRules
{
    /// <summary>
    /// This class implements the IFoulRule interface for the event when the player is out of turns.
    /// </summary>
    class TrickshotMode_OutOfTurns : IFoulRule
    {
        public Consequence CheckRule(GameState gameState)
        {
            if (gameState.GetHitsAvailbale() == 0 && gameState.GetQuantityToBePocketed() != 0)
            {
                UnityEngine.Debug.Log("Ran out of turns");

                return new EndGame(false);
            }
            return null;
        }
    }
}
