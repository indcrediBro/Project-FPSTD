public interface IEnemyState
{
    void EnterState(EnemyStateMachine stateMachine);
    void UpdateState(EnemyStateMachine stateMachine);
    void ExitState(EnemyStateMachine stateMachine);
}