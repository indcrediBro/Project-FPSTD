using UnityEngine;
using System.Collections;

public abstract class TrapBase : MonoBehaviour
{
    [SerializeField] protected float m_fireRate;
    [SerializeField] protected float m_damage;
    [SerializeField] protected float m_detectionRange;
    [SerializeField] protected LayerMask m_detectionLayer;
    [SerializeField] protected Transform m_rotationObject;
    [SerializeField] protected Transform m_firePoint;

    [SerializeField] protected float m_yOffset = .5f;
    [SerializeField] private float m_recoilDistance = 0.1f;
    [SerializeField] private float m_recoilDuration = 0.1f;

    [SerializeField] protected GameObject m_projectile;
    protected float m_fireCooldown = 0f;
    protected Transform m_currentTarget;
    private Vector3 m_originalPosition;

    protected virtual void Start()
    {
        m_originalPosition = transform.localPosition;
    }

    protected virtual void Update()
    {
        if (m_fireCooldown > 0)
        {
            m_fireCooldown -= Time.deltaTime;
        }

        LookForTarget();
    }

    protected abstract void Fire();
    private float m_currentTargetDistance;
    protected virtual void LookForTarget()
    {
        const int maxColliders = 20;
        Collider[] colliders = new Collider[maxColliders];
        int size = Physics.OverlapSphereNonAlloc(transform.position, m_detectionRange, colliders, m_detectionLayer);

        Transform closestEnemy = null;
        float distanceToCurrentTarget = m_detectionRange;

        for (int i = 0; i < size; i++)
        {
            if (!colliders[i].TryGetComponent(out EnemyStats enemy))
                continue;

            float distanceToSelf = Vector3.Distance(enemy.transform.position, transform.position);
            if (distanceToSelf < distanceToCurrentTarget)
            {
                closestEnemy = enemy.transform;
                distanceToCurrentTarget = distanceToSelf;
            }
        }

        if (closestEnemy != null)
        {
            m_currentTarget = closestEnemy;
            RotateTowardsTarget();

            if (m_fireCooldown <= 0f)
            {
                Fire();
            }
        }
        else
        {
            m_currentTarget = null; 
        }
    }

    protected void RotateTowardsTarget()
    {
        if (m_currentTarget == null) return;

        Vector3 direction = (m_currentTarget.position - transform.position).normalized;
        //direction.y += m_yOffset;
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        m_rotationObject.rotation = Quaternion.Slerp(m_rotationObject.rotation, lookRotation, Time.deltaTime * 5f);
    }

    protected void ApplyRecoil()
    {
        StartCoroutine(RecoilCoroutineCO());
    }

    private IEnumerator RecoilCoroutineCO()
    {
        Vector3 recoilPosition = m_originalPosition - transform.forward * m_recoilDistance;
        float elapsed = 0f;

        while (elapsed < m_recoilDuration)
        {
            transform.localPosition = Vector3.Lerp(m_originalPosition, recoilPosition, elapsed / m_recoilDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        elapsed = 0f;
        while (elapsed < m_recoilDuration)
        {
            transform.localPosition = Vector3.Lerp(recoilPosition, m_originalPosition, elapsed / m_recoilDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = m_originalPosition;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, m_detectionRange);
        if(m_currentTarget)
        {
            Gizmos.color = Color.red;
        }
        else
        {
            Gizmos.color = Color.white;
        }
    }
}

