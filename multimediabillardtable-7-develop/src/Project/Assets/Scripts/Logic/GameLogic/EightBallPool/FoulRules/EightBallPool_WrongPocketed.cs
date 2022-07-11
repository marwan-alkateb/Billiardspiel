using Assets.Scripts;
using Logic.GameLogic.Consequences;
using Tracking.Model;

namespace Logic.GameLogic.EightBallPool.FoulRules
{
    /// <summary>
    /// This class implements the IFoulRule interface for the foul of a ball was wrong pocketed.
    /// </summary>
    public class EightBallPool_WrongPocketed : IFoulRule
    {
        /// <summary>
        /// This is the necessary implementation of the IFoulRule interface.
        /// In this case, it checks if a wrong ball was pocketed in this round.
        /// It checks if the white ball was pocketed, black ball was pocketed or only balls
        /// from the wrong group (full/half) was pocketed.
        /// </summary>
        /// <param name="gameState"></param>
        /// <returns>
        /// - BallInHand consequence if the cue ball or at least one ball from the opponents group was pocketed.
        /// - EndGame consequence if the black ball was pocketed.
        /// - null if the pocketed ball was from the current player´s group or no ball was pocketed at all.
        /// </returns>
        public Consequence CheckRule(GameState gameState)
        {
            // If no groups are assigned yet legally pocketing any object-ball other than the black ball is fine
            // and allows the player to continue playing.
            if (gameState.GetPlayerOne().GetBallGroup().Equals(Ball.BallGroup.NotSet))
            {
                bool consequence = false;
                foreach (var pocketedEvent in gameState.GetPocketedEvents())
                {
                    if (pocketedEvent.Ball == BallTypes.FullWhite)
                        consequence = true;
                    else if (pocketedEvent.Ball == BallTypes.FullBlack)
                        return new EndGame(false);
                }
                return consequence ? new BallInHand() : null;
            }

            Logic.GameLogic.Ball.BallGroup currentGroup = gameState.GetCurrentPlayer().GetBallGroup();
            Logic.GameLogic.Ball pocketedBall;

            bool blackPocketed = false;
            bool cuePocketed = false;
            bool opponentsPocketed = false;

            // check for fouls
            foreach (PocketEvent pocketEvent in gameState.GetPocketedEvents())
            {
                pocketedBall = gameState.GetBallHandler().GetBall(pocketEvent.Ball);

                // pocketed cue ball
                if (pocketedBall.GetGroup() == Logic.GameLogic.Ball.BallGroup.White)
                {
                    cuePocketed = true;
                }
                // pocketed black ball
                if (pocketedBall.GetGroup() == Logic.GameLogic.Ball.BallGroup.Black)
                {
                    blackPocketed = true;
                }
                // pocketed opponents ball
                if (!pocketedBall.GetGroup().Equals(currentGroup))
                {
                    opponentsPocketed = true;
                }
            }

            if (blackPocketed && cuePocketed)
            {
                new EndGame(false);
            }
            else if (blackPocketed && !cuePocketed)
            {
                return new EndGame(gameState.GetCurrentPlayer().IsBlackAllowed() && BlackRightPocketed(gameState));
            }
            else
            {
                if (opponentsPocketed || cuePocketed)
                {
                    return new BallInHand();
                }
            }

            // no foul
            return null;
        }

        /// <summary>
        /// Checks if the black ball was pocketed in the opposite pocket of the last pocketed ball.
        /// </summary>
        /// <param name="gameState"></param>
        /// <returns>
        /// true if the black ball was pocketed legally and false if not.
        /// </returns>
        private bool BlackRightPocketed(GameState gameState)
        {
            foreach (var pocketEvent in gameState.GetPocketedEvents())
            {
                if (pocketEvent.Ball.Equals(BallTypes.FullBlack))
                {
                    PocketType lastPocketedPocketType = gameState.GetCurrentPlayer().GetLastPocketed().Pocket;

                    PocketType pocketType = pocketEvent.Pocket;
                    switch (pocketType)
                    {
                        case PocketType.TopLeft:
                            if (lastPocketedPocketType.Equals(PocketType.BottomRight))
                            {
                                return true;
                            }
                            break;
                        case PocketType.TopMid:
                            if (lastPocketedPocketType.Equals(PocketType.BottomMid))
                            {
                                return true;
                            }
                            break;
                        case PocketType.TopRight:
                            if (lastPocketedPocketType.Equals(PocketType.BottomLeft))
                            {
                                return true;
                            }
                            break;
                        case PocketType.BottomLeft:
                            if (lastPocketedPocketType.Equals(PocketType.TopRight))
                            {
                                return true;
                            }
                            break;
                        case PocketType.BottomMid:
                            if (lastPocketedPocketType.Equals(PocketType.TopMid))
                            {
                                return true;
                            }
                            break;
                        case PocketType.BottomRight:
                            if (lastPocketedPocketType.Equals(PocketType.TopLeft))
                            {
                                return true;
                            }
                            break;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// The explanation of this foul.
        /// </summary>
        /// <returns>
        /// A string with the explanation of this foul.
        /// </returns>
        public override string ToString()
        {
            return "Wrong ball was pocketed!";
        }
    }
}
