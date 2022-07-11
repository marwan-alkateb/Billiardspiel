using Display.UI.StaticUI;
using UnityEngine;
using UnityEngine.UI;

namespace StaticUI
{
    public class MoveWhiteBallOverlay : StaticUIComponent
    {
        /// <summary>
        /// The resume button.
        /// </summary>
        [SerializeField] private Button resumeButton;

        /// <summary>
        /// A property that gives access to the ButtonClickedEvent of the resume button
        /// </summary>
        public Button.ButtonClickedEvent ResumeButtonClicked
        {
            get => resumeButton.onClick;
            set => resumeButton. onClick = value;
        }
    }
}
