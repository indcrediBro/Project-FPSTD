using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardsPlayer : MonoBehaviour
{
    [SerializeField] private Transform m_player;

    private void Awake()
    {
        if(GameReferences.Instance) m_player = GameReferences.Instance.m_PlayerStats.transform;
    }

    private void LateUpdate()
    {
        if (m_player) {
            Vector3 direction = m_player.position - transform.position;

            direction.y = 0;
            transform.rotation = Quaternion.LookRotation(direction);
        }
        else
        {
            Vector3 direction = Camera.main.transform.position - transform.position;

            direction.y = 0;
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
