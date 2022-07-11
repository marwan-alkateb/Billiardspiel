using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class which handles what happens if a the play or quit button is clicked
/// </summary>
public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// contains the audiosource, which is played if a button is clicked
    /// </summary>  
    public AudioSource buttonclick;

    /// <summary>
    /// The PlayGame method plays the sound which is stored in the audiosource and starts a routine which waits for the sound the end 
    /// </summary>
    public void PlayGame()
    {
        buttonclick.Play();
        StartCoroutine(Wait("NameInput"));
    }

    /// <summary>
    /// The PlayTrickshot method plays the sound which is stored in the audiosource and starts a routine which waits for the sound the end 
    /// </summary>
    public void PlayTrickshot()
    {
        buttonclick.Play();
        StartCoroutine(Wait("TrickshotMode"));
    }

    /// <summary>
    /// waits until the sound is finished playing and then loads the scene which was given
    /// </summary>
    IEnumerator Wait(string sceneName)
    {
        yield return new WaitUntil(() => buttonclick.isPlaying == false);
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// The QuitGame method plays the sound which is stored in the audiosource and starts a routine which waits for the sound the end 
    /// </summary>
    public void QuitGame()
    {
        buttonclick.Play();
        StartCoroutine(Waiter());
    }

    /// <summary>
    /// waits until the sound is finished playing and then saves a default value for the sounds sliders and finally quits the application
    /// </summary>
    IEnumerator Waiter()
    {
        yield return new WaitUntil(() => buttonclick.isPlaying == false);
        Debug.Log("QUIT");
        Application.Quit();
    }
}
