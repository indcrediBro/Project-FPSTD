using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IEnemyState
{
    public void EnterState(EnemyStateMachine _stateMachine)
    {
    }

    public void UpdateState(EnemyStateMachine _stateMachine)
    {
        if (IsNotPlayingAttackAnimation(_stateMachine) || _stateMachine.m_Animations.IsAnimationNotInProgress("Idle"))
        {
            if (_stateMachine.m_PlayerTarget && _stateMachine.m_Detection.IsInAttackRange(_stateMachine.m_PlayerTarget))
            {
                _stateMachine.m_Animations.PlayAnimation(EnemyState.Attack);
                _stateMachine.m_Attack.PerformAttack(_stateMachine.m_PlayerTarget);
            }
            else if (_stateMachine.m_BaseTarget && _stateMachine.m_Detection.IsInAttackRange(_stateMachine.m_NavigationBaseTarget))
            {
                _stateMachine.m_Animations.PlayAnimation(EnemyState.Attack);
                _stateMachine.m_Attack.PerformAttack(_stateMachine.m_BaseTarget);
            }
        }

        if(!IsNotPlayingAttackAnimation(_stateMachine))
        {
            _stateMachine.TransitionToState(_stateMachine.m_IdleState);
        }
    }

    public void ExitState(EnemyStateMachine _stateMachine)
    {

    }

    private bool IsNotPlayingAttackAnimation(EnemyStateMachine _stateMachine)
    {
        return _stateMachine.m_Animations.IsAnimationNotInProgress("Attack 0") &&
               _stateMachine.m_Animations.IsAnimationNotInProgress("Attack 1") &&
               _stateMachine.m_Animations.IsAnimationNotInProgress("Attack 2") &&
               _stateMachine.m_Animations.IsAnimationNotInProgress("Attack 3");
    }
}
