using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

/// <summary>
/// class that handles the changes in volume for the soundeffects 
/// </summary>
public class SetSFXVolume : MonoBehaviour {

    /// <summary>
    /// <param name="sfxmixer"> contains the mixer for the sfx music</param>
    /// <param name="sfxslider"> contains the slider for the sound effects</param>
    /// </summary>
    public AudioMixer sfxmixer;
    public Slider sfxslider;

    /// <summary>
    /// the start Method sets the sfxmixer and sfxslider to the value stored in the PlayerpPrefs for volumeSfx
    /// </summary>
    private void Start()
    {
        if (PlayerPrefs.HasKey("volumeSfx"))
        {
            sfxmixer.SetFloat("PlaySfxSoundVol", Mathf.Log10(PlayerPrefs.GetFloat("volumeSfx")) * 20);
            sfxslider.value = PlayerPrefs.GetFloat("volumeSfx");
        }
        else
        {
            UnityEngine.Debug.Log("volumeSfx is not found");
        }
    }

    /// <summary>
    /// this method sets the sfxmixer to the slider value and stores the value in the PlayerPrefs
    /// </summary>
    /// <param name="sliderValue">Is the updated value of the sfx slider</param>
    public void SetLevel ( float sliderValue)
    {
        sfxmixer.SetFloat("PlaySfxSoundVol", Mathf.Log10 (sfxslider.value) * 20);
        PlayerPrefs.SetFloat("volumeSfx", sliderValue);
    }
}
