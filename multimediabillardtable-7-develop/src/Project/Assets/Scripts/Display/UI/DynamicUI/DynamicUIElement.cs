using UnityEngine;

namespace Display.UI.DynamicUI
{
    /// <summary>
    /// A abstract class that all DynamicUIElements need to derive from.
    /// </summary>
    public abstract class DynamicUIElement
    {
        /// <summary>
        /// The id of the DynamicUIElement.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// A property that sets and gets the position of the DynamicUIElement.
        /// </summary>
        public Vector2 Position
        {
            get
            {
                Vector3 position = _gameObject.transform.localPosition;
                return new Vector2(position.x, position.y);
            }
            set => _gameObject.transform.localPosition = new Vector3(value.x, value.y);
        }

        /// <summary>
        /// A property that sets and gets the rotation of the DynamicUIElement.
        /// </summary>
        public float Rotation
        {
            get => _gameObject.transform.localEulerAngles.z;
            set => _gameObject.transform.localEulerAngles = new Vector3(0, 0, value);
        }

        /// <summary>
        /// A property that returns the active state of the DynamicUIElement.
        /// </summary>
        public bool IsActive => _gameObject.activeInHierarchy;
        
        /// <summary>
        /// The <see cref="GameObject"/> that represents the DynamicUIElement in the scene.
        /// </summary>
        private readonly GameObject _gameObject;

        /// <summary>
        /// A property to get the <see cref="_gameObject"/>.
        /// </summary>
        public GameObject GameObject => _gameObject;

        /// <summary>
        /// The constructor creates the <see cref="GameObject"/> and initializes all members.
        /// </summary>
        /// <param name="id">The id of the DynamicUIElement.</param>
        /// <param name="position">The position of the DynamicUIElement. (optional)</param>
        /// <param name="rotation">The rotation of the DynamicUIElement. (optional)</param>
        /// <param name ="scale"> Scale of the DynamicUIElement. (optional)</param>
        protected DynamicUIElement(Vector2 position = new Vector2(), float rotation = 0.0f, int scale = 1)
        {
            ID = -1;
            _gameObject = new GameObject();
            _gameObject.SetActive(false);
            InitializeGameObject(ref _gameObject);
            Position = position;
            Rotation = rotation;
            _gameObject.transform.localScale = new Vector3(scale, scale, 1);
        }

        /// <summary>
        /// Abstract function to initialize the <see cref="GameObject"/>.
        /// </summary>
        /// <param name="gameObject">The <see cref="GameObject"/> to be initialized.</param>
        protected abstract void InitializeGameObject(ref GameObject gameObject);
    }
}