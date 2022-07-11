using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine.UI
{
    /// <summary>
    /// This Script is for the usage of the real Billiard Table. It detects every screen an Activates it, so that the Beamer Cameras can diplay their rendered picture on the real table. 
    /// </summary>
    public class ActivateAllDisplays : MonoBehaviour
    {

        public Color trackingBackgroundColor = Color.black;
        void Start()
        {
            // Display.displays[0] is the primary, default display and is always ON, so start at index 1.
            // Checks if there are 4 or more displays (real table has 4 displays, one from the PC and 3 projectors)
            // Only needed for the real table
            if (Display.displays.Length >= 4)
            {
                for (int i = 1; i < Display.displays.Length; i++)
                {
                    Display.displays[i].Activate();
                }

                //Background color change for better tracking algorithm
                GameObject table = GameObject.Find("SlatePMMA8");
                table.GetComponent<Renderer>().material.color = trackingBackgroundColor;



            }

        }

        void Update()
        {

        }
    }
}
