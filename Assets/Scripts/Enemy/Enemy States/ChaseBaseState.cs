using UnityEngine;

public class ChaseBaseState : IEnemyState
{
    public void EnterState(EnemyStateMachine _stateMachine)
    {
        _stateMachine.m_Animations.PlayAnimation(EnemyState.ChaseBase);
        _stateMachine.m_Stats.GetAudio().PlayChaseSound();
    }

    public void UpdateState(EnemyStateMachine _stateMachine)
    {
        if (GameReferences.Instance.m_IsGameOver) return;

        Transform target;
        target = NavmeshManager.Instance.GetBasePositionFromArray(_stateMachine.transform);

        if (_stateMachine.m_NavigationBaseTarget == null || _stateMachine.m_NavigationBaseTarget != target)
        {
            _stateMachine.SetNavigationalBaseTarget(target);
        }

        if (_stateMachine.m_Movement)
        {
            if (target)
            {
                _stateMachine.m_Movement.MoveToTarget(target.position);
            }
            else
            {
                _stateMachine.m_Stats.GetAudio().PlayAlertSound();
                _stateMachine.TransitionToState(_stateMachine.m_ChasePlayerState);
            }
        }

        if (_stateMachine.m_PlayerTarget && _stateMachine.m_Detection.IsPlayerInRange())
        {
            _stateMachine.m_Stats.GetAudio().PlayAlertSound();
            _stateMachine.TransitionToState(_stateMachine.m_ChasePlayerState);
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
