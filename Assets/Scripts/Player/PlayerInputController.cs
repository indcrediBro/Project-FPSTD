using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    public Vector2 m_MoveInput;
    public Vector2 m_LookInput;
    public bool m_JumpInput  ;
    public bool m_SprintInput ;
    public bool m_DodgeInput ;

    private PlayerInputSettings m_inputActions;

    private void Awake()
    {
        m_inputActions = new PlayerInputSettings();
    }

    private void OnEnable()
    {
        m_inputActions.Enable();

        m_inputActions.PlayerInputActions.Move.performed += OnMove;
        m_inputActions.PlayerInputActions.Look.performed += OnLook;
        m_inputActions.PlayerInputActions.Jump.performed += OnJump;
        m_inputActions.PlayerInputActions.Sprint.performed += OnSprint;
        m_inputActions.PlayerInputActions.Dodge.performed += OnDodge;
    }

    private void OnDisable()
    {
        m_inputActions.Disable();

        m_inputActions.PlayerInputActions.Move.performed -= OnMove;
        m_inputActions.PlayerInputActions.Look.performed -= OnLook;
        m_inputActions.PlayerInputActions.Jump.performed -= OnJump;
        m_inputActions.PlayerInputActions.Sprint.performed -= OnSprint;
        m_inputActions.PlayerInputActions.Dodge.performed -= OnDodge;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        m_MoveInput = context.ReadValue<Vector2>();
    }

    private void OnLook(InputAction.CallbackContext context)
    {
        m_LookInput = context.ReadValue<Vector2>();
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        m_JumpInput = context.ReadValueAsButton();
    }

    private void OnSprint(InputAction.CallbackContext context)
    {
        m_SprintInput = context.ReadValueAsButton();
    }

    private void OnDodge(InputAction.CallbackContext context)
    {
        m_DodgeInput = context.ReadValueAsButton();
    }
}
