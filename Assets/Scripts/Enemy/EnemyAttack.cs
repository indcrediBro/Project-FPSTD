using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public bool m_canAttack;
    public float m_attackDamage = 10f;
    public float m_attackRange = 2f;
    private EnemyStats m_stats;

    private void Awake()
    {
        m_stats = GetComponent<EnemyStats>();
    }

    private void OnDisable()
    {
        m_canAttack = true;
    }

    public void PerformAttack(Transform _target)
    {
        if (m_stats.GetHealth().IsDead())
        {
            StopAllCoroutines();
            return;
        }
        StartCoroutine(AttackCO(_target, 0.75f));
    }

    private IEnumerator AttackCO(Transform _target, float _timeToWait)
    {
        m_canAttack = false;
        yield return new WaitForSeconds(_timeToWait);

        if (m_stats.GetHealth().IsDead())
        {
            StopAllCoroutines();
        }

        if (_target.CompareTag("Player") &&
            Vector3.Distance(transform.position, _target.position) <= m_stats.GetAttack().m_attackRange)
        {
            if (!GameReferences.Instance.m_PlayerStats.GetPlayerHealthComponent().IsDead())
            {
                transform.LookAt(_target);
                GameReferences.Instance.m_PlayerStats.GetPlayerHealthComponent().TakeDamage(m_attackDamage);
                AudioManager.Instance.PlaySound("SFX_PlayerHit");
            }
        }
        else if (_target.CompareTag("PlayerBase"))
        {
            if (!GameReferences.Instance.m_PlayerBase.IsDead())
            {
                transform.LookAt(_target);
                GameReferences.Instance.m_PlayerBase.TakeDamage(m_attackDamage);
                AudioManager.Instance.PlaySound("SFX_BaseHit", transform.position);
            }
        }
        yield return new WaitForSeconds(0.35f);
        m_canAttack = true;
    }
}
