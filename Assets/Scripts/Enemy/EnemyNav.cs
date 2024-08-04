using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNav : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private GameObject target;

    private void Awake()
    {
        InitializeAgent();
    }

    private void InitializeAgent()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        int randomNumber = Random.Range(0, 101);

        switch (randomNumber)
        {
            case < 1:
                // Find an alternative to FindGameObjectWithTag
                target = GameObject.FindGameObjectWithTag("WeaponDealer");
                break;
            case > 1 and < 20:
                // Find an alternative to FindGameObjectWithTag
                target = GameObject.FindGameObjectWithTag("Player");
                break;
            default:
                // Find an alternative to FindGameObjectWithTag
                target = GameObject.FindGameObjectWithTag("King");
                break;
        }
    }

    private void Update()
    {
        HandleAgentMovement();
    }

    private void HandleAgentMovement()
    {
        if (target)
        {
            navMeshAgent.SetDestination(target.transform.position);
            navMeshAgent.isStopped = false;
        }
        else navMeshAgent.isStopped = true;
    }
}
