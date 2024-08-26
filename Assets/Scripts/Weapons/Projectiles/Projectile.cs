using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float m_speed = 10f;
    [SerializeField] private float m_gravity = -9.81f;
    [SerializeField] private string m_vfxName;
    private Vector3 m_target;
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

    public void Launch(Vector3 _targetPosition, float _damage, float _blastRadius = 0f)
    {
        m_target = _targetPosition;
        this.m_damage = _damage;
        this.m_blastRadius = _blastRadius;


        m_velocity = transform.forward * m_speed;
    }

    private void Update()
    {
        ApplyParabolicMotion();
    }

    //private void CalculateParabolicTrajectory()
    //{
    //    Vector3 direction = m_target - transform.position;
    //    float distance = direction.magnitude;
    //    float angle = 45f;
    //    float radianAngle = Mathf.Deg2Rad * angle;

    //    float vX = Mathf.Sqrt(distance * Mathf.Abs(m_gravity) / Mathf.Sin(2 * radianAngle));
    //    m_velocity = new Vector3(direction.x, Mathf.Tan(radianAngle) * distance, direction.z).normalized * vX;
    //}

    private void ApplyParabolicMotion()
    {
        m_velocity.y += m_gravity * Time.deltaTime;
        transform.position += m_velocity * Time.deltaTime;
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
