using System.Collections.Generic;
using Assets.Scripts;
using Display.Logic;
using Display.UI.StaticUI;
using Logic.GameLogic.Util;
using StaticUI;
using Tracking.Model;
using UnityEngine;

namespace Logic.GameLogic
{
    /// <summary>
    /// This container class stores all information about the current gamestate which can be relevant.
    /// While a turn is being played tracking information is also accumulated here to be collectively read after the turn.
    /// </summary>
    public class GameState
    {
        /// <summary>
        /// Handles all the balls.
        /// </summary>
        private readonly BallHandler _ballHandler = new BallHandler();
        /// <summary>
        /// Accumulates all Events of a ball being pocketed since the last turn.
        /// Gets emptied after every turn.
        /// </summary>
        private List<PocketEvent> _pocketedEvents = new List<PocketEvent>();
        /// <summary>
        /// Accumulates all Events of a ball hitting another ball since the last turn.
        /// Gets emptied after every turn.
        /// </summary>
        private List<BallCollisionEvent> _ballCollisions = new List<BallCollisionEvent>();
        /// <summary>
        /// Accumulates all Events of a ball hitting a wall since the last turn.
        /// Gets emptied after every turn.
        /// </summary>
        private List<WallCollisionEvent> _wallCollisions = new List<WallCollisionEvent>();
        /// <summary>
        /// If this is true than it´s the first player´s turn. If it´s false it´s the second player´s turn. 
        /// </summary>
        private Player _playerOne;
        /// <summary>
        /// One of the players participating in this match.
        /// </summary>
        private Player _playerTwo;
        /// <summary>
        /// This bool describes whether the white ball can be moved, which happens after/while the BallInHand
        /// Consequence was executed. 
        /// </summary>
        private bool _playerOneActive;
        /// <summary>
        /// One of the players participating in this match.
        /// </summary>
        private bool _freeWhiteBall = false;
        /// <summary>
        /// True for the first turn of a game.
        /// </summary>
        private bool _breakShot = true;
        ///<summary>
        /// Variables only needed for trickshot mode
        /// number of hits available before the game ends
        ///</summary>
        private int _hitsAvailable;
        ///<summary>
        /// number of balls to be pocketed
        ///</summary>
        private int _quantityToBePocketed;
        ///<summary>
        /// pocket in which the balls need to be shot
        ///</summary>
        private PocketType _selectedPocket;
        /// <summary>
        /// GameObject of the white Ball to get the shoot count
        /// </summary>
        private GameObject _fullWhite;
        /// <summary>
        /// An instance of the TouchControl class used for the White Ball
        /// </summary>
        private TouchControl _touchControl;
        ///<summary>
        /// if the current game is a two player game
        ///</summary>
        private bool _twoPlayerGame;

        /// <summary>
        /// Contains the <see cref="StaticUIComponent"/> for the game overlay
        /// </summary>
        public readonly GameOverlay GameOverlay;

        /// <summary>
        /// Contains the <see cref="StaticUIComponent"/> for moving the white ball
        /// </summary>
        public readonly MoveWhiteBallOverlay MoveWhiteBallOverlay;

        /// <summary>
        /// The <see cref="LogicController"/> of the <see cref="Game"/>
        /// </summary>
        public readonly LogicController LogicController;

        /// <summary>
        /// A property to set the color of the current player
        /// </summary>
        public Color CurrentPlayerColor
        {
            get => _playerOneActive ? GameOverlay.FirstPlayerColor : GameOverlay.SecondPlayerColor;
            set
            {
                if (_playerOneActive)
                {
                    GameOverlay.FirstPlayerColor = value;
                }
                else
                {
                    GameOverlay.SecondPlayerColor = value;
                }
            }
        }

        /// <summary>
        /// A property to set the color of the currently not playing player
        /// </summary>
        public Color OtherPlayerColor
        {
            get => _playerOneActive ? GameOverlay.SecondPlayerColor : GameOverlay.FirstPlayerColor;
            set
            {
                if (_playerOneActive)
                {
                    GameOverlay.SecondPlayerColor = value;
                }
                else
                {
                    GameOverlay.FirstPlayerColor = value;
                }
            }
        }

