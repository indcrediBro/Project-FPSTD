using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapFirepit : MonoBehaviour
{
    [SerializeField] private float m_damagePerSecond = 5f;
    [SerializeField] private float m_damageInterval = 0.5f;

    private void Start()
    {
        StartCoroutine(DamageOverTimeRoutine());
    }

    private IEnumerator DamageOverTimeRoutine()
    {
        while (true)
        {
            DamageEnemiesInRange();
            yield return new WaitForSeconds(m_damageInterval);
        }
    }

    private void DamageEnemiesInRange()
    {
        Collider[] hitColliders = Physics.OverlapBox(transform.position, transform.localScale / 2);
        foreach (Collider collider in hitColliders)
        {
            if (collider.TryGetComponent(out Health enemyHealth))
            {
                enemyHealth.TakeDamage(m_damagePerSecond * m_damageInterval);
            }
        }
    }
}
