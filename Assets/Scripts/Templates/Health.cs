using System.Collections;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    [Header("Health Settings")]
    [Space(2)]
    [SerializeField] protected bool m_isPlayer;
    [SerializeField] protected bool m_dontDestroy;

    [SerializeField] protected float m_maxHealth;
    [SerializeField] protected float m_currentHealth;
    [SerializeField] protected float m_waitTimeBeforeDeath = 0f;

    protected bool m_isDead;

    public virtual bool IsPlayer() { return m_isPlayer; }
    public virtual float GetMaxHealthValue() { return m_maxHealth; }
    public virtual float GetCurrentHealthValue() { return m_currentHealth; }
    public virtual bool IsDead() { return m_isDead; }

    public virtual void Heal(float _value)
    {
        m_currentHealth += _value;
        if (m_currentHealth >= m_maxHealth)
        {
            m_currentHealth = m_maxHealth;
        }
    }

    public virtual void TakeDamage(float _damage)
    {
        m_currentHealth -= _damage;
        if (m_currentHealth <= 0)
        {
            Die(m_waitTimeBeforeDeath);
        }
    }

    protected virtual void Die(float _timeBeforeRemoving)
    {
        StartCoroutine(DieCO(_timeBeforeRemoving));
    }

    public void ResetHealthToMax()
    {
        m_currentHealth = m_maxHealth;
        m_isDead = false;
    }

    public void SetMaxHealth(float _amount)
    {
        m_maxHealth = _amount;
        m_currentHealth = m_maxHealth;
        m_isDead = false;
    }

    protected virtual void OnEnable()
    {
        ResetHealthToMax();
    }

    private void Deactivate()
    {
        if (m_dontDestroy)
        {
            StopAllCoroutines();
            gameObject.SetActive(false);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator DieCO(float _timeToWait)
    {
        m_isDead = true;
        yield return new WaitForSeconds(_timeToWait);
        Deactivate();
    }
}
