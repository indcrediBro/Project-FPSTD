using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    private PlayerInputActions m_inputActions;

    [HideInInspector] public Vector2 m_MoveInput;
    [HideInInspector] public Vector2 m_LookInput;
    [HideInInspector] public float m_SwitchWeaponInput;
    [HideInInspector] public bool m_JumpInput;
    [HideInInspector] public bool m_SprintInput;
    [HideInInspector] public bool m_DodgeInput;
    [HideInInspector] public bool m_ReloadInput;
    [HideInInspector] public InputAction m_AttackInput;

    protected override void Awake()
    {
        base.Awake();
        InitializeInputs();
    }

    private void InitializeInputs()
    {
        m_inputActions = new PlayerInputActions();

        m_inputActions.PlayerInputMap.Move.performed += ctx => m_MoveInput = ctx.ReadValue<Vector2>();
        m_inputActions.PlayerInputMap.Look.performed += ctx => m_LookInput = ctx.ReadValue<Vector2>();
        m_inputActions.PlayerInputMap.Jump.performed += ctx => m_JumpInput = ctx.ReadValueAsButton();
        m_inputActions.PlayerInputMap.Sprint.performed += ctx => m_SprintInput = ctx.ReadValueAsButton();
        m_inputActions.PlayerInputMap.Dodge.performed += ctx => m_DodgeInput = ctx.ReadValueAsButton();
        m_inputActions.PlayerInputMap.Reload.performed += ctx => m_ReloadInput = ctx.ReadValueAsButton();
        m_AttackInput = m_inputActions.PlayerInputMap.Attack;
    }

    private void OnEnable()
    {
        m_inputActions.PlayerInputMap.Enable();
    }

    private void OnDisable()
    {
        m_inputActions.PlayerInputMap.Disable();
    }
}
