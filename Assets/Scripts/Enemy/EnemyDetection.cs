using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    public float m_detectionRadius = 10f;
    public LayerMask m_targetLayer;
    public Transform m_attackPoint;

    private EnemyStateMachine m_stateMachine;

    private void Awake()
    {
        m_stateMachine = GetComponent<EnemyStateMachine>();
    }

    public bool IsTargetInRange(Transform target)
    {
        if (target == null) return false;
        return Vector3.Distance(transform.position, target.position) <= m_detectionRadius * 2;
    }

    public bool IsInAttackRange(Transform target)
    {
        return Vector3.Distance(m_attackPoint.position, target.position) <= m_stateMachine.m_Attack.m_AttackRange / 2;
    }

    public Transform GetNearestTarget()
    {
        const int maxColliders = 100;
        Collider[] colliders = new Collider[maxColliders];
        int size = Physics.OverlapSphereNonAlloc(transform.position, 100f, colliders, m_targetLayer);

        Transform closestEnemy = null;
        float distanceToCurrentTarget = m_detectionRadius;

        for (int i = 0; i < size; i++)
        {
            if (!colliders[i].TryGetComponent(out Health target) && colliders[i].TryGetComponent(out EnemyHealth nontarget) &&
                !colliders[i].gameObject.activeInHierarchy)
                continue;

            if (target)
            {
                float distanceToSelf = Vector3.Distance(target.transform.position, transform.position);
                if (distanceToSelf < distanceToCurrentTarget)
                {
                    closestEnemy = target.transform;
                    distanceToCurrentTarget = distanceToSelf;
                }
            }
        }
        return closestEnemy;
    }

    private void OnDrawGizmosSelected()
    {
        //Gizmos.DrawSphere()
    }
}
