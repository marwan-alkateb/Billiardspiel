using Tracking;
using Tracking.Model;
using UnityEngine;

namespace Display.UI.DynamicUI.DynamicUIElements
{
    /// <summary>
    /// An implementation of the <see cref="DynamicUIElement"/> to dynamically render sprites.
    /// </summary>
    public class DynamicSprite : DynamicUIElement
    {
        private SpriteRenderer _sprite;
        private BallType _type;
    
        protected override void InitializeGameObject(ref GameObject gameObject)
        {
            gameObject.name = "DynamicSprite";

            _sprite = gameObject.AddComponent<SpriteRenderer>();
        }
    
        public DynamicSprite(BallType type, Vector2 position = new Vector2(), float rotation = 0, int scale = 8) : base(position, rotation, scale)
        {
            _type = type;
            _sprite.sprite = Resources.Load<Sprite>($"Sprites/{type}");
        }
    }
}
