using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float m_damage;
    [SerializeField] private float m_attackChargeInterval;
    private float m_attackChargeTimer;

    private void Start()
    {
        ResetChargeTimer();
    }

    private void ResetChargeTimer()
    {
        m_attackChargeTimer = m_attackChargeInterval;
    }

    private void OnTriggerEnter(Collider _collider)
    {
        if (_collider.CompareTag("Player"))
        {
            HandlePlayerCollisionHit(_collider);
        }
    }

    private void OnTriggerStay(Collider _collider)
    {
        if (_collider.CompareTag("Player"))
        {
            HandlePlayerCollisionStay(_collider);
        }
    }

    private void OnTriggerExit(Collider _collider)
    {
        if (_collider.CompareTag("Player"))
        {
            // Extract this into function
            m_attackChargeTimer = m_attackChargeInterval;
        }
    }

    private void HandlePlayerCollisionHit(Collider _collider)
    {
        ResetChargeTimer();
    }

    private void HandlePlayerCollisionStay(Collider _collider)
    {
        m_attackChargeTimer -= Time.deltaTime;

        if (m_attackChargeTimer < 0)
        {
            Attack(_collider.gameObject);
        }
    }

    private void Attack(GameObject _player)
    {
        if (_player.TryGetComponent(out PlayerStats playerStats))
        {
            playerStats.GetPlayerHealthComponent().TakeDamage(m_damage);
        }
        m_attackChargeTimer = m_attackChargeInterval;
    }
}