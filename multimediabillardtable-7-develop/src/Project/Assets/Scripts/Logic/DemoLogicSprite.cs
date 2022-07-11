using System;
using System.Collections.Generic;
using Display.Logic;
using Display.UI.DynamicUI.DynamicUIElements;
using Tracking;
using Tracking.Model;
using UnityEngine;

public class DemoLogicSprite : LogicComponent
{
    private Dictionary<BallType, DynamicSprite> _ballTexts;
    protected override void Init()
    {
        _ballTexts = new Dictionary<BallType, DynamicSprite>();
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
                    _ballTexts.Add(ball.Type, new DynamicSprite(ball.Type));
                    DynamicUIController.RegisterDynamicUIElement(_ballTexts[ball.Type], name);
                }
                _ballTexts[ball.Type].Position = new Vector2(denormalizedBallPositions[ball.Type].x,
                    denormalizedBallPositions[ball.Type].y);
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
