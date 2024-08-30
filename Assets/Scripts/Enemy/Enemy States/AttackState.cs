public class AttackState : IEnemyState
{
    public void EnterState(EnemyStateMachine _stateMachine)
    {
        if (_stateMachine.m_CurrentTarget)
        {
            _stateMachine.m_Animations.PlayAnimation(EnemyState.Attack);
            //_stateMachine.m_Attack.PerformAttack();
        }

        //if (_stateMachine.m_PlayerTarget && _stateMachine.m_Detection.IsInAttackRange(_stateMachine.m_PlayerTarget))
        //{
        //    _stateMachine.m_Animations.PlayAnimation(EnemyState.Attack);
        //    _stateMachine.m_Attack.PerformAttack(_stateMachine.m_PlayerTarget);
        //}
        //else if (_stateMachine.m_BaseTarget && _stateMachine.m_Detection.IsInAttackRange(_stateMachine.m_NavigationBaseTarget))
        //{
        //    _stateMachine.m_Animations.PlayAnimation(EnemyState.Attack);
        //    _stateMachine.m_Attack.PerformAttack(_stateMachine.m_BaseTarget);
        //}
    }

    public void UpdateState(EnemyStateMachine _stateMachine)
    {
        if (IsNotPlayingAnimations(_stateMachine))
        {
            if (_stateMachine.m_CurrentTarget && _stateMachine.m_Detection.IsInAttackRange(_stateMachine.m_CurrentTarget))
            {
                _stateMachine.m_Animations.PlayAnimation(EnemyState.Attack);
                //_stateMachine.m_Attack.PerformAttack();
            }
            else
            {
                _stateMachine.TransitionToState(_stateMachine.m_ChaseState);
            }

            //if (_stateMachine.m_PlayerTarget && _stateMachine.m_Detection.IsInAttackRange(_stateMachine.m_PlayerTarget))
            //{
            //    _stateMachine.m_Animations.PlayAnimation(EnemyState.Attack);
            //    _stateMachine.m_Attack.PerformAttack(_stateMachine.m_PlayerTarget);
            //}
            //else if (_stateMachine.m_PlayerTarget && _stateMachine.m_BaseTarget &&
            //    !_stateMachine.m_Detection.IsInAttackRange(_stateMachine.m_PlayerTarget) &&
            //    !_stateMachine.m_Detection.IsInAttackRange(_stateMachine.m_NavigationBaseTarget))
            //{
            //    _stateMachine.TransitionToState(_stateMachine.m_ChaseBaseState);
            //}
            //else if (_stateMachine.m_BaseTarget && _stateMachine.m_Detection.IsInAttackRange(_stateMachine.m_NavigationBaseTarget))
            //{
            //    _stateMachine.m_Animations.PlayAnimation(EnemyState.Attack);
            //    _stateMachine.m_Attack.PerformAttack(_stateMachine.m_BaseTarget);
            //}
            //else if (_stateMachine.m_BaseTarget && !_stateMachine.m_Detection.IsInAttackRange(_stateMachine.m_NavigationBaseTarget))
            //{
            //    _stateMachine.TransitionToState(_stateMachine.m_ChaseBaseState);
            //}
        }
    }

    public void ExitState(EnemyStateMachine _stateMachine)
    {

    }

    private bool IsNotPlayingAnimations(EnemyStateMachine _stateMachine)
    {
        return _stateMachine.m_Animations.IsAnimationNotInProgress("Burn") ||
               _stateMachine.m_Animations.IsAnimationNotInProgress("Dead") ||
               _stateMachine.m_Animations.IsAnimationNotInProgress("Idle") ||
               _stateMachine.m_Animations.IsAnimationNotInProgress("Attack 0") ||
               _stateMachine.m_Animations.IsAnimationNotInProgress("Attack 1") ||
               _stateMachine.m_Animations.IsAnimationNotInProgress("Attack 2") ||
               _stateMachine.m_Animations.IsAnimationNotInProgress("Attack 3");
    }
}
