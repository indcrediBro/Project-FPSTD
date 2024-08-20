using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] private Slider m_healthBar;
    [SerializeField] private Slider m_staminaBar;
    [SerializeField] private TMP_Text m_ammoText;
    [SerializeField] private TMP_Text m_currencyText;

    private PlayerStats m_playerStats;
    private PlayerHealth m_playerHealth;
    private PlayerStamina m_playerStamina;
    private PlayerBuildController m_playerBuilder;

    private void Awake()
    {
        m_playerStats = GetComponent<PlayerStats>();
        m_playerHealth = m_playerStats.GetPlayerHealthComponent();
        m_playerStamina = m_playerStats.GetPlayerStaminaComponent();
        m_playerBuilder = GetComponent<PlayerBuildController>();
    }

    private void Start()
    {
        InitializeHealthBar();
        InitializeStaminaBar();
    }

    private void Update()
    {
        UpdateHealthBar();
        UpdateStaminaBar();
        UpdateCurrencyText();
        UpdateAmmoText();
    }

    private void InitializeHealthBar()
    {
        if (m_healthBar)
        {
            m_healthBar.minValue = 0;
            m_healthBar.maxValue = m_playerHealth.GetMaxHealthValue();
            m_healthBar.value = m_playerHealth.GetCurrentHealthValue();
        }
    }

    private void UpdateHealthBar()
    {
        if (m_healthBar)
        {
            m_healthBar.value = m_playerHealth.GetCurrentHealthValue();
        }
    }

    private void InitializeStaminaBar()
    {
        if (m_staminaBar)
        {
            m_staminaBar.minValue = 0;
            m_staminaBar.maxValue = m_playerStamina.GetMaxStamina();
            m_staminaBar.value = m_playerStamina.GetStamina();
        }
    }

    private void UpdateStaminaBar()
    {
        if (m_staminaBar)
        {
            m_staminaBar.value = m_playerStamina.GetStamina();
        }
    }

    private void UpdateAmmoText()
    {
        if (m_ammoText)
        {
            if (m_playerStats.IsInBuilderMode())
            {
                if(InventoryManager.Instance.GetBuildableItem(m_playerBuilder.GetActiveBuildableName()) != null)
                    m_ammoText.text = InventoryManager.Instance.GetBuildableItem(m_playerBuilder.GetActiveBuildableName()).Quantity.ToString();
                else
                    m_ammoText.text = "0";
            }
        }
    }

    public void UpdateAmmoText(string _text)
    {
        if (m_ammoText)
        {
            m_ammoText.text = _text;
        }
    }

    private void UpdateCurrencyText()
    {
        if (m_currencyText)
        {
            m_currencyText.text = "$" + EconomyManager.Instance.GetPlayerMoney().ToString();
        }
    }
}
