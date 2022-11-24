using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider mainSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider ambienceSlider;

    const string MIXER_MAIN = "MainVolume";
    const string MIXER_MUSIC = "MusicVolume";
    const string MIXER_SFX = "SfxVolume";
    const string MIXER_Ambience = "AmbienceVolume";

    private void Awake()
    {
        mainSlider.onValueChanged.AddListener(SetMainVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSfxVolume);
        ambienceSlider.onValueChanged.AddListener(SetAmbienceVolume);
    }

    void SetMainVolume(float value)
    {
        mixer.SetFloat(MIXER_MAIN, Mathf.Log10(value) * 20);
    }

    void SetMusicVolume(float value)
    {
        mixer.SetFloat(MIXER_MUSIC, Mathf.Log10(value) * 20);
    }

    void SetSfxVolume(float value)
    {
        mixer.SetFloat(MIXER_SFX, Mathf.Log10(value) * 20);
    }

    void SetAmbienceVolume(float value)
    {
        mixer.SetFloat(MIXER_Ambience, Mathf.Log10(value) * 20);
    }
}
