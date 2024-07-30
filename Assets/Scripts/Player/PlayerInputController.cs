using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    public Vector2 m_MoveInput;
    public Vector2 m_LookInput;
    public float m_SwitchWeaponInput;
    public bool m_JumpInput;
    public bool m_SprintInput;
    public bool m_DodgeInput;
    public bool m_ReloadInput;
    public InputAction m_AttackInput;

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
        m_inputActions.PlayerInputActions.SwitchWeapon.performed += OnWeaponSwitch;
        m_inputActions.PlayerInputActions.Jump.performed += OnJump;
        m_inputActions.PlayerInputActions.Sprint.performed += OnSprint;
        m_inputActions.PlayerInputActions.Dodge.performed += OnDodge;
        m_inputActions.PlayerInputActions.Dodge.performed += OnDodge;
        m_inputActions.PlayerInputActions.Reload.performed += OnReload;

        m_AttackInput = m_inputActions.PlayerInputActions.Attack;
        //m_inputActions.PlayerInputActions.Attack.started += OnAttackStart; 
    }

    private void OnDisable()
    {
        m_inputActions.Disable();

        m_inputActions.PlayerInputActions.Move.performed -= OnMove;
        m_inputActions.PlayerInputActions.Look.performed -= OnLook;
        m_inputActions.PlayerInputActions.SwitchWeapon.performed -= OnWeaponSwitch;
        m_inputActions.PlayerInputActions.Jump.performed -= OnJump;
        m_inputActions.PlayerInputActions.Sprint.performed -= OnSprint;
        m_inputActions.PlayerInputActions.Dodge.performed -= OnDodge;
        m_inputActions.PlayerInputActions.Reload.performed -= OnReload;
        //m_inputActions.PlayerInputActions.Attack.started -= OnAttackStart;
        m_AttackInput = null;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        m_MoveInput = context.ReadValue<Vector2>();
    }

    private void OnLook(InputAction.CallbackContext context)
    {
        m_LookInput = context.ReadValue<Vector2>();
    }

    private void OnWeaponSwitch(InputAction.CallbackContext context)
    {
        m_SwitchWeaponInput = context.ReadValue<float>();
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

    //private void OnAttackStart(InputAction.CallbackContext context)
    //{
    //    m_AttackInput = context.ReadValueAsButton();
    //}

    private void OnReload(InputAction.CallbackContext context)
    {
        m_ReloadInput = context.ReadValueAsButton();
    }
}
