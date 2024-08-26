using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    public float m_detectionRadius = 10f;
    public LayerMask m_targetLayer;

    private Collider[] detectedTargets;
    private EnemyStateMachine m_stateMachine;

    private void Awake()
    {
        m_stateMachine = GetComponent<EnemyStateMachine>();
    }

    public bool IsPlayerInRange()
    {
        detectedTargets = Physics.OverlapSphere(transform.position + transform.forward, m_detectionRadius, m_targetLayer);
        foreach (var target in detectedTargets)
        {
            if (target.CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
    }

    public bool IsInAttackRange(Transform target)
    {
        return Vector3.Distance(transform.position, target.position) <= m_stateMachine.m_Attack.m_attackRange;
    }

}
