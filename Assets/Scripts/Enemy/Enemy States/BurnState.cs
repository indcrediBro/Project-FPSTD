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
        else if (_stateMachine.m_Animations.IsAnimationNotInProgress("Hurt") || _stateMachine.m_Animations.IsAnimationNotInProgress("Burn"))
        {
            if (_stateMachine.m_PlayerTarget && _stateMachine.m_Detection.IsPlayerInRange())
            {
                _stateMachine.m_Stats.GetAudio().PlayAlertSound();
                _stateMachine.TransitionToState(_stateMachine.m_ChasePlayerState);
            }
            else
            {
                _stateMachine.TransitionToState(_stateMachine.m_ChaseBaseState);
            }
        }
    }

    public void ExitState(EnemyStateMachine _stateMachine)
    {
        //_stateMachine.m_Burn.StopBurning();
    }
}
