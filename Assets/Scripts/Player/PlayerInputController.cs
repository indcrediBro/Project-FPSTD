using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    private InputManager m_inputManager;
    private PlayerStats m_playerStats;
    private PlayerMovement m_playerMovement;
    private PlayerJump m_playerJump;
    private PlayerDodge m_playerDodge;
    private PlayerLook m_playerLook;
    private PlayerSprint m_playerSprint;
    private PlayerInteract m_playerInteract;

    private void Start()
    {
        m_inputManager = InputManager.Instance;
        m_playerStats = GetComponent<PlayerStats>();
        m_playerMovement = GetComponent<PlayerMovement>();
        m_playerJump = GetComponent<PlayerJump>();
        m_playerDodge = GetComponent<PlayerDodge>();
        m_playerLook = GetComponent<PlayerLook>();
        m_playerSprint = GetComponent<PlayerSprint>();
        m_playerInteract = GetComponent<PlayerInteract>();
    }

    private void Update()
    {
        //GameManager.Instance.HandlePause(m_inputManager.m_PauseInput.WasPerformedThisFrame());
        m_playerJump.Jump(m_inputManager.m_JumpInput.WasPerformedThisFrame());
        m_playerMovement.Move(m_inputManager.m_MoveInput);
        m_playerDodge.Dodge(m_inputManager.m_MoveInput, m_inputManager.m_DodgeInput);
        m_playerLook.Look(m_inputManager.m_LookInput);
        m_playerSprint.Sprint(m_inputManager.m_SprintInput, m_inputManager.m_MoveInput, m_playerStats.IsGrounded());
        if (m_inputManager.m_InteractInput.WasPerformedThisFrame()) m_playerInteract.Interact();
    }
}
