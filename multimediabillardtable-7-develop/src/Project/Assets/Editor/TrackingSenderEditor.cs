using UnityEditor;
using UnityEngine;

namespace Tracking.Unity
{
    [CustomEditor(typeof(TrackingSender))]
    public class TrackingSenderEditor : CustomInspector<TrackingSender>
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            GUILayout.Space(20);

            GUILayout.Label(
                $"Start/Stop Sending (currently: {(ScriptBehaviour?.Activated ?? false ? "Activated" : "Deactivated")})");

            if (GUILayout.Button("Start Sending"))
                ScriptBehaviour.StartSend();

            if (GUILayout.Button("Stop Sending"))
                ScriptBehaviour.StopSend();
        }
    }
}