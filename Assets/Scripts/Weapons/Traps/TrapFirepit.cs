using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapFirepit : MonoBehaviour
{
    [SerializeField] private float m_damagePerSecond = 5f;
    [SerializeField] private float m_burnRate = 1.5f; // Time between burns
    [SerializeField] private AudioSource m_audioSource;

    private void OnEnable()
    {
        m_audioSource.Play();
    }

    private void OnDisable()
    {
        m_audioSource.Stop();
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (!_other.CompareTag("Enemy")) return;

        if (_other.TryGetComponent(out EnemyBurn enemy))
        {
            if (!enemy.m_isBurning && !enemy.GetComponent<EnemyStats>().GetHealth().IsDead())
            {
                StartCoroutine(BurnCO(enemy));
            }
        }
    }

    private void OnTriggerExit(Collider _other)
    {
        if (_other.TryGetComponent(out EnemyBurn enemy))
        {
            StopCoroutine(nameof(BurnCO));
            enemy.StopBurning();
        }
    }

    private IEnumerator BurnCO(EnemyBurn _enemy)
    {
        _enemy.m_isBurning = true;
        _enemy.m_burnDamagePerSecond = m_damagePerSecond;
        _enemy.StartBurning();

        while (_enemy.m_isBurning)
        {
            _enemy.ApplyBurnDamage();
            yield return new WaitForSeconds(m_burnRate); // Delay between each burn
        }
    }
}


