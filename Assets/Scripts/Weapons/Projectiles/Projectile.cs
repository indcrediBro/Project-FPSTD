using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float m_speed = 10f;
    [SerializeField] private float m_gravity = -9.81f;
    private Vector3 m_target;
    private float m_damage;
    private float m_blastRadius;
    private bool m_isParabolic;
    private Vector3 m_velocity;

    public void Launch(Vector3 _targetPosition, float _damage, bool _isParabolic = false, float _blastRadius = 0f)
    {
        m_target = _targetPosition;
        this.m_damage = _damage;
        this.m_isParabolic = _isParabolic;
        this.m_blastRadius = _blastRadius;

        if (_isParabolic)
        {
            CalculateParabolicTrajectory();
        }
        else
        {
            m_velocity = (m_target - transform.position).normalized * m_speed;
        }
    }

    private void Update()
    {
        if (m_isParabolic)
        {
            ApplyParabolicMotion();
        }
        else
        {
            transform.position += m_velocity * Time.deltaTime;
        }
    }

    private void CalculateParabolicTrajectory()
    {
        Vector3 direction = m_target - transform.position;
        float distance = direction.magnitude;
        float angle = 45f;
        float radianAngle = Mathf.Deg2Rad * angle;

        float vX = Mathf.Sqrt(distance * Mathf.Abs(m_gravity) / Mathf.Sin(2 * radianAngle));
        m_velocity = new Vector3(direction.x, Mathf.Tan(radianAngle) * distance, direction.z).normalized * vX;
    }

    private void ApplyParabolicMotion()
    {
        m_velocity.y += m_gravity * Time.deltaTime;
        transform.position += m_velocity * Time.deltaTime;
    }

    private void OnImpact()
    {
        if (m_blastRadius > 0f)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, m_blastRadius);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.TryGetComponent(out EnemyHealth enemyHealth))
                {
                    float distance = Vector3.Distance(transform.position, hitCollider.transform.position);
                    float scaledDamage = Mathf.Lerp(m_damage*2, m_damage/2, distance / m_blastRadius);
                    enemyHealth.TakeDamage(scaledDamage);
                    Debug.Log(enemyHealth.name + " took damage of " + scaledDamage);
                }
            }
        }
        else
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.5f);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.TryGetComponent(out EnemyHealth enemyHealth))
                {
                    enemyHealth.TakeDamage(m_damage);
                }
            }
        }

        // Add explosion effects (particles, sound)
        Destroy(gameObject); // Destroy the projectile
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != this.gameObject)
        {
            OnImpact();
        }
    }
}
