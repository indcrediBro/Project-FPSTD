using UnityEngine;

public abstract class Health : MonoBehaviour
{
	[Header("Health Settings")]
	[SerializeField] protected bool m_isPlayer;
	[SerializeField] protected bool m_dontDestroy;
	[SerializeField] protected bool m_dontRemoveDeadBody;

	[SerializeField] protected int m_maxHealth;
	[SerializeField] protected int m_currentHealth;

	[SerializeField] protected int m_regenerateAmount;
	[SerializeField] protected float m_regenerateRate;

	[SerializeField] protected UnityEngine.UI.Slider m_healthBar;

	private float m_currentRegenTime;
	protected bool m_isDead;

	public virtual bool IsPlayer() { return m_isPlayer; }
	public virtual int GetMaxHealthValue() { return m_maxHealth; }
	public virtual int GetCurrentHealthValue() { return m_currentHealth; }
	public virtual bool IsDead() { return m_isDead; }

	public virtual void Heal(int _value)
	{
		m_currentHealth += _value;
		if (m_currentHealth >= m_maxHealth)
		{
			m_currentHealth = m_maxHealth;
		}
	}

	public virtual void TakeDamage(int _damage)
	{
		m_currentHealth -= _damage;
		if (m_currentHealth <= 0)
		{
			Die();
		}
	}

	protected virtual void Die()
	{
		m_isDead = true;

		if(m_healthBar) m_healthBar.value = 0;

		if (!m_dontRemoveDeadBody)
		{
            if (m_dontDestroy)
            {
				gameObject.SetActive(false);
			}
			else
			{
				Destroy(gameObject);
			}
		}
	}

	private void RegenerateOverTime()
	{
		m_currentRegenTime -= Time.deltaTime;

		if (m_currentRegenTime <= 0)
		{
			Heal(m_regenerateAmount);
			m_currentRegenTime = m_regenerateRate;
		}
	}

	private void ResetHealth()
	{
		m_currentHealth = m_maxHealth;
		m_isDead = false;
	}

	protected virtual void OnEnable()
	{
		ResetHealth();

		if (m_healthBar)
		{
			m_healthBar.minValue = 0;
			m_healthBar.maxValue = m_maxHealth;
			m_healthBar.value = m_maxHealth;
		}
	}

	protected virtual void Update()
	{
		if (m_healthBar) m_healthBar.value = m_currentHealth;

		if (m_regenerateRate > 0 && m_regenerateAmount > 0 && m_currentHealth < m_maxHealth)
		{
			RegenerateOverTime();
		}
	}
}
