public class ChasePlayerState : IEnemyState
{
    public void EnterState(EnemyStateMachine _stateMachine)
    {
        _stateMachine.m_Animations.PlayAnimation(EnemyState.ChasePlayer);
    }

    public void UpdateState(EnemyStateMachine _stateMachine)
    {
        if (GameReferences.Instance.m_IsGameOver) return;

        _stateMachine.m_Movement.MoveToTarget(_stateMachine.m_PlayerTarget.position);

        if (_stateMachine.m_PlayerTarget)
        {
            if (!_stateMachine.m_Detection.IsPlayerInRange())
            {
                _stateMachine.TransitionToState(_stateMachine.m_IdleState);
            }

            if (_stateMachine.m_Detection.IsInAttackRange(_stateMachine.m_PlayerTarget))
            {
                _stateMachine.TransitionToState(_stateMachine.m_AttackState);
            }
        }
    }

    public void ExitState(EnemyStateMachine _stateMachine)
    {
        _stateMachine.m_Movement.StopMoving();
    }
}
