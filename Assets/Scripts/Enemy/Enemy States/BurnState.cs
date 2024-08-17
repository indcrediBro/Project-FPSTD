using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnState : IEnemyState
{
    public void EnterState(EnemyStateMachine _stateMachine)
    {
        _stateMachine.m_Animations.PlayAnimation(EnemyState.Burn);
        _stateMachine.m_Burn.StartBurning();
    }

    public void UpdateState(EnemyStateMachine _stateMachine)
    {
        if (_stateMachine.m_Health.IsDead())
        {
            _stateMachine.TransitionToState(_stateMachine.m_DeadState);
        }
    }

    public void ExitState(EnemyStateMachine _stateMachine)
    {
        _stateMachine.m_Burn.StopBurning();
    }
}
