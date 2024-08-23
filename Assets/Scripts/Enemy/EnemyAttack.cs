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
        if (_target.CompareTag("Player"))
        {
            if (!GameReferences.Instance.m_PlayerStats.GetPlayerHealthComponent().IsDead())
                GameReferences.Instance.m_PlayerStats.GetPlayerHealthComponent().TakeDamage(m_attackDamage);
        }
        else if (_target.CompareTag("PlayerBase"))
        {
            if (!GameReferences.Instance.m_PlayerBase.IsDead())
            {
                transform.LookAt(_target);
                GameReferences.Instance.m_PlayerBase.TakeDamage(m_attackDamage);
            }
        }
    }
}
