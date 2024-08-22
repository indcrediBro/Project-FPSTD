using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : IEnemyState
{
    public void EnterState(EnemyStateMachine _stateMachine)
    {
        _stateMachine.m_Stats.GetCollider().enabled = false;
        _stateMachine.m_Stats.GetRigidbody().isKinematic = true;
        GameObject coin = ObjectPoolManager.Instance.GetPooledObject("Coin");
        coin.transform.position = _stateMachine.transform.position;
        coin.SetActive(true);
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
