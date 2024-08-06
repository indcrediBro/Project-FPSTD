using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerStats))]
public class PlayerSprint : MonoBehaviour
{
    private PlayerStats m_stats;

    private float m_sprintTimer = 0f;
    [SerializeField] private float m_sprintRate = 0.5f; //in seconds

    [SerializeField] private bool m_useStamina;
    [SerializeField] private float m_staminaCost = 1f;

    private void Start()
    {
        m_stats = GetComponent<PlayerStats>();
    }

    // Fix this
    public void Sprint(bool _sprintInput, Vector2 _moveInput, bool _isGrounded)
    {
        if (_sprintInput && _moveInput.magnitude > 0 && _isGrounded)
        {
            m_sprintTimer += Time.deltaTime;
            if (m_sprintTimer > m_sprintRate)
            {
                if (m_useStamina)
                {
                    if( m_stats.GetStamina() > m_staminaCost)
                    {
                        m_stats.GetPlayerStaminaComponent().UseStamina(m_staminaCost);
                        m_stats.SetSprintMultiplier(2f);
                        m_sprintTimer = 0f;
                    }
                    else
                    {
                        m_stats.SetSprintMultiplier(1f);
                    }
                }
                else
                {
                    m_stats.SetSprintMultiplier(2f);
                }
            }
        }
        else
        {
            m_stats.SetSprintMultiplier(1f);
        }
    }
}
