using System.Collections.Generic;
using Assets.Scripts;
using Display.Logic;
using Display.UI;
using Display.UI.DynamicUI.DynamicUIElements;
using Tracking;
using Tracking.Model;
using UnityEngine;

namespace Logic
{
    /// <summary>
    /// This class is for demonstration purposes only. If its not used it may be removed.
    /// </summary>
    public class DemoLogicLine : LogicComponent
    {
        private DynamicLine _ballLine;
        private DynamicLine _pocketLine;
        private List<Vector2> _pockets;

        protected override void Init()
        {
            _ballLine = new DynamicLine(new List<Vector2>());
            _pocketLine = new DynamicLine(new List<Vector2>());
            DynamicUIController.RegisterDynamicUIElement(_ballLine, name);
            DynamicUIController.RegisterDynamicUIElement(_pocketLine, name);
            _pockets = new List<Vector2>
            {
                UICanvas.NormalizedCoordsToCanvas(new Vector2(0.0f, 0.0f)),
                UICanvas.NormalizedCoordsToCanvas(new Vector2(0.5f, 0f)),
                UICanvas.NormalizedCoordsToCanvas(new Vector2(1.0f, 0)),
                UICanvas.NormalizedCoordsToCanvas(new Vector2(0.0f, 1.0f)),
                UICanvas.NormalizedCoordsToCanvas(new Vector2(0.5f, 1.0f)),
                UICanvas.NormalizedCoordsToCanvas(new Vector2(1.0f, 1.0f))
            };
            
            Debug.Log("all good ");
        }
        
        public override void LogicUpdate(Tracking.Model.Ball[] balls, Dictionary<BallType, Vector2> denormalizedBallPositions,
            BallCollisionEvent[] ballCollisionEvents, PocketEvent[] pocketEvents, WallCollisionEvent[] wallCollisionEvents)
        {
            Dictionary<BallType, Vector2> ballPositions = new Dictionary<BallType, Vector2>();
            
            foreach (var ball in balls)
            {
                if(ball.OnTable)
                    ballPositions.Add(ball.Type, denormalizedBallPositions[ball.Type]);
            }

            if (ballPositions.ContainsKey(BallTypes.FullWhite) && ballPositions.Count > 1)
            {
                Vector2 whiteBallPosition = ballPositions[BallTypes.FullWhite];
                ballPositions.Remove(BallTypes.FullWhite);

                float smallestDistance = float.MaxValue;
                BallType closestBall = BallTypes.FullWhite;
                
                // find the ball with the smallest distance to the white ball
                foreach (var keyValuePair in ballPositions)
                {
                    float distance = Vector2.Distance(keyValuePair.Value, whiteBallPosition);
                    
                    if (distance < smallestDistance)
                    {
                        smallestDistance = distance;
                        closestBall = keyValuePair.Key;
                    }
                }
                
                _ballLine.Points = new List<Vector2>
                {
                    UICanvas.CanvasCoordsToNormalized(whiteBallPosition), UICanvas.CanvasCoordsToNormalized(ballPositions[closestBall])
                };

                // find the poocket with the smallest distance to the ball with the smallest distance to the whiteball
                smallestDistance = float.MaxValue;
                int index = 0;

                for (int i = 0; i < _pockets.Count; ++i)
                {
                    float distance = Vector2.Distance(ballPositions[closestBall], _pockets[i]);
                    if (distance < smallestDistance)
                    {
                        smallestDistance = distance;
                        index = i;
                    }
                }
                
                _pocketLine.Points = new List<Vector2>
                {
                    UICanvas.CanvasCoordsToNormalized(_pockets[index]), UICanvas.CanvasCoordsToNormalized(ballPositions[closestBall])
                };
                
                if(!_ballLine.IsActive)
                    DynamicUIController.ActivateDynamicUIElement(_ballLine);    
                
                if(!_pocketLine.IsActive)
                    DynamicUIController.ActivateDynamicUIElement(_pocketLine);
            }
            else
            {
                DynamicUIController.DeactivateDynamicUIElement(_ballLine);
                DynamicUIController.DeactivateDynamicUIElement(_pocketLine);
            }
        }
    }
}
