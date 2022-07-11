using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;

namespace Display.UI.DynamicUI.DynamicUIElements
{
    /// <summary>
    /// An implementation of the <see cref="DynamicUIElement"/> to dynamically render lines.
    /// </summary>
    public class DynamicLine : DynamicUIElement
    {
        /// <summary>
        /// A <see cref="UILineRenderer"/> that's responsible for drawing the line on the canvas
        /// </summary>
        private UILineRendererList _lineRenderer;

        /// <summary>
        /// A property that gets and sets the points of the line.
        /// </summary>
        /// <remarks>
        /// The points are expected to be normalized and will be converted when setting the points.
        /// </remarks>
        public List<Vector2> Points
        {
            get => _lineRenderer.Points;
            set
            {
                List<Vector2> points = new List<Vector2>();
                
                foreach (var point in value)
                {
                    points.Add(UICanvas.NormalizedCoordsToCanvas(point));
                }

                _lineRenderer.Points = points;
            }
        }

        /// <summary>
        /// A property that determines the thickness of the line.
        /// </summary>
        public float Thickness
        {
            get => _lineRenderer.lineThickness;
            set => _lineRenderer.lineThickness = value;
        } 
        
        /// <summary>
        /// The constructor of the DynamicText calls the base and initializes the text string.
        /// </summary>
        /// <param name="id">The id of the DynamicUIElement.</param>
        /// <param name="points">The points of the line.</param>
        /// <param name="position">The position of the DynamicUIElement. (optional)</param>
        /// <param name="rotation">The rotation of the DynamicUIElement. (optional)</param>
        public DynamicLine(List<Vector2> points, Vector2 position = new Vector2(), float rotation = 0) : base(position, rotation)
        {
            Points = points;
        }
        
        /// <summary>
        /// Initializes the <see cref="GameObject"/> of the <see cref="DynamicUIElement"/>
        /// </summary>
        /// <param name="gameObject">The <see cref="GameObject"/> to be initialized.</param>
        protected override void InitializeGameObject(ref GameObject gameObject)
        {
            gameObject.name = "DynamicLine";

            _lineRenderer = gameObject.AddComponent<UILineRendererList>();

            _lineRenderer.lineThickness = 10f;
        }
    }
}