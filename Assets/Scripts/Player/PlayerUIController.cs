using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] private Slider m_healthBar;
    [SerializeField] private Slider m_baseHealthBar;
    [SerializeField] private Slider m_staminaBar;
    [SerializeField] private TMP_Text m_ammoText;
    [SerializeField] private TMP_Text m_equippedText;
    [SerializeField] private TMP_Text m_levelText;
    [SerializeField] private TMP_Text m_currencyText;
    [SerializeField] private TMP_Text m_scoreText;

    private PlayerStats m_playerStats;
    private PlayerHealth m_playerHealth;
    private PlayerStamina m_playerStamina;
    private PlayerBuildController m_playerBuilder;
    private PlayerWeaponController m_playerWeapon;

    private void Awake()
    {
        m_playerStats = GetComponent<PlayerStats>();
        m_playerHealth = m_playerStats.GetPlayerHealthComponent();
        m_playerStamina = m_playerStats.GetPlayerStaminaComponent();
        m_playerBuilder = GetComponent<PlayerBuildController>();
        m_playerWeapon = GetComponent<PlayerWeaponController>();
    }

    private void Start()
    {
        InitializeHealthBar();
        InitializeStaminaBar();
        InitializePlayerBaseHealthBar();
    }

    private void Update()
    {
        if (GameReferences.Instance.m_IsGameOver) return;

        UpdateHealthBar();
        UpdatePlayerBaseHealthBar();
        UpdateStaminaBar();
        UpdateCurrencyText();
        if (m_playerStats.IsInBuilderMode()) UpdateAmmoBuilderModeText();
        UpdateScoreText();
        UpdateCurrentEquippedText();
        UpdateLevelText();
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
    private void InitializePlayerBaseHealthBar()
    {
        if (m_baseHealthBar)
        {
            m_baseHealthBar.minValue = 0;
            m_baseHealthBar.maxValue = GameReferences.Instance.m_PlayerBase.GetMaxHealthValue();
            m_baseHealthBar.value = GameReferences.Instance.m_PlayerBase.GetCurrentHealthValue();
        }
    }
    private void UpdateHealthBar()
    {
        if (m_healthBar)
        {
            m_healthBar.value = m_playerHealth.GetCurrentHealthValue();
        }
    }
    private void UpdatePlayerBaseHealthBar()
    {
        if (m_baseHealthBar)
        {
            m_baseHealthBar.value = GameReferences.Instance.m_PlayerBase.GetCurrentHealthValue();
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

    private void UpdateAmmoBuilderModeText()
    {
        if (m_ammoText)
        {
            if (m_playerStats.IsInBuilderMode())
            {
                if (InventoryManager.Instance.GetBuildableItem(m_playerBuilder.GetActiveBuildableName()) != null)
                    m_ammoText.text = InventoryManager.Instance.GetBuildableItem(m_playerBuilder.GetActiveBuildableName()).Quantity.ToString();
                else
                    m_ammoText.text = "0";
            }
        }
    }

    private void UpdateScoreText()
    {
        if (m_scoreText)
        {
            m_scoreText.text = ScoreManager.Instance.GetScore().ToString();
        }
    }

    public void UpdateAmmoText(string _text)
    {
        if (m_ammoText)
        {
            m_ammoText.text = _text;
            Debug.Log("Updating Ammo UI: " + _text);
        }
    }

    private void UpdateCurrencyText()
    {
        if (m_currencyText)
        {
            m_currencyText.text = EconomyManager.Instance.GetPlayerMoney().ToString();
        }
    }

    private void UpdateCurrentEquippedText()
    {
        if (m_equippedText)
        {
            if (m_playerStats.IsInBuilderMode())
            {
                m_equippedText.text = m_playerBuilder.GetActiveBuildableName();
            }
            else
            {
                m_equippedText.text = m_playerWeapon.GetActiveWeapon().m_weaponName;
            }
        }
    }

    private void UpdateLevelText()
    {
        if (m_levelText)
        {
            if (!m_playerStats.IsInBuilderMode())
            {
                m_levelText.text = "Lvl." + m_playerWeapon.GetActiveWeapon().GetLevelNumber();
            }
        }
    }
}
