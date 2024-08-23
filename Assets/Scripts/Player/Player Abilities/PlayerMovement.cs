using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerStats))]
public class PlayerMovement : MonoBehaviour
{
    private CharacterController m_characterController;
    private PlayerStats m_stats;

    private void Start()
    {
        m_stats = GetComponent<PlayerStats>();
        m_characterController = m_stats.GetCharacterControllerComponent();
    }

    public void Move(Vector2 _input)
    {
        if (GameReferences.Instance.m_IsPaused) return;

        Vector3 move = transform.right * _input.x + transform.forward * _input.y;
        m_characterController.Move(move * m_stats.GetMoveSpeed() * Time.deltaTime);
    }
}
