using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerStats))]
public class PlayerJump : MonoBehaviour
{
    private CharacterController m_characterController;
    private PlayerStats m_stats;
    private Vector3 m_velocity;
    private bool m_isGrounded;

    [SerializeField] private bool m_useStamina;
    [SerializeField] private float m_staminaCost = 3f;

    private void Start()
    {
        m_stats = GetComponent<PlayerStats>();
        m_characterController = m_stats.GetCharacterControllerComponent();
    }

    public void Jump(bool _jumpInput)
    {
        m_isGrounded = m_characterController.isGrounded;
        ApplyGroundedForce();

        bool canJump = _jumpInput && m_isGrounded;

        if (canJump)
        {
            DoJump();
        }
        
        ApplyGravity();
        m_characterController.Move(m_velocity * Time.deltaTime);
    }

    private void DoJump()
    {
        if (m_useStamina && m_stats.GetStamina() > m_staminaCost)
        {
            if (m_stats.GetPlayerStaminaComponent().UseStamina(m_staminaCost) > 0)
            {
                m_velocity.y = Mathf.Sqrt(m_stats.GetJumpHeight() * -2f * m_stats.GetGravity());
            }
            return;
        }

        m_velocity.y = Mathf.Sqrt(m_stats.GetJumpHeight() * -2f * m_stats.GetGravity());
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