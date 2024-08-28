using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using Unity.VisualScripting;
using System;

public class SettingsManager : Singleton<SettingsManager>
{
    [SerializeField] public AudioMixer m_audioMixer;
    [SerializeField] private Slider m_masterVolumeSlider;
    [SerializeField] private Slider m_musicVolumeSlider;
    [SerializeField] private Slider m_sfxVolumeSlider;
    [SerializeField] private Slider m_lookSensitivitySlider;
    [SerializeField] private float m_masterVolume;
    [SerializeField] private float m_musicVolume;
    [SerializeField] private float m_sfxVolume;
    [SerializeField] private float m_lookSensitivity;
    [SerializeField] private int m_qualitySettings;
    [SerializeField] private int m_postProcessing;
    [SerializeField] private int m_vfx;
    [SerializeField] private bool m_isVfxOn;
    [SerializeField] private bool m_isQualitySettingsOn;
    [SerializeField] private bool m_isPostProcessingOn;
    [SerializeField] private Toggle m_qualitySettingsToggle;
    [SerializeField] private Toggle m_postProcessingToggle;
    [SerializeField] private Toggle m_vfxToggle;
    [SerializeField] private AudioMixerGroup m_Master;
    [SerializeField] private AudioMixerGroup m_Music;
    [SerializeField] private AudioMixerGroup m_Sfx;

    private void Start()
    {
        LoadSettings();

    }
    private void LoadSettings()
    {
        m_masterVolume = PlayerPrefs.GetFloat("MASTER", .8f);
        m_masterVolumeSlider.value = m_masterVolume;
        m_musicVolume = PlayerPrefs.GetFloat("MUSIC", 1f);
        m_musicVolumeSlider.value = m_musicVolume;
        m_sfxVolume = PlayerPrefs.GetFloat("SFX", 1f);
        m_sfxVolumeSlider.value = m_sfxVolume;
        m_lookSensitivity = PlayerPrefs.GetFloat("SENSITIVITY", 1f);
        m_lookSensitivitySlider.value = m_lookSensitivity;

        bool isVfxOn = PlayerPrefs.GetInt("VFX") == 1;
        m_isVfxOn = isVfxOn;
        m_vfxToggle.isOn = m_isVfxOn;

        bool isQualitySettingsOn = PlayerPrefs.GetInt("SETINGS") == 1;
        m_isQualitySettingsOn = isQualitySettingsOn;
        m_qualitySettingsToggle.isOn = m_isQualitySettingsOn;

        bool isPostProcessingOn = PlayerPrefs.GetInt("PROCESSING") == 1;
        m_isPostProcessingOn = isPostProcessingOn;
        m_postProcessingToggle.isOn = m_isPostProcessingOn;


        SetMasterVolume();
        SetMusicVolume();
        SetSFXVolume();
        SetLookSensitivity();
        SetQualitySettings();
        SetPostProcessing();
        SetVFX();

    }

    public void SetMasterVolume()
    {
        m_masterVolume = m_masterVolumeSlider.value;
        m_audioMixer.SetFloat("Master", Mathf.Log10(m_masterVolume) * 20);
        PlayerPrefs.SetFloat("MASTER", m_masterVolume);
    }

    public void SetMusicVolume()
    {
        m_musicVolume = m_musicVolumeSlider.value;
        m_audioMixer.SetFloat("Music", Mathf.Log10(m_musicVolume) * 20);
        PlayerPrefs.SetFloat("MUSIC", m_musicVolume);
    }

    public void SetSFXVolume()
    {
        m_sfxVolume = m_sfxVolumeSlider.value;
        m_audioMixer.SetFloat("Sfx", Mathf.Log10(m_sfxVolume) * 20);
        PlayerPrefs.SetFloat("SFX", m_sfxVolume);
    }

    public void SetLookSensitivity()
    {
        m_lookSensitivity = m_lookSensitivitySlider.value;
        PlayerPrefs.SetFloat("SENSITIVITY", m_lookSensitivity);
    }

    public float GetLookSensitivity()
    {
        return m_lookSensitivity;
    }

    public void SetQualitySettings()
    {
        if (m_qualitySettingsToggle.isOn == true)
        {
            m_qualitySettings = 1;
        }
        else
        {
            m_qualitySettings = 0;
        }
        PlayerPrefs.SetInt("SETINGS", m_qualitySettings);

    }

    public void SetPostProcessing()
    {
        if (m_postProcessingToggle.isOn == true)
        {
            m_postProcessing = 1;
        }
        else
        {
            m_postProcessing = 0;
        }
        PlayerPrefs.SetInt("PROCESSING", m_postProcessing);
    }

    public void SetVFX()
    {
        if (m_vfxToggle.isOn == true)
        {
            m_vfx = 1;
        }
        else
        {
            m_vfx = 0;
        }
        PlayerPrefs.SetInt("VFX", m_vfx);
    }
}
