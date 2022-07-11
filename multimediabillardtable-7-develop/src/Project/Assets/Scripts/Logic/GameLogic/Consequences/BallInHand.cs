using System.Collections.Generic;
using Logic.GameLogic.Util;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Logic.GameLogic.Consequences
{
    /// <summary>
    /// This class implements the foul consequence, that allows the player to move the white ball freely.
    /// </summary>
    public class BallInHand : Consequence
    {
        /// <summary>
        /// The Transform of the "Balls" object in the unity project
        /// which stores all the individual ball objects.
        /// </summary>
        private readonly Transform _ballsObjectTransform;
        
        // /// <summary>
        // /// The GameObject that tells the player to move the cue ball freely.
        // /// </summary>
        // private readonly GameObject _moveCueBallGUI;

        /// <summary>
        /// This is the Constructor, which sets the severity of of this consequence on medium
        /// and assigns the _ballsObject.
        /// </summary>
        public BallInHand()
        {
            Severity = SeverityUtil.Severity.Medium;
            
            foreach (var rootGameObject in SceneManager.GetActiveScene().GetRootGameObjects())
            {
                if (rootGameObject.gameObject.name == "Balls")
                {
                    _ballsObjectTransform = rootGameObject.gameObject.transform;
                }
            }
        }

        /// <summary>
        /// This method executes the actual consequence.
        /// The first thing that happens is a player change, so the opponent of the player which caused the foul
        /// gets the "reward".
        /// Than the "MoveWhiteBall" GameObject gets set to active, which means it will be
        /// visible on the screen (Its the Screen which contains the Resume Button).
        /// After that, the checking of foul rules will be deactivated by setting the _freeWhiteBall member of the
        /// GameState on true ( true in this case means that the white ball is being moved freely).
        /// All other balls freeze until the Player presses the resume button .
        /// </summary>
        /// <param name="gameState">
        /// is the current State of the Game
        /// </param>
        public override void ExecuteConsequence(GameState gameState)
        {
            UnityEngine.Debug.Log("Move White freely");
            new ChangePlayerTurn().ExecuteConsequence(gameState);
            
            gameState.LogicController.staticUIController.SwitchActiveUI(gameState.MoveWhiteBallOverlay.name);


            gameState.SetFreeWhiteBall(true);
            FreezeBalls(GetBallRigidbodies());
            
            gameState.MoveWhiteBallOverlay.ResumeButtonClicked = new Button.ButtonClickedEvent();
            gameState.MoveWhiteBallOverlay.ResumeButtonClicked.AddListener(delegate { ResumeOnClick(gameState); });
        }

        /// <summary>
        /// Returns the Rigidbody of every ball.
        /// </summary>
        /// <param name="ballsObject">
        /// The unity-object that stores all balls.
        /// </param>
        /// <returns>
        /// A List with the Rigidbodies of every ball.
        /// </returns>
        private List<Rigidbody> GetBallRigidbodies()
        {
            List<Rigidbody> balls = new List<Rigidbody>();
            for (int i = 0; i < 16; i++)
            {
                balls.Add(_ballsObjectTransform.GetChild(i).GetComponent<Rigidbody>());
            }
            return balls;
        }

        /// <summary>
        /// This method freezes all Rigidbodies of the parameter.
        /// As this method also servers as a helper Function, it is used to freeze the balls
        /// while the "MoveWhiteBall" GameObject (The screen with the resume button) is being shown.
        /// For the cue ball it only freezes the rotation .
        /// </summary>
        /// <param name="balls">
        /// A List of Rigidbodies, in this context the Rigidbodies of all existing Balls
        /// </param>
        private void FreezeBalls(List<Rigidbody> balls)
        {
            foreach (var ball in balls)
            {
                ball.constraints = 
                    ball.name == "fullWhite" ? RigidbodyConstraints.FreezeRotation : RigidbodyConstraints.FreezeAll;
            }
        }
        
        /// <summary>
        /// This method unfreezes all Rigidbodies of the parameter.
        /// As this method also servers as a helper Function, it is used to unfreeze the balls
        /// after the Resume button is being pressed
        /// </summary>
        /// <param name="balls">
        /// A List of Rigidbodies, in this context the Rigidbodies of all existing Balls
        /// </param>
        private void UnfreezeBalls(List<Rigidbody> balls)
        {
            foreach (var ball in balls)
            {
                ball.constraints = RigidbodyConstraints.None;
            }
        }

        /// <summary>
        /// This is the callback method for the Resume button. It will be called, when the Resume button is pressed.
        /// It unfreezes all balls, makes them sleep again, allows the Game to start again the rule checking and
        /// turns the "MoveWhiteBall" GameObject (the screen that has the Resume button in it) off (makes it
        /// invisible again).
        /// </summary>
        /// <param name="moveWhiteBallComponent">
        /// The screen with the Resume Button on it
        /// </param>
        /// <param name="gameState">
        /// The current GameState
        /// </param>
        /// <param name="balls">
        ///  A List of Rigidbodies, in this context the Rigidbodies of all existing Balls
        /// </param>
        private void ResumeOnClick(GameState gameState)
        {
            UnfreezeBalls(GetBallRigidbodies());
            gameState.SetFreeWhiteBall(false);
            gameState.LogicController.staticUIController.SwitchActiveUI(gameState.GameOverlay.name);
        }
    }
}