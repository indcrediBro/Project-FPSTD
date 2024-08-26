using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public bool m_canAttack;
    public float m_attackDamage = 10f;
    public float m_attackRange = 2f;

    public void PerformAttack(Transform _target)
    {
        StartCoroutine(AttackCO(_target, 0.75f));
    }
    private IEnumerator AttackCO(Transform _target, float _timeToWait)
    {
        m_canAttack = false;
        yield return new WaitForSeconds(_timeToWait);
        if (_target.CompareTag("Player"))
        {
            if (!GameReferences.Instance.m_PlayerStats.GetPlayerHealthComponent().IsDead())
            {
                GameReferences.Instance.m_PlayerStats.GetPlayerHealthComponent().TakeDamage(m_attackDamage);
            }
        }
        else if (_target.CompareTag("PlayerBase"))
        {
            if (!GameReferences.Instance.m_PlayerBase.IsDead())
            {
                transform.LookAt(_target);
                GameReferences.Instance.m_PlayerBase.TakeDamage(m_attackDamage);
            }
        }
        yield return new WaitForSeconds(0.35f);
        m_canAttack = true;
    }
}
