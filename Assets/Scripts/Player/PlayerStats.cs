using Cinemachine;
using UnityEngine;

public class PlayerStats : Health
{
    [Space(5)]
    [Header("References")]
    [Space(2)]
    [SerializeField] private PlayerInputController m_inputController;
    public PlayerInputController GetPlayerInput() { return m_inputController; }
    [SerializeField] private CharacterController m_characterController;
    public CharacterController GetCharacterController() { return m_characterController; }
    [SerializeField] private CinemachineVirtualCamera m_virtualCamera;
    public CinemachineVirtualCamera GetVirtualCamera() { return m_virtualCamera; }

    [Space(5)]
    [Header("Stamina Settings")]
    [Space(2)]
    [SerializeField] private UnityEngine.UI.Slider m_staminaBar;
    [SerializeField] private float m_maxStamina = 10f;

    private float m_stamina;
    private float m_staminaRegenTime;
    private float m_staminaRegenTimer;
    private float m_staminaRegenRate = .5f;
    private bool m_allowStaminaRegen;

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

    protected override void OnEnable()
    {
        base.OnEnable();

        m_stamina = m_maxStamina;

        if (m_staminaBar)
        {
            m_staminaBar.minValue = 0;
            m_staminaBar.maxValue = m_maxStamina;
            m_staminaBar.value = m_stamina;
        }
    }

    protected override void Update()
    {
        base.Update();

        if (m_staminaBar) m_staminaBar.value = m_stamina;

        if(m_allowStaminaRegen) RegenarateStamina();
    }

    private void RegenarateStamina()
    {
        if (m_stamina < m_maxStamina)
        {
            m_staminaRegenTimer += Time.deltaTime;

            if (m_staminaRegenTimer > m_staminaRegenTime)
            {
                m_stamina += m_staminaRegenRate;
                m_staminaRegenTimer = 0f;
            }
        }
    }

    public void EnableStaminaRegeneration()
    {
        m_allowStaminaRegen = true;
    }

    public void DisableStaminaRegeneration()
    {
        m_allowStaminaRegen = false;
    }

    public float UseStamina(float _amount)
    {
        m_allowStaminaRegen = false;

        if (m_stamina > 0f)
        {
            if (m_stamina > _amount)
            {
                m_stamina -= _amount;
                return _amount;
            }
            else
            {
                float availableStamina = m_stamina - _amount;
                m_stamina = 0f;
                return _amount + availableStamina;
            }
        }
        return 0f;
    }

    public void IncreaseMaxStamina(float _amount)
    {
        m_maxStamina += _amount;

        if (m_staminaBar)
            m_staminaBar.maxValue = m_maxStamina;
    }
}
