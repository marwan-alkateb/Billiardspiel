using System.Collections.Generic;
using Tracking;
using Tracking.Model;
using UnityEngine;

namespace Logic.GameLogic
{
    public class Player
    {
        /// <summary>
        /// The name of the player.
        /// </summary>
        private readonly string _name;
        /// <summary>
        /// The Group of balls assigned to this player.
        /// </summary>
        private global::Logic.GameLogic.Ball.BallGroup _ballGroup;
        /// <summary>
        /// The remaining balls this player has to pocket.
        /// This does not include the black ball until it is the last ball of this player.
        /// </summary>
        private HashSet<global::Logic.GameLogic.Ball> _remainingBalls;
        /// <summary>
        /// The last pocketEvent where a ball of this players group was pocketed.
        /// </summary>
        private PocketEvent _lastPocketed;
        /// <summary>
        /// True if the black ball is the last ball of this player.
        /// </summary>
        private bool _blackAllowed;

        /// <summary>
        /// This constructor initializes the group with not set.  
        /// </summary>
        public Player()
        {
            _ballGroup = global::Logic.GameLogic.Ball.BallGroup.NotSet;
            _remainingBalls = new HashSet<global::Logic.GameLogic.Ball>();
        }
        
        /// <summary>
        /// This constructor initializes the group with not set.  
        /// </summary>
        public Player(string playerPrefs)
        {
            _ballGroup = global::Logic.GameLogic.Ball.BallGroup.NotSet;
            _name = PlayerPrefs.GetString(playerPrefs);
            _remainingBalls = new HashSet<global::Logic.GameLogic.Ball>();
        }

        /// <summary>
        /// Removes the given ball from the list of remaining balls of this player.
        /// Does nothing if the given ball is not in the list.
        /// </summary>
        /// <param name="ball">
        /// The ball to be removed.
        /// </param>
        public void RemoveBall(global::Logic.GameLogic.Ball ball)
        {
            _remainingBalls.Remove(ball);
        }
        
        /// <summary>
        /// Adds the given ball from the list of remaining balls of this player.
        /// Does nothing if the given ball is already in the list.
        /// </summary>
        /// <param name="ball">
        /// The ball to be added.
        /// </param>
        public void AddBall(global::Logic.GameLogic.Ball ball)
        {
            _remainingBalls.Add(ball);
        }

        // Getter/Setter methods
        
        public string GetName()
        {
            return _name;
        }

        public void SetBallGroup(global::Logic.GameLogic.Ball.BallGroup group)
        {
            _ballGroup = group;
        }

        public global::Logic.GameLogic.Ball.BallGroup GetBallGroup()
        {
            return _ballGroup;
        }

        public HashSet<global::Logic.GameLogic.Ball> GetRemainingBalls()
        {
            return _remainingBalls;
        }

        public PocketEvent GetLastPocketed()
        {
            return _lastPocketed;
        }

        public void SetLastPocketed(PocketEvent lastPocketed)
        {
            _lastPocketed = lastPocketed;
        }

        public bool IsBlackAllowed()
        {
            return _blackAllowed;
        }

        public void SetBlackAllowed(bool allowed)
        {
            _blackAllowed = allowed;
        }
    }
}