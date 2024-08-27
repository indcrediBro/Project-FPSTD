using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    [SerializeField] private AudioClip m_chaseClip, m_alertClip, m_hurtClip, m_deadClip;
    [SerializeField] private AudioSource m_audioSource;

    public void PlayChaseSound()
    {
        m_audioSource.Stop();
        m_audioSource.clip = m_chaseClip;
        m_audioSource.pitch = RandomNumber.Instance.NextFloat(0.8f, 1.2f);
        m_audioSource.loop = true;
        m_audioSource.Play();
    }

    public void StopSound()
    {
        m_audioSource.Stop();
    }

    public void PlayHurtSound()
    {
        m_audioSource.Stop();
        m_audioSource.clip = m_hurtClip;
        m_audioSource.pitch = RandomNumber.Instance.NextFloat(0.8f, 1.2f);
        m_audioSource.loop = false;
        m_audioSource.Play();
    }

    public void PlayDeadSound()
    {
        m_audioSource.Stop();
        m_audioSource.clip = m_hurtClip;
        m_audioSource.pitch = RandomNumber.Instance.NextFloat(0.8f, 1.2f);
        m_audioSource.loop = false;
        m_audioSource.Play();
    }

    public void PlayAlertSound()
    {
        m_audioSource.Stop();
        m_audioSource.clip = m_alertClip;
        m_audioSource.pitch = RandomNumber.Instance.NextFloat(0.8f, 1.2f);
        m_audioSource.loop = false;
        m_audioSource.Play();
    }
}
