using Logic.GameLogic.Consequences;
using Tracking.Model;

namespace Logic.GameLogic.TrickshotMode.FoulRules
{
    /// <summary>
    /// This class implements the IFoulRule interface for the event when the player hits a wrong pocket.
    /// </summary>
    class TrickshotMode_WrongPocket : IFoulRule
    {
        public Consequence CheckRule(GameState gameState)
        {
            foreach (PocketEvent pocketEvent in gameState.GetPocketedEvents()) {
                if (pocketEvent.Pocket != gameState.GetSelectedPocket())
                {
                    UnityEngine.Debug.Log("pocketed in wrong pocket");

                    return new EndGame(false);
                }
            }
            return null;
        }
    }
}
