using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

/// <summary>
/// class that handles the changes in volume for the backround sfx 
/// </summary>
public class SetBackroundVolume : MonoBehaviour
{
    /// <summary>
    /// <param name="backgroundMixer"> contains the mixer for the backround music</param>
    /// <param name="backgroundSlider"> contains the slider for the backround music</param>
    /// </summary>
    public AudioMixer backgroundMixer;
    public Slider backgroundSlider;

    /// <summary>
    /// the start Method sets the brackroundmixer and backgroundSlider to the value stored in the PlayerpPrefs for volumeBackground
    /// </summary>
    private void Start()
    {
        // if a stored value with name MasterVol exists 
        // load this value to the mixer asset MasterVol in the main mixer
        if (PlayerPrefs.HasKey("volumeBackground"))
        {
            backgroundMixer.SetFloat("BackgroundSoundVol", Mathf.Log10(PlayerPrefs.GetFloat("volumeBackground")) * 20);
            backgroundSlider.value = PlayerPrefs.GetFloat("volumeBackground");
            UnityEngine.Debug.Log("Audio Manager was loaded with Data from Preferences!");
        }
        else
        {
            UnityEngine.Debug.Log("volumeBackground is not found");
        }
    }

    /// <summary>
    /// this method sets the backgroundMixer to the slider value and stores the value in the PlayerPrefs
    /// </summary>
    /// <param name="sliderValue">Is the updated value of the backroundsfx slider</param>
    public void SetLevel(float sliderValue)
    {
        backgroundMixer.SetFloat("BackgroundSoundVol", Mathf.Log10(backgroundSlider.value) * 20); // backgroundSlider.value
        PlayerPrefs.SetFloat("volumeBackground", sliderValue);
    }
}
