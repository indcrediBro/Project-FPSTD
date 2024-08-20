using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] private EnemyManager.EnemyNames m_enemyName;
    [SerializeField] private Rigidbody m_rigidbody;
    [SerializeField] private EnemyHealth m_health;

    public Rigidbody GetRigidbody()
    {
        return m_rigidbody;
    }

    public EnemyHealth GetHealth()
    {
        return m_health;
    }

    private void OnEnable()
    {
        EnemyManager.Instance.AddEnemyToList(gameObject, m_enemyName);
    }

    private void Start()
    {
        if (m_rigidbody == null)
        {
            m_rigidbody = GetComponent<Rigidbody>();
        }

        if (m_health == null)
        {
            m_health = GetComponent<EnemyHealth>();
        }
    }

    private void OnDisable()
    {
        EnemyManager.Instance.RemoveEnemyFromList(gameObject, m_enemyName);
    }
}