using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


// This script is used for selecting a pocket either via Dropdown menu and show it on the toggles (to show the ring on the pockets)
// or select a toggle (Pocket) and show the corresponding value on the dropdown menu.
public class SelectPocket : MonoBehaviour
{

   // Array of Toggles
    public Toggle[] toggles;
    // Dropdown Menu
    public TMP_Dropdown dropdown;




    // This function is used by toggles if on is clicked it changes the value of the dropdown Menu
    public void userToggle ()
    {


        for (int i = 0; i < toggles.Length; i++)
        {
            if (toggles[i].GetComponent<Toggle>().isOn == true)
            {
                dropdown.value = i;
                dropdown.RefreshShownValue();
            }
        }
        
    }

    // This Function is used by the dropdown menu and sets the corresponding toggle (Pocket) that is selected by the dropdown
    public void HandleInputData(int val)
    {
        toggles[val].GetComponent<Toggle>().isOn = true;
        
    }

}
