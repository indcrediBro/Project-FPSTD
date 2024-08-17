using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent m_navMeshAgent;

    private void Awake()
    {
        m_navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void MoveToTarget(Vector3 _target)
    {
        if (_target != null)
        {
            m_navMeshAgent.SetDestination(_target);
            m_navMeshAgent.isStopped = false;
        }
    }

    public void StopMoving()
    {
        m_navMeshAgent.isStopped = true;
    }
}
