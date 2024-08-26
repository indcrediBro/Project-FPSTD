using UnityEngine;
using System.Collections;

public class EnemyBurn : MonoBehaviour
{
    private EnemyHealth m_health;
    public float m_burnDamagePerSecond = 5f;
    public bool m_isBurning = false;
    [SerializeField] private GameObject m_burnParticles;

    private void Awake()
    {
        m_health = GetComponent<EnemyHealth>();
    }

    public void StartBurning()
    {
        if (!m_isBurning)
        {
            m_isBurning = true;
            m_burnParticles.SetActive(true);
        }
    }

    private void OnDisable()
    {
        StopBurning();
    }

    public void StopBurning()
    {
        m_isBurning = false;
        m_burnParticles.SetActive(false);
    }

    public void ApplyBurnDamage()
    {
        if (m_isBurning && !m_health.IsDead())
        {
            m_health.TakeDamage(m_burnDamagePerSecond, true);
        }
    }
}

