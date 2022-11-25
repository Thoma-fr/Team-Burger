using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;

    [Header("MASTER")]
    [SerializeField] private Slider mainSlider;
    [SerializeField] private TMP_InputField mainField;

    [Header("MUSIC")]
    [SerializeField] private Slider musicSlider;
    [SerializeField] private TMP_InputField musicField;

    [Header("SFX")]
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private TMP_InputField sfxField;

    [Header("AMBIENCE")]
    [SerializeField] private Slider ambienceSlider;
    [SerializeField] private TMP_InputField ambienceField;

    const string MIXER_MAIN = "MainVolume";
    const string MIXER_MUSIC = "MusicVolume";
    const string MIXER_SFX = "SfxVolume";
    const string MIXER_AMBIENCE = "AmbienceVolume";

    private void Awake()
    {
        

        // Fields
        mainField.onSubmit.AddListener(SubmitMainField);
        musicField.onSubmit.AddListener(SubmitMusicField);
        sfxField.onSubmit.AddListener(SubmitSfxField);
        ambienceField.onSubmit.AddListener(SubmitAmbienceField);

        // Sliders
        mainSlider.onValueChanged.AddListener(SetMainVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSfxVolume);
        ambienceSlider.onValueChanged.AddListener(SetAmbienceVolume);
    }

    private void Start()
    {
        SaveData data = SaveSystem.LoadGameData();
        mainSlider.value = (data.masterLevel + 80) / 100;
        musicSlider.value = (data.musicLevel + 80) / 100;
        sfxSlider.value = (data.sfxLevel + 80) / 100;
        ambienceSlider.value = (data.ambienceLevel + 80) / 100;
    }

    private void SubmitMainField(string value)
    {
        if (float.TryParse(value, out float submit))
            mainSlider.value = Mathf.Clamp(submit, 0, 100) / 100.0f;
        else
            mainField.text = (mainSlider.value * 100).ToString();
    }

    private void SubmitMusicField(string value)
    {
        if (float.TryParse(value, out float submit))
            musicSlider.value = Mathf.Clamp(submit, 0, 100) / 100.0f;
        else
            musicField.text = (musicSlider.value * 100).ToString();
    }

    private void SubmitSfxField(string value)
    {
        if (float.TryParse(value, out float submit))
            sfxSlider.value = Mathf.Clamp(submit, 0, 100) / 100.0f;
        else
            sfxField.text = (sfxSlider.value * 100).ToString();
    }

    private void SubmitAmbienceField(string value)
    {
        if (float.TryParse(value, out float submit))
            ambienceSlider.value = Mathf.Clamp(submit, 0, 100) / 100.0f;
        else
            ambienceField.text = (ambienceSlider.value * 100).ToString();
    }

    private void SetMainVolume(float value)
    {
        mixer.SetFloat(MIXER_MAIN, ParseToDebit20(value));
        mainField.text = ((int)(value * 100)).ToString();
    }

    private void SetMusicVolume(float value)
    {
        mixer.SetFloat(MIXER_MUSIC, ParseToDebit20(value));
        musicField.text = ((int)(value * 100)).ToString();
    }

    private void SetSfxVolume(float value)
    {
        mixer.SetFloat(MIXER_SFX, ParseToDebit20(value));
        sfxField.text = (value * 100).ToString();
    }

    private void SetAmbienceVolume(float value)
    {
        mixer.SetFloat(MIXER_AMBIENCE, ParseToDebit20(value));
        ambienceField.text = (value * 100).ToString();
    }

    private static float ParseToDebit20(float value)
    {
        float parse = Mathf.Lerp(-80, 20, Mathf.Clamp01(value));
        return parse;
    }

    public void OnSaveData()
    {
        SaveSystem.SaveGameData(ParseToDebit20(mainSlider.value), ParseToDebit20(musicSlider.value), ParseToDebit20(sfxSlider.value), ParseToDebit20(ambienceSlider.value));
    }
}
