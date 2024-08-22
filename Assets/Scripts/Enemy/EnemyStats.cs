using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] private EnemyStateMachine m_stateMachine;
    [SerializeField] private EnemyManager.EnemyNames m_enemyName;

    [SerializeField] private Collider m_collider;
    [SerializeField] private Rigidbody m_rigidbody;
    [SerializeField] private EnemyHealth m_health;
    [SerializeField] private EnemyAttack m_attack;

    public Collider GetCollider() { return m_collider; }
    public Rigidbody GetRigidbody() { return m_rigidbody; }
    public EnemyHealth GetHealth() { return m_health; }
    public EnemyAttack GetAttack() { return m_attack; }

    private void OnEnable()
    {
        OnObjectSpawn();
    }

    private void Start()
    {
        InitialiseReferences();
    }

    private void InitialiseReferences()
    {
        if (m_collider == null) { m_collider = GetComponent<Collider>(); }
        if (m_rigidbody == null) { m_rigidbody = GetComponent<Rigidbody>(); }
        if (m_health == null) { m_health = GetComponent<EnemyHealth>(); }
        if (m_attack == null) { m_attack = GetComponent<EnemyAttack>(); }
        if (m_stateMachine == null) { m_stateMachine = GetComponent<EnemyStateMachine>(); }
    }

    private void OnDisable()
    {
        OnObjectDespawn();
    }

    public void OnObjectSpawn()
    {
        if (EnemyManager.Instance != null)
        {
            EnemyManager.Instance.AddEnemyToList(gameObject, m_enemyName);
        }
        int currentWave = WaveManager.Instance.GetCurrentWave();

        UpgradeStats(currentWave);

        if (m_stateMachine.m_SpawnState != null && m_stateMachine.m_Animations)
        {
            m_stateMachine.TransitionToState(m_stateMachine.m_SpawnState);
        }
    }

    public void OnObjectDespawn()
    {
        if (EnemyManager.Instance != null)
        {
            EnemyManager.Instance.RemoveEnemyFromList(gameObject, m_enemyName);
        }
    }

    public void UpgradeStats(int wave)
    {
        m_health.SetMaxHealth(wave * 10f);
        m_attack.m_attackDamage += wave * 2f;
    }
}