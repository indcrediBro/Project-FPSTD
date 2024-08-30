using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public bool m_CanAttack;
    public float m_AttackDamage = 10f;
    public float m_AttackRange = 2f;
    private EnemyStats m_stats;
    private Collider[] _colliders;

    private void Awake()
    {
        m_stats = GetComponentInParent<EnemyStats>();
    }

    private void OnEnable()
    {
        m_CanAttack = true;
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

        _colliders = Physics.OverlapSphere(transform.position, m_AttackRange);

        foreach (Collider collider in _colliders)
        {
            if (collider.TryGetComponent(out Health targetHealth) && !targetHealth.IsDead())
            {
                if (collider.CompareTag("Player") || collider.CompareTag("PlayerBase"))
                {
                    StartCoroutine(AttackCO(targetHealth));
                    return;
                }
            }
        }
    }

    private IEnumerator AttackCO(Health targetHealth)
    {
        m_CanAttack = false;
        yield return null;

        if (m_stats.GetHealth().IsDead())
        {
            StopAllCoroutines();
            yield break;
        }

        Vector3 direction = m_stats.transform.position - targetHealth.transform.position;
        direction.y = 0;
        m_stats.transform.rotation = Quaternion.LookRotation(direction * -1);

        if (m_stats.GetEnemyStateMachine().m_CurrentTarget == m_stats.GetEnemyStateMachine().m_BaseTarget)
        {
            GameReferences.Instance.m_PlayerBase.TakeDamage(m_AttackDamage);
            AudioManager.Instance.PlaySound("SFX_BaseHit", transform.position);
        }
        else if (Vector3.Distance(targetHealth.transform.position, m_stats.transform.position) > m_AttackRange)
        {
            targetHealth.TakeDamage(m_AttackDamage);
        }

        if (targetHealth.CompareTag("Player"))
        {
            AudioManager.Instance.PlaySound("SFX_PlayerHit");
        }

        yield return new WaitForSeconds(0.35f);
        m_CanAttack = true;
    }
}
