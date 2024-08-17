using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using Unity.VisualScripting;
using System;

public class SettingsManager : MonoBehaviour
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
        m_masterVolume = PlayerPrefs.GetFloat("MASTER",1f);
        m_masterVolumeSlider.value = m_masterVolume;
        m_musicVolume = PlayerPrefs.GetFloat("MUSIC",1f);
        m_musicVolumeSlider.value = m_musicVolume;
        m_sfxVolume = PlayerPrefs.GetFloat("SFX", 1f);
        m_sfxVolumeSlider.value = m_sfxVolume;
        m_lookSensitivity = PlayerPrefs.GetFloat("SENSITIVITY", 1f);
        m_lookSensitivitySlider.value = m_lookSensitivity;
        m_qualitySettings = PlayerPrefs.GetInt("QUALITY", 1);
        m_postProcessing = PlayerPrefs.GetInt("PROCESSING", 1);
        m_vfx = PlayerPrefs.GetInt("VFX", 1);
        

        MasterVolume();
        MusicVolume();
        SfxVolume();
        LookSensitivity(); 
        
    }
    public void MasterVolume()
    {
        m_masterVolume = m_masterVolumeSlider.value;
        m_audioMixer.SetFloat("Master", Mathf.Log10(m_masterVolume) * 20);
        PlayerPrefs.SetFloat("MASTER", m_masterVolume);
    }  
    public void MusicVolume()
    {
        m_musicVolume = m_musicVolumeSlider.value;
        m_audioMixer.SetFloat("Music", Mathf.Log10(m_musicVolume) * 20);
        PlayerPrefs.SetFloat("MUSIC", m_musicVolume);
    }
    public void SfxVolume()
    {
        m_sfxVolume = m_sfxVolumeSlider.value;
        m_audioMixer.SetFloat("Sfx", Mathf.Log10(m_sfxVolume) * 20);
        PlayerPrefs.SetFloat("SFX", m_sfxVolume);
    }
    public void LookSensitivity()
    {
        m_lookSensitivity = m_lookSensitivitySlider.value;
        PlayerPrefs.SetFloat("SENSITIVITY", m_lookSensitivity);
    }
    public void QualitySettings()
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
    public void PostProcessing()
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
    public void VFX()
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
