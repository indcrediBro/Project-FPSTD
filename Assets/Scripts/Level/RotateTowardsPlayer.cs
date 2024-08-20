using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardsPlayer : MonoBehaviour
{
#nullable enable
    [SerializeField] private Transform? m_target;

    private void Start()
    {
        m_target = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    private void LateUpdate()
    {
        if (m_target != null) {
            Vector3 direction = m_target.position - transform.position;

            direction.y = 0;
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
