using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// class that sets the names and turns off the music
/// </summary>
public class GetInputNames : MonoBehaviour
{
    /// <summary>
    /// <param name="firstNameField">is the name field for the first name</param>
    /// <param name="secondNameField">is the name field for the second name</param>
    /// </summary>
    public TextMeshProUGUI firstNameField;
    public TextMeshProUGUI secondNameField;

    /// <summary>
    /// sets the firstNameField and secondNameField to the string stored in the PlayerPrefs
    /// </summary>
    private void getNames()
    {
        firstNameField.text = PlayerPrefs.GetString("firstName");
        secondNameField.text = PlayerPrefs.GetString("secondName");
    }

    /// <summary>
    /// sets the sfx and backround volume to 0
    /// </summary>
    private void Start()
    {
        getNames();
    }
}
