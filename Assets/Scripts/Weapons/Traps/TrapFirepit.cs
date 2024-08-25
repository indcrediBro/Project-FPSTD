using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapFirepit : MonoBehaviour
{
    [SerializeField] private float m_damagePerSecond = 5f;

    private void OnTriggerStay(Collider _other)
    {
        if (_other.TryGetComponent(out EnemyBurn enemy))
        {
            enemy.StartBurning();
        }
    }

    private void OnTriggerExit(Collider _other)
    {
        if (_other.TryGetComponent(out EnemyBurn enemy))
        {
            enemy.StopBurning();
        }
    }
}
