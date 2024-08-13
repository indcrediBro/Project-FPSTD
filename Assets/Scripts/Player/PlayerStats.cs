using Cinemachine;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Space(5)]
    [Header("References")]
    [Space(2)]
    [SerializeField] private PlayerInputController m_inputController;
    [SerializeField] private CharacterController m_characterController;
    [SerializeField] private CinemachineVirtualCamera m_virtualCamera;
    [SerializeField] private PlayerHealth m_playerHealth;
    [SerializeField] private PlayerStamina m_playerStamina;
    public PlayerInputController GetPlayerInputComponent() { return m_inputController; }
    public CharacterController GetCharacterControllerComponent() { return m_characterController; }
    public CinemachineVirtualCamera GetVirtualCameraComponent() { return m_virtualCamera; }
    public PlayerHealth GetPlayerHealthComponent() { return m_playerHealth; }
    public PlayerStamina GetPlayerStaminaComponent() { return m_playerStamina; }

    [Space(5)]
    [Header("Stamina Settings")]
    [Space(2)]
    [SerializeField] private float m_maxStamina = 10f;
    private float m_stamina;
    public float GetMaxStamina() { return m_maxStamina; }

    [Space(5)]
    [Header("Movement Settings")]
    [Space(2)]
    [SerializeField] private float m_moveSpeed = 10f;
    [SerializeField] private float m_sprintMultiplier = 1f;
    [SerializeField] private float m_jumpHeight = 3f;
    [SerializeField] private float m_gravity = -20f;
    [SerializeField] private float m_lookSensitivity = 50f;
    public float GetMoveSpeed() { return m_moveSpeed * m_sprintMultiplier; }
    public void SetSprintMultiplier(float _multiplier) { m_sprintMultiplier = _multiplier; }
    public float GetJumpHeight() { return m_jumpHeight; }
    public float GetLookSensitivity() { return m_lookSensitivity; }
    public float GetGravity() { return m_gravity; }

    public float GetStamina() { return m_stamina; }
    public void SetStamina(float _value) { m_stamina = _value; }

    private bool m_isGrounded;
    public bool IsGrounded() { return m_isGrounded; }
    public void SetGrounded(bool _value) { m_isGrounded = _value; }

    [Space(5)]
    [Header("Weapon/Builder Settings")]
    [Space(2)]
    [SerializeField] private bool m_inBuilderMode;

    public bool IsInBuilderMode() { return m_inBuilderMode; }
    public void SetBuilderMode(bool _value) { m_inBuilderMode = _value; }
}
