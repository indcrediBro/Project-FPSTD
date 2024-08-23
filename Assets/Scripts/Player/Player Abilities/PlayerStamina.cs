using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerStats))]
public class PlayerStamina : MonoBehaviour
{
    private PlayerStats m_stats;
    private PlayerDodge m_dodge;

    [SerializeField] private float m_staminaRegenRate = .5f;

    [SerializeField] private float m_staminaRegenTime = 0.5f;
    private float m_staminaRegenTimer;
    private bool m_allowStaminaRegen;

    private void Awake()
    {
        m_stats = GetComponent<PlayerStats>();
        m_dodge = GetComponent<PlayerDodge>();
    }

    private void OnEnable()
    {
        ResetStaminaToMax();
    }

    private void Update()
    {
        if (GameReferences.Instance.m_IsPaused) return;

        if (m_stats.IsGrounded() && !InputManager.Instance.m_SprintInput && m_dodge.CanDodge())
        {
            EnableStaminaRegeneration();
        }
        else
        {
            DisableStaminaRegeneration();
        }

        if (m_allowStaminaRegen)
        {
            RegenerateStamina();
        }
    }

    public float GetStamina() { return m_stats.GetStamina(); }
    public float GetMaxStamina() { return m_stats.GetMaxStamina(); }

    public void UseStamina(float _amount)
    {
        if (GetStamina() > 0f)
        {
            if (GetStamina() >= _amount)
            {
                float stamina = GetStamina() - _amount;
                m_stats.SetStamina(stamina);
            }
            else
            {
                m_stats.SetStamina(0f);
            }
        }
    }

    private void RegenerateStamina()
    {
        if (GetStamina() < GetMaxStamina())
        {
            m_staminaRegenTimer += Time.deltaTime;

            if (m_staminaRegenTimer > m_staminaRegenTime)
            {
                UseStamina(-m_staminaRegenRate); // Negative to increase stamina
                m_staminaRegenTimer = 0f;
            }
        }
    }

    public void EnableStaminaRegeneration()
    {
        m_allowStaminaRegen = true;
    }

    public void DisableStaminaRegeneration()
    {
        m_allowStaminaRegen = false;
    }

    public void ResetStaminaToMax()
    {
        m_stats.SetStamina(GetMaxStamina());
    }
}
