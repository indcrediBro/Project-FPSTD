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
        if (m_isLaunched) OnImpact(_other);
    }

    private void OnDisable()
    {
        m_collider.enabled = true;
    }

    private void OnImpact(Collider _other)
    {
        m_collider.enabled = false;
        m_rigidbody.isKinematic = true;

        GameObject hitImpact = ObjectPoolManager.Instance.GetPooledObject("VFX_HitArrow");
        if (hitImpact != null)
        {
            hitImpact.transform.position = transform.position;
            hitImpact.SetActive(true);
        }

        if (_other.TryGetComponent(out EnemyStats _enemy))
        {
            _enemy.GetHealth().TakeDamage(m_damage);
            DestroyAfterImpact();
        }

        Invoke(nameof(DestroyAfterImpact), m_postImpactDestroyTime);
    }

    private void DestroyAfterImpact()
    {
        transform.SetParent(null);
        gameObject.SetActive(false);
    }
}
