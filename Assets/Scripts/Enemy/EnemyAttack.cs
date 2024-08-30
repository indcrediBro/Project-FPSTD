using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public bool m_CanAttack;
    public float m_AttackDamage = 10f;
    public float m_AttackRange = 2f;
    private EnemyStats m_stats;

    private void Awake()
    {
        m_stats = GetComponentInParent<EnemyStats>();
    }

    private void OnDisable()
    {
        m_CanAttack = true;
    }

    public void PerformAttack()
    {
        if (m_stats.GetHealth().IsDead() || !m_CanAttack)
        {
            StopAllCoroutines();
            return;
        }
        StartCoroutine(AttackCO(m_stats.GetEnemyStateMachine().m_CurrentTarget));
    }

    private IEnumerator AttackCO(Transform _target)
    {
        m_CanAttack = false;
        yield return null;

        if (m_stats.GetHealth().IsDead() || Vector3.Distance(_target.position, transform.position) > m_AttackRange)
        {
            StopAllCoroutines();
            yield break;
        }

        if (_target.TryGetComponent(out Health targetHealth) && !targetHealth.IsDead())
        {
            Vector3 direction = transform.position - _target.position;
            direction.y = 0;
            transform.rotation = Quaternion.LookRotation(-direction);
            //transform.LookAt(_target);
            targetHealth.TakeDamage(m_AttackDamage);

            if (targetHealth.CompareTag("Player"))
            {
                AudioManager.Instance.PlaySound("SFX_PlayerHit");
            }
            else
            {
                AudioManager.Instance.PlaySound("SFX_BaseHit", transform.position);
            }
        }
        else
        {
            m_stats.GetEnemyStateMachine().TransitionToState(m_stats.GetEnemyStateMachine().m_ChaseState);
            StopAllCoroutines();
            yield break;
        }

        //if (_target.CompareTag("Player") &&
        //    Vector3.Distance(transform.position, _target.position) <= m_stats.GetAttack().m_AttackRange)
        //{
        //    if (!GameReferences.Instance.m_PlayerStats.GetPlayerHealthComponent().IsDead())
        //    {
        //        GameReferences.Instance.m_PlayerStats.GetPlayerHealthComponent().TakeDamage(m_AttackDamage);
        //        AudioManager.Instance.PlaySound("SFX_PlayerHit");
        //    }
        //}
        //else if (_target.CompareTag("PlayerBase"))
        //{
        //    if (!GameReferences.Instance.m_PlayerBase.IsDead())
        //    {
        //        transform.LookAt(_target);
        //        GameReferences.Instance.m_PlayerBase.TakeDamage(m_AttackDamage);
        //        AudioManager.Instance.PlaySound("SFX_BaseHit", transform.position);
        //    }
        //}
        yield return new WaitForSeconds(0.35f);
        m_CanAttack = true;
    }
}
