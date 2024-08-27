using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IEnemyState
{
    public void EnterState(EnemyStateMachine _stateMachine)
    {
        _stateMachine.m_Animations.PlayAnimation(EnemyState.Idle);
        _stateMachine.m_Stats.GetAudio().PlayAlertSound();
    }

    public void UpdateState(EnemyStateMachine _stateMachine)
    {
        if (_stateMachine.m_NavigationBaseTarget == null)
        {
            _stateMachine.SetNavigationalBaseTarget(NavmeshManager.Instance.GetBasePositionFromArray(_stateMachine.transform));
        }

        if (IsNotPlayingAnyAnimations(_stateMachine)) { return; }

        if (_stateMachine.m_Detection.IsPlayerInRange())
        {
            if (_stateMachine.m_Detection.IsInAttackRange(_stateMachine.m_PlayerTarget))
            {
                _stateMachine.TransitionToState(_stateMachine.m_AttackState);
            }
            else
            {
                _stateMachine.TransitionToState(_stateMachine.m_ChasePlayerState);
            }
        }

        if (_stateMachine.m_Detection.IsInAttackRange(_stateMachine.m_NavigationBaseTarget))
        {
            _stateMachine.TransitionToState(_stateMachine.m_AttackState);
        }
        else
        {
            _stateMachine.TransitionToState(_stateMachine.m_ChaseBaseState);
        }
    }

    public void ExitState(EnemyStateMachine _stateMachine)
    {

    }

    private bool IsNotPlayingAnyAnimations(EnemyStateMachine _stateMachine)
    {
        return _stateMachine.m_Animations.IsAnimationNotInProgress("Idle") ||
            _stateMachine.m_Animations.IsAnimationNotInProgress("Attack 0") ||
            _stateMachine.m_Animations.IsAnimationNotInProgress("Attack 1") || _stateMachine.m_Animations.IsAnimationNotInProgress("Attack 2") ||
            _stateMachine.m_Animations.IsAnimationNotInProgress("Attack 3");
    }
}
