using UnityEngine;
using System.Collections;

public class SpawnState : IEnemyState
{
    public void EnterState(EnemyStateMachine _stateMachine)
    {
        _stateMachine.m_Animations.PlayAnimation(EnemyState.Spawn);
    }

    public void UpdateState(EnemyStateMachine _stateMachine)
    {
        if (_stateMachine.m_Animations.IsAnimationNotInProgress("Spawn"))
        {
            _stateMachine.TransitionToState(_stateMachine.m_IdleState);
        }
    }

    public void ExitState(EnemyStateMachine _stateMachine)
    {

    }
}
