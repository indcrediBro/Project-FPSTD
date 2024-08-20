using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    private EnemyStateMachine m_stateMachine;

    private void Start()
    {
        if (m_stateMachine == null) m_stateMachine = GetComponent<EnemyStateMachine>();
    }

    protected override void Die(float _time)
    {
        base.Die(m_waitTimeBeforeDeath);
        m_stateMachine.TransitionToState(m_stateMachine.m_DeadState);
        if (EnemyManager.Instance)
        {
            // EnemyManager.Instance.ActiveEnemies.Remove(gameObject);
            // EnemyManager.Instance.UpdateAcviteEnemies();
            ResetHealthToMax();
        }
    }

    public override void TakeDamage(float _damage)
    {
        m_stateMachine.TransitionToState(m_stateMachine.m_HurtState);
        base.TakeDamage(_damage);
    }
}
