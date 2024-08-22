using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float m_attackDamage = 10f;
    public float m_attackRange = 2f;

    public void PerformAttack(Transform _target)
    {
        StartCoroutine(AttackCO(_target, 0.75f));
    }
    private IEnumerator AttackCO(Transform _target, float _timeToWait)
    {
        yield return new WaitForSeconds(_timeToWait);
        // Logic to deal damage to the target
        if (_target.CompareTag("Player"))
        {
            _target.GetComponent<PlayerHealth>().TakeDamage(m_attackDamage);
        }
        else if (_target.CompareTag("PlayerBase"))
        {
            transform.LookAt(_target);
            _target.GetComponent<PlayerBaseHealth>().TakeDamage(m_attackDamage);
        }
    }
}
