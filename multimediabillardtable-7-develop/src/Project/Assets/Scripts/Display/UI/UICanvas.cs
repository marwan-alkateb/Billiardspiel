using UnityEngine;

namespace Display.UI
{
    /// <summary>
    /// A class that contains information and functions regarding the display canvas.
    /// </summary>
    public class UICanvas : MonoBehaviour
    {
        /// <summary>
        /// A <see cref="RectTransform"/> that contains the RectTransform of the canvas.
        /// </summary>
        /// <remarks>
        /// Set this object by dragging the canvas of the display on to it in the inspector.
        /// </remarks>
        public RectTransform rectTransform;

        /// <summary>
        /// A static version of the <see cref="rectTransform"/>.
        /// </summary>
        private static RectTransform _rectTransform;

        /// <summary>
        /// A property to get the height of the canvas in pixels.
        /// </summary>
        public static float Height => _rectTransform.rect.height;
        
        /// <summary>
        /// A property to get the width of the canvas in pixels.
        /// </summary>
        public static float Width => _rectTransform.rect.width;

        /// <summary>
        /// This function assigns the <see cref="rectTransform"/> to the static <see cref="_rectTransform"/>.
        /// </summary>
        private void Awake()
        {
            _rectTransform = rectTransform;
        }
        
        /// <summary>
        /// This function converts normalized coordinates to coordinates on the display canvas.
        /// </summary>
        /// <param name="coords">The coordinates to be converted as a <see cref="Vector2"/>.</param>
        /// <returns>The converted coordinates as a <see cref="Vector2"/>.</returns>
        public static Vector2 NormalizedCoordsToCanvas(Vector2 coords)
        {
            var pixelRect = _rectTransform.rect;
            return new Vector2(coords.x * pixelRect.width, coords.y * pixelRect.height);
        }
        
        /// <summary>
        /// This function converts coordinates on the display canvas to normalized coordinates.
        /// </summary>
        /// <param name="coords">The coordinates to be converted as a <see cref="Vector2"/>.</param>
        /// <returns>The converted coordinates as a <see cref="Vector2"/>.</returns>
        public static Vector2 CanvasCoordsToNormalized(Vector2 coords)
        {
            var pixelRect = _rectTransform.rect;
            return new Vector2(coords.x / pixelRect.width, coords.y / pixelRect.height);
        }
    }
}