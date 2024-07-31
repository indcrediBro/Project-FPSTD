using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthRegeneration : MonoBehaviour
{
    [SerializeField] private int m_regenerateAmount;
    [SerializeField] private float m_regenerateRate;

    private PlayerHealth m_playerHealth;
    private float m_currentRegenTime;

    private void Awake()
    {
        m_playerHealth = GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        if (m_regenerateRate > 0 && m_regenerateAmount > 0 && m_playerHealth.GetCurrentHealthValue() < m_playerHealth.GetMaxHealthValue())
        {
            Regenerate();
        }
    }

    private void Regenerate()
    {
        m_currentRegenTime -= Time.deltaTime;

        if (m_currentRegenTime <= 0)
        {
            m_playerHealth.Heal(m_regenerateAmount);
            m_currentRegenTime = m_regenerateRate;
        }
    }
}