        /// <summary>
        /// Initiales the current game state at the beginning of a game.
        /// </summary>
        /// <param name="gameOverlay"></param>
        /// <param name="moveWhiteBallOverlay"></param>
        /// <param name="logicController"></param>
        public GameState(GameOverlay gameOverlay, MoveWhiteBallOverlay moveWhiteBallOverlay, LogicController logicController)
        {
            LogicController = logicController;
            GameOverlay = gameOverlay;
            MoveWhiteBallOverlay = moveWhiteBallOverlay;
            _playerOne = new Player("firstName");
            _playerTwo = new Player("secondName");
            _fullWhite = GameObject.Find("fullWhite");
            _touchControl = _fullWhite.GetComponent<TouchControl>();
            _touchControl.resetShootCount();
        }

        ///<summary>
        /// Initializes the variables with the values saved in the payer preferences
        ///</summary>
        public void InitializeTrickshotVariables()
        {
            _hitsAvailable = PlayerPrefs.GetInt("hitsAvailable");
            _quantityToBePocketed = PlayerPrefs.GetInt("ballQuantity");
            SetSelectedPocket();
        }

        ///<summary>
        /// Sets selected pocket based on value from the player prefernces
        ///</summary>
        private void SetSelectedPocket()
        {
            int pocket = PlayerPrefs.GetInt("selectedPocket");
            UnityEngine.Debug.Log("Pocket Selected is: " + pocket);
            switch (pocket)
            {
                case 0:
                    _selectedPocket = PocketType.TopLeft;
                    break;
                case 1:
                    _selectedPocket = PocketType.TopMid;
                    break;
                case 2:
                    _selectedPocket = PocketType.TopRight;
                    break;
                case 3:
                    _selectedPocket = PocketType.BottomLeft;
                    break;
                case 4:
                    _selectedPocket = PocketType.BottomMid;
                    break;
                case 5:
                    _selectedPocket = PocketType.BottomRight;
                    break;
            }
        }

        /// <summary>
        /// Returns the last PocketEvent in _pocketedEvents that was a ball of the current players group
        /// or a new and empty PocketEvent object if no ball of the current players group was pocketed.
        /// </summary>
        public PocketEvent GetLastOfGroupPocketed()
        {
            int indexLastPocketed = _pocketedEvents.FindLastIndex(PocketEventHasBallGroupOfCurrentPlayer);
            return indexLastPocketed == -1 ? new PocketEvent() : _pocketedEvents[indexLastPocketed];
        }

        /// <summary>
        /// Determines if the given pocketEvent has a ball of the current players group.
        /// </summary>
        /// <param name="pocketEvent">
        /// A pocketEvent in question.
        /// </param>
        /// <returns>
        /// True if the given pocketEvent has a ball of the current players group.
        /// False if not.
        /// </returns>
        private bool PocketEventHasBallGroupOfCurrentPlayer(PocketEvent pocketEvent)
        {
            return _ballHandler.GetBall(pocketEvent.Ball).GetGroup().Equals(GetCurrentPlayer().GetBallGroup());
        }

        /// <summary>
        /// This method must be executed after a turn ends and is processed.
        /// It resets the turn specific information of the GameState.
        /// </summary>
        public void ResetAfterTurn()
        {
            _pocketedEvents.Clear();
            _ballCollisions.Clear();
            _wallCollisions.Clear();
        }

        /// <summary>
        /// Switches who is currently playing.
        /// </summary>
        public void SwitchCurrentPlayer()
        {
            _playerOneActive = !_playerOneActive;
        }

        // Getter/Setter methods

        public BallHandler GetBallHandler()
        {
            return _ballHandler;
        }

        public PocketEvent[] GetPocketedEvents()
        {
            return _pocketedEvents.ToArray();
        }

        public void AddPocketEvent(PocketEvent pocketEvent)
        {
            _pocketedEvents.Add(pocketEvent);
        }

