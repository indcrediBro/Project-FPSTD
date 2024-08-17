using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] private Rigidbody m_rigidbody;
    public Rigidbody GetRigidbody() { return m_rigidbody; }

    [SerializeField] private EnemyHealth m_health;
    public EnemyHealth GetHealth() { return m_health; }

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
}
