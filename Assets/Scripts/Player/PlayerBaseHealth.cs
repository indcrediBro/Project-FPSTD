using UnityEngine;
using System.Collections;

public class PlayerBaseHealth : Health
{
    [SerializeField] private int m_regenerateAmount;
    [SerializeField] private float m_regenerateRate;

    private float m_currentRegenTime;

    private void Update()
    {
        if (GameReferences.Instance.m_IsPaused) return;

        if (m_regenerateRate > 0 && m_regenerateAmount > 0 && GetCurrentHealthValue() < GetMaxHealthValue())
        {
            Regenerate();
        }
    }

    private void Regenerate()
    {
        m_currentRegenTime -= Time.deltaTime;

        if (m_currentRegenTime <= 0)
        {
            Heal(m_regenerateAmount);
            m_currentRegenTime = m_regenerateRate;
        }
    }
    public override void TakeDamage(float _damage)
    {
        base.TakeDamage(_damage);
        GameReferences.Instance.m_CameraShake.TriggerShake();
    }
    protected override void Die(float _timeBeforeRemoving)
    {
        base.Die(_timeBeforeRemoving);
        MenuManager.Instance.SetGameOverText("Base Destroyed!");
        GameStateManager.Instance.SetState(new GameOverState(GameStateManager.Instance));
    }
}
