using Logic.GameLogic.Util;

namespace Logic.GameLogic.Consequences
{
    /// <summary>
    /// This abstract class describes a consequence, that will be returned by a foul rule when it is violated.
    /// </summary>
    public abstract class Consequence
    {
        /// <summary>
        /// The severity should be:
        /// critical, if the game ends
        /// medium, if the other player should be able to move the white ball freely
        /// low, if you only need to change the turn
        /// </summary>
        protected SeverityUtil.Severity Severity;

        public SeverityUtil.Severity GetSeverity()
        {
            return Severity;
        }
        /// <summary>
        /// In this method the actual logic of a Consequence should be implemented.
        /// </summary>
        /// <param name="gameState">
        /// the current state of the Game, which contains information about collisions that happened, which balls
        /// are on the table etc.
        /// </param>
        public abstract void ExecuteConsequence(GameState gameState);
    }
}