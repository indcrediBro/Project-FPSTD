using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerWeaponProjectile : MonoBehaviour
{
    private float m_damage;
    [SerializeField] private float m_postImpactDestroyTime = 2f;
    private bool m_isLaunched;

    private Rigidbody m_rigidbody;
    private Collider m_collider;

    private void Awake()
    {
        m_collider = GetComponent<Collider>();
        m_rigidbody = GetComponent<Rigidbody>();
    }

    public void SetDamage(float damageAmount)
    {
        m_damage = damageAmount;
        m_isLaunched = true;
    }

    private void OnTriggerEnter(Collider _other)
    {
        if(m_isLaunched) OnImpact(_other);
    }

    private void OnImpact(Collider _other)
    {
        m_collider.enabled = false;
        m_rigidbody.isKinematic = true;

        Debug.Log("Collided with " + _other.name + " dealing damage of " + m_damage);

        if (_other.TryGetComponent(out EnemyStats _health))
        {
            _health.TakeDamage(m_damage);
            DestroyAfterImpact();
        }

        Invoke(nameof(DestroyAfterImpact),m_postImpactDestroyTime);
    }

    private void DestroyAfterImpact()
    {
        Destroy(gameObject);
        return;
    }
}
