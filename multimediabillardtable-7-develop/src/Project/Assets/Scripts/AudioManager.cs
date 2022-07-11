using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer theMixer;

    // Load all volumes values at start

    void Start()
    {
        // if a stored value with name "volumeBackground" exists, load the value to the mixer.
        if (PlayerPrefs.HasKey("volumeBackground"))
        {
            theMixer.SetFloat("BackgroundSoundVol", Mathf.Log10(PlayerPrefs.GetFloat("volumeBackground"))*20 );
            UnityEngine.Debug.Log("Audio Manager was loaded with Data from Preferences!");
        }
        else
        {
            UnityEngine.Debug.Log("volumeBackground is not found"); 
        }

    }

}
