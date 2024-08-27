using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnAudioClipPlayed : MonoBehaviour
{
    [SerializeField] private AudioSource m_audioSource;

    private void Update()
    {
        if (m_audioSource.time >= m_audioSource.clip.length)
        {
            gameObject.SetActive(false);
        }
    }
}
