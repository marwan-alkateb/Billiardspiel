using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// class which handles what happens if a button is clicked
/// </summary>
public class PlayButtonClick : MonoBehaviour
{
    /// <summary>
    /// contains the audiosource
    /// </summary>
    public AudioSource buttonClickSound;

    /// <summary>
    /// plays the the sound stored in the audiosource
    /// </summary>
    public void playSoundEffect()
    {
        buttonClickSound.Play();
    }
}
