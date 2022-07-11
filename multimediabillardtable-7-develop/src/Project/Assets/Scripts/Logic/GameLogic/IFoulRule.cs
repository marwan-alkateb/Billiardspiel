using Logic.GameLogic.Consequences;

namespace Logic.GameLogic
{
    /// <summary>
    /// This is an interface for all foul rules of any game mode. If you add a new foul rule, it needs to implement
    /// at least the CheckRule() method.
    /// </summary>
    public interface IFoulRule
    {
        /// <summary>
        /// In this method you should implement the logic of the rule that you created.
        /// </summary>
        /// <param name="gameState">
        /// the current state of the Game, which contains information about collisions that happened, which balls
        /// are on the table etc.
        /// </param>
        /// <returns>
        /// If you detected a violation, you should return the corresponding Consequence e.g. BallInHand, EndGame, etc.
        /// If there was no violation detected, you can return null.
        /// </returns>
        Consequence CheckRule(GameState gameState);
    }
}