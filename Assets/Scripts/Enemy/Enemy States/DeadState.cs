using UnityEngine;

public class DeadState : IEnemyState
{
    public void EnterState(EnemyStateMachine _stateMachine)
    {
        ScoreManager.Instance.AddScore(100);
        EnemyManager.Instance.ReduceActiveEnemyCount(1);

        _stateMachine.m_Stats.GetAudio().PlayDeadSound();
        _stateMachine.m_Stats.GetCollider().enabled = false;
        _stateMachine.m_Stats.GetRigidbody().isKinematic = true;

        SpawnRandomNumberOfCoins(_stateMachine.transform);
        _stateMachine.m_Animations.PlayAnimation(EnemyState.Dead);
    }

    public void UpdateState(EnemyStateMachine _stateMachine)
    {

    }

    public void ExitState(EnemyStateMachine _stateMachine)
    {

    }

    private void SpawnRandomNumberOfCoins(Transform _transform)
    {
        int r = RandomNumber.Instance.NextInt(1, 5);

        for (int i = 0; i < r; i++)
        {
            GameObject coin = ObjectPoolManager.Instance.GetPooledObject("Coin");
            coin.transform.position = _transform.position;
            coin.SetActive(true);
        }
    }
}
