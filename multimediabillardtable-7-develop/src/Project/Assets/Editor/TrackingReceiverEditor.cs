using UnityEditor;
using UnityEngine;

namespace Tracking.Unity
{
    [CustomEditor(typeof(TrackingReceiver))]
    public class TrackingReceiverEditor : CustomInspector<TrackingReceiver>
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            GUILayout.Space(20);
            if (GUILayout.Button("Start Receive"))
                ScriptBehaviour.StartReceive();

            if (GUILayout.Button("End Receive"))
                ScriptBehaviour.EndReceive();
        }
    }
}