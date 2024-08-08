using UnityEngine;

public class PoolObject : MonoBehaviour
{
    public PoolSystem PoolSystem;
    [SerializeField] private float m_lifeTime;

    [Header("=====Debug Purpose=====")]
    [SerializeField] private float m_disableTimer;


    private void Start()
    {
        m_disableTimer = m_lifeTime;
    }
    private void OnEnable()
    {
        m_disableTimer = m_lifeTime;
    }


    private void Update()
    {
        m_disableTimer -= Time.deltaTime;
        if (m_disableTimer < 0)
        {
            Die();
        }
    }

    private void Die()
    {
        gameObject.SetActive(false);
        if (PoolSystem.CurrentActivObjects.Contains(gameObject))
        {
            PoolSystem.CurrentActivObjects.Remove(gameObject);
        }
    }
}
