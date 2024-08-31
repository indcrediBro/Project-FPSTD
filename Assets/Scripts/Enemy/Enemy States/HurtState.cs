using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtState : IEnemyState
{
    public void EnterState(EnemyStateMachine _stateMachine)
    {
        _stateMachine.m_Animations.PlayAnimation(EnemyState.Hurt);
    }

    public void UpdateState(EnemyStateMachine _stateMachine)
    {
        if (_stateMachine.m_Health.IsDead())
        {
            _stateMachine.TransitionToState(_stateMachine.m_DeadState);
        }
        else if (_stateMachine.m_Animations.IsAnimationNotInProgress("Hurt") || _stateMachine.m_Animations.IsAnimationNotInProgress("Burn"))
        {
            _stateMachine.TransitionToState(_stateMachine.m_ChaseState);
        }
    }

    public void ExitState(EnemyStateMachine _stateMachine)
    {

    }
}
