using UnityEngine;

public class PlayerStats : Health
{
    [SerializeField] private UnityEngine.UI.Slider m_staminaBar;
    [SerializeField] private float m_maxStamina = 10f;
    private float m_stamina;
    private float m_staminaRegenTime;
    private float m_staminaRegenTimer;
    private float m_staminaRegenRate = 1f;

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
        RegenarateStamina();
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

    public float UseStamina(float _amount)
    {
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
        m_stamina = m_maxStamina;
    }
}
