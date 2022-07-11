using System;
using System.Collections.Generic;
using Display.Logic;
using Display.UI.DynamicUI.DynamicUIElements;
using Tracking;
using Tracking.Model;
using UnityEngine;

namespace Logic
{
    /// <summary>
    /// This class is for demonstration purposes only. If its not used it may be removed.
    /// </summary>
    public class DemoLogicText : LogicComponent
    {
        private Dictionary<BallType, DynamicText> _ballTexts;
        protected override void Init()
        {
            _ballTexts = new Dictionary<BallType, DynamicText>();
        }
        
        public override void LogicUpdate(Tracking.Model.Ball[] balls, Dictionary<BallType, Vector2> denormalizedBallPositions,
            BallCollisionEvent[] ballCollisionEvents, PocketEvent[] pocketEvents, WallCollisionEvent[] wallCollisionEvents)
        {
            foreach (var ball in balls)
            {
                if (ball.OnTable)
                {
                    if (!_ballTexts.ContainsKey(ball.Type))
                    {
                        _ballTexts.Add(ball.Type, new DynamicText(""));
                        DynamicUIController.RegisterDynamicUIElement(_ballTexts[ball.Type], name);
                    }

                    string textString = "{ " + Math.Round(ball.Position.X, 3).ToString("0.000") + " | " + Math.Round(ball.Position.Y, 3).ToString("0.000") + " } " + ball.Type.ToString();                           
                    _ballTexts[ball.Type].Text = textString;
                    _ballTexts[ball.Type].Position = new Vector2(denormalizedBallPositions[ball.Type].x,
                        denormalizedBallPositions[ball.Type].y + 65);
                }
                else
                {
                    if (_ballTexts.ContainsKey(ball.Type))
                    {
                        DynamicUIController.DestroyDynamicUIElement(_ballTexts[ball.Type]);
                        _ballTexts.Remove(ball.Type);
                    }
                }
            }
        }
    }
}