        public BallCollisionEvent[] GetBallCollisions()
        {
            return _ballCollisions.ToArray();
        }

        /// <summary>
        /// Detects if the cue ball hit another ball this turn.
        /// </summary>
        /// <returns>
        /// True if the cue ball hit an object ball this turn. Otherwise false.
        /// </returns>
        public bool DidCueBallCollide()
        {
            foreach (var ballCollision in _ballCollisions)
            {
                if (ballCollision.BallA.Equals(BallTypes.FullWhite) || ballCollision.BallB.Equals(BallTypes.FullWhite))
                {
                    return true;
                }
            }
            return false;
        }


        public void AddBallCollision(BallCollisionEvent ballCollision)
        {
            _ballCollisions.Add(ballCollision);
        }

        public WallCollisionEvent[] GetWallCollisions()
        {
            return _wallCollisions.ToArray();
        }

        /// <summary>
        /// Creates a list with all wall-collisions of the current turn
        /// excluding the one where the cue ball hit a wall.
        /// </summary>
        /// <returns>
        /// An Array of wall-collisions without any cue ball wall-collisions.
        /// </returns>
        public WallCollisionEvent[] GetWallCollisionsWithoutCueBall()
        {
            var collisions = new List<WallCollisionEvent>(_wallCollisions);

            foreach (var collision in new List<WallCollisionEvent>(collisions))
            {
                if (collision.Ball.Equals(BallTypes.FullWhite))
                {
                    collisions.Remove(collision);
                }
            }

            return collisions.ToArray();
        }

        public void AddWallCollision(WallCollisionEvent wallCollision)
        {
            _wallCollisions.Add(wallCollision);
        }

        public Player GetPlayerOne()
        {
            return _playerOne;
        }

        public Player GetPlayerTwo()
        {
            return _playerTwo;
        }

        public Player GetCurrentPlayer()
        {
            return _playerOneActive ? _playerOne : _playerTwo;
        }

        public void SetCurrentPlayer(Player currentPlayer)
        {
            _playerOneActive = currentPlayer.Equals(_playerOne);
        }

        public Player GetOtherPlayer()
        {
            return _playerOneActive ? _playerTwo : _playerOne;
        }

        public bool GetFreeWhiteBall()
        {
            return _freeWhiteBall;
        }

        public void SetFreeWhiteBall(bool freeWhiteBall)
        {
            _freeWhiteBall = freeWhiteBall;
        }

        public bool IsBreakShot()
        {
            return _breakShot;
        }

        public void SetBreakShot(bool breakShot)
        {
            _breakShot = breakShot;
        }

        public int GetHitsAvailbale()
        {
            return _hitsAvailable;
        }

        /// <summary>
        /// Checks the number of available hits and compares it to the actual shots made.  
        /// </summary>

        public void CheckHitsAvailable()
        {
            int shootCount = _touchControl.getShootCount();
            if (_hitsAvailable == shootCount)
            {
                _hitsAvailable = 0;
            }
        }

        public int GetQuantityToBePocketed()
        {
            return _quantityToBePocketed;
        }

        /// <summary>
        /// Reduces the number of the pocketed balls by the number of balls that were pocketed this turn excluding the cue ball.
        /// </summary>
        public void ReduceQuantityToBePocketed()
        {
            int pocketedThisTurn =0;

            // subtract cue ball from number of pocketed balls, if it is in the list
            foreach (PocketEvent pocketEvent in _pocketedEvents)
            {
                if (pocketEvent.Pocket == _selectedPocket && pocketEvent.Ball != BallTypes.FullWhite)
                {
                   pocketedThisTurn++;
                }
            }

            _quantityToBePocketed -= pocketedThisTurn;
        }

        public PocketType GetSelectedPocket()
        {
            return _selectedPocket;
        }

        public bool GetTwoPlayerGame()
        {
            return _twoPlayerGame;
        }

        public void SetTwoPlayerGame(bool twoPlayerGame)
        {
            _twoPlayerGame = twoPlayerGame;
        }

        public TouchControl GetTouchControl()
        {
           return _touchControl;
        }
    }
}
