using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerStats))]
public class PlayerMovement : MonoBehaviour
{
    private CharacterController m_characterController;
    private PlayerStats m_stats;
    private bool m_isMoving;

    private void Start()
    {
        m_stats = GetComponent<PlayerStats>();
        m_characterController = m_stats.GetCharacterControllerComponent();
    }

    public void Move(Vector2 _input)
    {
        if (GameReferences.Instance.m_IsPaused)
        {
            AudioManager.Instance.StopSound("SFX_PlayerMove");
            return;
        }
        Vector3 move = transform.right * _input.x + transform.forward * _input.y;
        m_characterController.Move(move * m_stats.GetMoveSpeed() * Time.deltaTime);

        if (_input.magnitude > 0 && m_stats.IsGrounded())
        {
            if (!m_isMoving)
            {
                AudioManager.Instance.PlaySound("SFX_PlayerMove");
                m_isMoving = true;
            }
        }
        else
        {
            if (m_isMoving)
            {
                AudioManager.Instance.StopSound("SFX_PlayerMove");
                m_isMoving = false;
            }
        }
    }
}
