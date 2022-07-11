using UnityEngine;
using UnityEngine.UI;

namespace Display.UI.DynamicUI.DynamicUIElements
{
    /// <summary>
    /// An implementation of the <see cref="DynamicUIElement"/> to dynamically render text.
    /// </summary>
    public class DynamicText : DynamicUIElement
    {
        /// <summary>
        /// The text object of the DynamicText
        /// </summary>
        private Text _text;
        
        /// <summary>
        /// The <see cref="RectTransform"/> of the <see cref="GameObject"/>.
        /// </summary>
        private RectTransform _rectTransform;
        
        /// <summary>
        /// A property that sets and gets the text of the <see cref="_text"/> as a string.
        /// </summary>
        /// <remarks>
        /// It also updates the position of the <see cref="DynamicUIElement"/> when setting the text.
        /// </remarks>
        public string Text
        {
            get => _text.text;
            set
            {
                _text.text = value;
                Position = Position;
            }
        }

        /// <summary>
        /// A property that represents the fontsize of the text.
        /// </summary>
        public int Size
        {
            get => _text.fontSize;
            set => _text.fontSize = value;
        }

        /// <summary>
        /// A property that sets and gets the position of the <see cref="GameObject"/>.
        /// </summary>
        /// <remarks>
        /// Setting the position will also check if the text is outside of the display canvas
        /// and force it to stay inside.
        /// </remarks>
        public new Vector2 Position
        {
            get
            {
                Vector3 position = GameObject.transform.localPosition;
                return new Vector2(position.x, position.z);
            }
            set
            {
                if (value.x + (Width / 2) > UICanvas.Width)
                    value.x -= value.x + (Width / 2) - UICanvas.Width;
                
                if (value.y + (Height / 2) > UICanvas.Height)
                    value.y -= value.y + (Height / 2) - UICanvas.Height;
                
                if (value.x - (Width / 2) < 0)
                    value.x += (Width / 2) - value.x;
                
                if (value.y - (Height / 2) < 0)
                    value.y += (Height / 2) - value.y;
                
                GameObject.transform.localPosition = new Vector3(value.x, value.y);
            }
        }

        /// <summary>
        /// A property that gets the height of the text.
        /// </summary>
        public float Height => _rectTransform.rect.height;
        
        /// <summary>
        /// A property that gets the width of the text.
        /// </summary>
        public float Width => _rectTransform.rect.width;

        /// <summary>
        /// The constructor of the DynamicText calls the base and initializes the text string.
        /// </summary>
        /// <param name="id">The id of the DynamicUIElement.</param>
        /// <param name="text">The string that the text is initialized with.</param>
        /// <param name="position">The position of the DynamicUIElement. (optional)</param>
        /// <param name="rotation">The rotation of the DynamicUIElement. (optional)</param>
        public DynamicText(string text, Vector2 position = new Vector2(), float rotation = 0) : base(position, rotation)
        {
            Text = text;
        }

        /// <summary>
        /// Initializes the <see cref="GameObject"/> of the <see cref="DynamicUIElement"/>
        /// </summary>
        /// <param name="gameObject">The <see cref="GameObject"/> to be initialized.</param>
        protected override void InitializeGameObject(ref GameObject gameObject)
        {
            gameObject.name = "DynamicText";

            _text = gameObject.AddComponent<Text>();
            _text.fontSize = 50;
            _text.font = Font.CreateDynamicFontFromOSFont("Arial", 50);
            _text.color = Color.white;

            ContentSizeFitter contentSizeFitter = gameObject.AddComponent<ContentSizeFitter>();

            contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
            contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

            _rectTransform = gameObject.GetComponent<RectTransform>();
            
        }
    }
}