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
        navMeshAgent = GetComponent<NavMeshAgent>();

        int randomNumber = Random.Range(0, 101);

        if (randomNumber < 1)
        {
            target = GameObject.FindGameObjectWithTag("WeaponDealer");
        }
        else if (randomNumber > 1 && randomNumber < 20)
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }
        else
        {
            target = GameObject.FindGameObjectWithTag("King");
        }
    }

    void Update()
    {
        if (target)
        {
            navMeshAgent.SetDestination(target.transform.position);
            navMeshAgent.isStopped = false;
        }
        else navMeshAgent.isStopped = true;
    }
}
