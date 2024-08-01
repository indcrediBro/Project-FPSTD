using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] public AudioMixer audioMixer;
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;
    [SerializeField] private float musicVolume;
    [SerializeField] private float sfxVolume;
    [SerializeField] private AudioMixerGroup Master;
    [SerializeField] private AudioMixerGroup Music;
    [SerializeField] private AudioMixerGroup SFX;
    private void Awake()
    {
        PlayerPrefs.GetFloat("MASTERVOLUME");
        PlayerPrefs.GetFloat("MUSICVOLUME");
        PlayerPrefs.GetFloat("SFXVOLUME");
    }
    

}
