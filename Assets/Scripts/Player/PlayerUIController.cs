using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] private Slider m_healthBar;
    [SerializeField] private Slider m_staminaBar;

    private PlayerHealth m_playerHealth;
    private PlayerStamina m_playerStamina;

    private void Awake()
    {
        m_playerHealth = GetComponent<PlayerHealth>();
        m_playerStamina = GetComponent<PlayerStamina>();
    }

    private void Start()
    {
        InitializeHealthBar();
        InitializeStaminaBar();
    }

    private void Update()
    {
        UpdateHealthBar();
        UpdateStaminaBar();
    }

    private void InitializeHealthBar()
    {
        if (m_healthBar)
        {
            m_healthBar.minValue = 0;
            m_healthBar.maxValue = m_playerHealth.GetMaxHealthValue();
            m_healthBar.value = m_playerHealth.GetCurrentHealthValue();
        }
    }

    private void UpdateHealthBar()
    {
        if (m_healthBar)
        {
            m_healthBar.value = m_playerHealth.GetCurrentHealthValue();
        }
    }

    private void InitializeStaminaBar()
    {
        if (m_staminaBar)
        {
            m_staminaBar.minValue = 0;
            m_staminaBar.maxValue = m_playerStamina.GetMaxStamina();
            m_staminaBar.value = m_playerStamina.GetStamina();
        }
    }

    private void UpdateStaminaBar()
    {
        if (m_staminaBar)
        {
            m_staminaBar.value = m_playerStamina.GetStamina();
        }
    }
}
