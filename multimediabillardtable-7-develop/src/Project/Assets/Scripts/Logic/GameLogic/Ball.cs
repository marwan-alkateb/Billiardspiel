using System.Drawing;
using Tracking;
using Tracking.Model;
using UnityEngine;

namespace Logic.GameLogic
{
    /// <summary>
    /// This class contains methods and an enum which are useful to make the determining of fouls easier 
    /// </summary>
    public class Ball
    {
        /// <summary>
        /// An enum used to differentiate balls by their group.
        /// This is also used to assign a group of balls to the players.
        /// </summary>
        public enum BallGroup
        {
            Full,
            Half,
            White,
            Black,
            NotSet
        }

        /// <summary>
        /// The type of this ball.
        /// </summary>
        private readonly BallType _ballType;
        /// <summary>
        /// The group of this ball.
        /// </summary>
        private readonly BallGroup _group;
        /// <summary>
        /// True if the ball is currently moving.
        /// </summary>
        private bool _moving;
        /// <summary>
        /// True if this ball is still on the table and in the game.
        /// </summary>
        private bool _onTable;
        /// <summary>
        /// The current position of this ball.
        /// Item1 is x and Item2 is y.
        /// </summary>
        private PointF _position;
        /// <summary>
        /// Stores the position of this ball during the last 10 frames as a ringbuffer.
        /// </summary>
        private PointF[] _lastPositions = new PointF[10];
        /// <summary>
        /// Iterator for the _lastPositions ringbuffer.
        /// </summary>
        private int _lastPositionsIterator = 0;
        /// <summary>
        /// Used to ensure that within the first frames until the buffer is filled once
        /// the balls are not detected as moving
        /// </summary>
        private int _bufferCounter = 0;
        
        /// <summary>
        /// The constructor for the Ball class.
        /// </summary>
        /// <param name="type">
        /// The ball type to create the ball object from.
        /// </param>
        public Ball(BallType type)
        {
            _ballType = type;
            _moving = false;
            _onTable = false;
            _position = PointF.Empty;

            int intType = type.ToInt();
            
            if (intType == 0)
            {
                _group = BallGroup.White;
            }
            else if (intType <= 7)
            {
                _group = BallGroup.Full;
            }
            else if (intType == 8)
            {
                _group = BallGroup.Black;
            }
            else if (intType <= 15)
            {
                _group = BallGroup.Half;
            }
            else
            {
                _group = BallGroup.NotSet;
            }
        }

        /// <summary>
        /// Adds a new position to the ringbuffer _lastPositions with the last 10 positions of this ball.
        /// The new Position overwrites the oldest position stored in _lastPositions.
        /// </summary>
        /// <param name="position">
        /// The new position to be added.
        /// </param>
        private void AddPosition(PointF position)
        {
            _lastPositions[_lastPositionsIterator] = position;
            _lastPositionsIterator++;
            if (_lastPositionsIterator > 9)
            {
                _lastPositionsIterator = 0;
            }
        }

        /// <summary>
        /// Compares the positions in _lastPositions to determine if this ball is currently moving.
        /// </summary>
        private void CalculateIfMoving()
        {
            // Check if the buffer was filled once
            if (_bufferCounter < _lastPositions.Length)
            {
                _moving = false;
                _bufferCounter++;
                return;
            }

            // Used to indicate the number of decimal places that are checked to see if the positions changed
            const int decimalPlaces = 100000;
            
            PointF pos0 = _lastPositions[0];
            for (int i = 1; i < _lastPositions.Length; i++)
            {
                if ((int)(pos0.X * decimalPlaces) != (int)(_lastPositions[i].X * decimalPlaces) ||
                    (int)(pos0.Y * decimalPlaces) != (int)(_lastPositions[i].Y * decimalPlaces))
                {
                    _moving = true;
                    return;
                }
            }
            _moving = false;
        }
        
        // Getter/Setter methods
        
        public BallType GetBallType()
        {
            return _ballType;
        }
        
        public BallGroup GetGroup()
        {
            return _group;
        }
        
        public bool IsMoving()
        {
            return _moving;
        }

        public bool IsOnTable()
        {
            return _onTable;
        }

        public void SetIsOnTable(bool onTable)
        {
            _onTable = onTable;
        }

        /// <summary>
        /// Returns the number of the ball
        /// </summary>
        /// <returns>number of the ball</returns>
        public int GetNumberOnBall()
        {
            return _ballType.ToInt();
        }
        
        public PointF GetPosition()
        {
            return _position;
        }
        
        /// <summary>
        /// This sets the current position of this ball.
        /// It also adds that position to the _lastPositions buffer and checks if
        /// this ball is currently moving.
        /// </summary>
        /// <param name="position">
        /// The new position of this ball.
        /// </param>
        public void SetPosition(PointF position)
        {
            _position = position;
            AddPosition(position);
            CalculateIfMoving();
        }
    }
}
