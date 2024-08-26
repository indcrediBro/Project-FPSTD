using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float m_speed = 10f;
    [SerializeField] private string m_vfxName;
    private Transform m_target;
    private float m_damage;
    private float m_blastRadius;
    private Vector3 m_velocity;

    private void OnEnable()
    {
        Invoke(nameof(DeactivateObject), 4f);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(DeactivateObject));
    }

    public void Launch(Transform _targetPosition, float _damage, float _blastRadius = 0f)
    {
        m_target = _targetPosition;
        this.m_damage = _damage;
        this.m_blastRadius = _blastRadius;
    }

    private void Update()
    {
        MoveProjectile();
    }

    private void MoveProjectile()
    {
        Vector3 dir = m_target.position - transform.position;
        float distanceThisFrame = m_speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            OnImpact(m_target.GetComponentInParent<Collider>());
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(m_target);
    }

    private void OnImpact(Collider _other)
    {
        Vector3 closestPoint = transform.position;
        if (m_blastRadius > 0f)
        {
            Collider[] hitColliders = Physics.OverlapSphere(closestPoint, m_blastRadius);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.TryGetComponent(out EnemyHealth enemyHealth))
                {
                    float distance = Vector3.Distance(transform.position, hitCollider.transform.position);
                    float scaledDamage = Mathf.Lerp(m_damage * 2, m_damage / 2, distance / m_blastRadius);
                    enemyHealth.TakeDamage(scaledDamage, false);
                }
            }
        }
        else
        {
            if (_other.TryGetComponent(out EnemyHealth enemyHealth))
            {
                enemyHealth.TakeDamage(m_damage, false);
            }
        }

        GameObject hitImpact = ObjectPoolManager.Instance.GetPooledObject("VFX_Hit" + m_vfxName);
        if (hitImpact != null)
        {
            hitImpact.transform.position = transform.position;
            hitImpact.SetActive(true);
            DeactivateObject();
        }
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.gameObject != this.gameObject)
        {
            OnImpact(_other);
        }
    }

    private void DeactivateObject()
    {
        gameObject.SetActive(false);
    }
}
