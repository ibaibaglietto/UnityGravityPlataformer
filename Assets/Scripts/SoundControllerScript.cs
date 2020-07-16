using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundControllerScript : MonoBehaviour
{
    public AudioMixer mixer;

    public void SetMasterLevel (float sliderValue)
    {
        mixer.SetFloat("Master", Mathf.Log10(sliderValue) * 20);
    }
    public void SetMusicLevel(float sliderValue)
    {
        mixer.SetFloat("Music", Mathf.Log10(sliderValue) * 20);
    }
    public void SetEffectsLevel(float sliderValue)
    {
        mixer.SetFloat("Effects", Mathf.Log10(sliderValue) * 20);
    }
}
