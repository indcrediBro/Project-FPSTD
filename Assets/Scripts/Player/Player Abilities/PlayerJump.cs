using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerStats))]
public class PlayerJump : MonoBehaviour
{
    private PlayerStats m_stats;
    private Vector3 m_velocity;
    private bool m_isGrounded;

    [SerializeField] private bool m_useStamina;
    [SerializeField] private float m_staminaCost = 3f;

    private void Start()
    {
        m_stats = GetComponent<PlayerStats>();
    }

    public void Jump(bool _jumpInput)
    {
        if (GameReferences.Instance.m_IsPaused) return;

        m_isGrounded = m_stats.IsGrounded();
        ApplyGroundedForce();

        bool canJump = _jumpInput && m_isGrounded;

        if (canJump)
        {
            DoJump();
        }

        ApplyGravity();
        m_stats.GetCharacterControllerComponent().Move(m_velocity * Time.deltaTime);
    }

    private void DoJump()
    {
        if (m_useStamina)
        {
            if (m_stats.GetStamina() > m_staminaCost)
            {
                AudioManager.Instance.PlaySound("SFX_PlayerJump");
                m_stats.GetPlayerStaminaComponent().UseStamina(m_staminaCost);
                m_velocity.y = Mathf.Sqrt(m_stats.GetJumpHeight() * -2f * m_stats.GetGravity());
            }
        }
        else
        {
            AudioManager.Instance.PlaySound("SFX_PlayerJump");
            m_velocity.y = Mathf.Sqrt(m_stats.GetJumpHeight() * -2f * m_stats.GetGravity());
        }

    }

    private void ApplyGroundedForce()
    {
        if (m_isGrounded && m_velocity.y < 0)
        {
            m_velocity.y = -2f;
        }
    }

    private void ApplyGravity()
    {
        m_velocity.y += m_stats.GetGravity() * Time.deltaTime;
    }

    public bool IsGrounded()
    {
        return m_isGrounded;
    }
}