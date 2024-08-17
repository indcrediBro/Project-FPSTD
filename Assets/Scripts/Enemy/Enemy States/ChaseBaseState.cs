using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseBaseState : IEnemyState
{
    public void EnterState(EnemyStateMachine _stateMachine)
    {
        _stateMachine.m_Animations.PlayAnimation(EnemyState.ChaseBase);
    }

    public void UpdateState(EnemyStateMachine _stateMachine)
    {
        Transform target;
        target = NavmeshManager.Instance.GetBasePositionFromArray(_stateMachine.transform);

        if (_stateMachine.m_NavigationBaseTarget == null || _stateMachine.m_NavigationBaseTarget != target)
        {
            _stateMachine.SetNavigationalBaseTarget(target);
        }

        _stateMachine.m_Movement.MoveToTarget(_stateMachine.m_BaseTarget.position);

        if (_stateMachine.m_PlayerTarget)
        {
            if (_stateMachine.m_Detection.IsPlayerInRange())
            {
                _stateMachine.TransitionToState(_stateMachine.m_ChasePlayerState);
            }
        }
        
        if (_stateMachine.m_Detection.IsInAttackRange(_stateMachine.m_NavigationBaseTarget))
        {
            _stateMachine.TransitionToState(_stateMachine.m_AttackState);
        }
    }

    public void ExitState(EnemyStateMachine stateMachine)
    {
        stateMachine.m_Movement.StopMoving();
    }
}
