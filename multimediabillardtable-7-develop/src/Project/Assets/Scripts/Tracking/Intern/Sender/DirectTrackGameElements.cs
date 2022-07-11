using System;
using UnityEngine;

namespace Tracking.Intern.Sender
{
    /// <summary>
    /// This class holds all relevant unity objects to track the balls and all related events
    /// </summary>
    [Serializable]
    public class DirectTrackGameElements
    {
        [Tooltip("The white cue is at position(0)! Add all remaining balls ordered by the number on it.")]
        public GameObject[] balls = new GameObject[16];

        [Header("Table")]
        public Collider tableField;

        [Header("Table Pockets")] 
        public Collider upperLeft;
        public Collider upperMid;
        public Collider upperRight;

        public Collider bottomLeft;
        public Collider bottomMid;
        public Collider bottomRight;
        
        [Header("Wall")]
        public Collider head;
        public Collider foot;
        public Collider left;
        public Collider right;
    }
}