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
            ResetHealthToMax();
        }
    }

    public void TakeDamage(float _damage, bool _isBurning)
    {
        if (!_isBurning)
        {
            m_stateMachine.TransitionToState(m_stateMachine.m_HurtState);
        }
        else
        {
            m_stateMachine.TransitionToState(m_stateMachine.m_BurnState);
        }
        TakeDamage(_damage);

        m_stateMachine.m_Stats.GetAudio().PlayHurtSound();
    }
}
