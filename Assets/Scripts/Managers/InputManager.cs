#region

using UnityEngine;
using UnityEngine.InputSystem;

#endregion

public class InputManager : Singleton<InputManager>
{
    private PlayerInputActions m_inputActions;

    [HideInInspector] public Vector2 m_MoveInput;
    [HideInInspector] public Vector2 m_LookInput;
    [HideInInspector] public InputAction m_SwitchWeaponInput;
    [HideInInspector] public bool m_SprintInput;
    [HideInInspector] public bool m_DodgeInput;
    [HideInInspector] public InputAction m_ReloadInput;
    [HideInInspector] public InputAction m_JumpInput;
    [HideInInspector] public InputAction m_InteractInput;
    [HideInInspector] public InputAction m_PauseInput;
    [HideInInspector] public InputAction m_AttackInput;
    [HideInInspector] public InputAction m_BuildInput;

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
        m_inputActions.PlayerInputMap.Sprint.performed += ctx => m_SprintInput = ctx.ReadValueAsButton();
        m_ReloadInput = m_inputActions.PlayerInputMap.Reload;
        m_SwitchWeaponInput = m_inputActions.PlayerInputMap.SwitchWeapon;
        m_InteractInput = m_inputActions.PlayerInputMap.Interact;
        m_PauseInput = m_inputActions.PlayerInputMap.Pause;
        m_JumpInput = m_inputActions.PlayerInputMap.Jump;
        m_AttackInput = m_inputActions.PlayerInputMap.Attack;
        m_BuildInput = m_inputActions.PlayerInputMap.Build;
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