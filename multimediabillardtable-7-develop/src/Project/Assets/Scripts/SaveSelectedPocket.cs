using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SaveSelectedPocket : MonoBehaviour
{

    // This Function is used to save player pocket choice in player preferences
    public void HandleInputData(int val)
    {
        PlayerPrefs.SetInt("selectedPocket", val);
    }
}
