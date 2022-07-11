namespace Logic.GameLogic.Util
{
    /// <summary>
    /// This class serves the purpose of having a severity for different fouls, i.e. when the black ball
    /// gets wrongly pocketed in 8Ball, the consequence is EndGame, which has a critical severity.
    /// </summary>
    public class SeverityUtil
    {
        /// <summary>
        /// The severity is used to be able to determine, which consequence will be executed when multiple
        /// fouls occured
        /// . I.e. 8Ball:
        /// if a wrong ball was touched, the consequence is BallInHand, which has a
        /// Medium severity.
        /// If it happens, that in the same turn the black ball was wrongly pocketed, then a EndGame consequence
        /// will be returned, which has a critical severity.
        /// Since the Game will be over, there is no need to execute the BallInHand consequence.  
        /// </summary>
        public enum Severity
        {
            Low = 0,
            Medium,
            Critical
        }
    }
}