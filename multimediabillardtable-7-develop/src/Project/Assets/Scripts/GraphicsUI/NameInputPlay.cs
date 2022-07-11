using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// class that stores the name of the playes and starts the actual game scene
/// </summary>
public class NameInputPlay : MonoBehaviour
{
    /// <summary>
    /// <param name="firstname">the name that is stored, if no name is in the input for the first name</param>
    /// <param name="secondname">the name that is stored, if no name is in the input for the second name</param>
    /// <param name="firstNameInputField">the input field for the first player name</param>
    /// <param name="secondNameInputField">the input field for the second player name</param>
    /// <param name="buttonclick">contains the audiosource which is played if a button is clicked</param>
    /// </summary>
    public string firstname = "First Player";
    public string secondname = "Second Player";
    public TMP_InputField firstNameInputField;
    public TMP_InputField secondNameInputField;
    public AudioSource buttonclick;

    /// <summary>
    /// calls the storeName method, than starts playing the sound in the AudioSource Input and starts a Coroutine
    /// </summary>
    public void PlayGame()
    {
        storeName();
        buttonclick.Play();
        StartCoroutine(wait());
    }

    /// <summary>
    /// Wait until the sound is finished playing and then loads the next Scene in the Scene Manager
    /// </summary>
    IEnumerator wait()
    {
        yield return new WaitUntil(() => buttonclick.isPlaying == false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /// <summary>
    /// stores the input of the firstNameInputField and secondNameInputField in the PlayerPrefs. If the input fields are empty it uses the firstname and secondname string
    /// </summary>
    public void storeName()
    {
        if (firstNameInputField.GetComponent<TMP_InputField>().text == "")
        {
            firstname = "First Player";
            PlayerPrefs.SetString("firstName", firstname);
        }
        else
        {
            firstname = firstNameInputField.GetComponent<TMP_InputField>().text;
            PlayerPrefs.SetString("firstName", firstname);
        }
        if (secondNameInputField.GetComponent<TMP_InputField>().text == "")
        {
            secondname = "Second Player";
            PlayerPrefs.SetString("secondName", secondname);
        }
        else
        {
            secondname = secondNameInputField.GetComponent<TMP_InputField>().text;
            PlayerPrefs.SetString("secondName", secondname);
        }
    }
}
