using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : IEnemyState
{
    public void EnterState(EnemyStateMachine _stateMachine)
    {
        _stateMachine.m_Animations.PlayAnimation(EnemyState.Dead);
    }

    public void UpdateState(EnemyStateMachine _stateMachine)
    {
        // Possible logic for corpse decay or despawning
    }

    public void ExitState(EnemyStateMachine _stateMachine)
    {
        // Cleanup logic if needed before removing the enemy
    }
}
