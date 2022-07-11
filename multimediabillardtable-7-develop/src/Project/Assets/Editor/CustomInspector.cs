using UnityEditor;
using UnityEngine;

namespace Tracking.Unity
{
    public class CustomInspector<T> : Editor where T : MonoBehaviour
    {
        private T _target;

        public T ScriptBehaviour
        {
            get
            {
                if (_target == null)
                    _target = (T) target;

                return _target;
            }
        }

        protected void Start()
        {
            _target = (T) target;
        }
    }
}