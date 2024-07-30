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
    m_attackChargeTimer = m_attackChargeInterval;
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      m_attackChargeTimer = m_attackChargeInterval;
    }

  }

  private void OnTriggerStay(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      m_attackChargeTimer -= Time.deltaTime;

      if (m_attackChargeTimer < 0)
      {
        Attack(other.gameObject);
      }
    }

  }
  private void OnTriggerExit(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      m_attackChargeTimer = m_attackChargeInterval;
    }
  }

  private void Attack(GameObject _player)
  {
    _player.GetComponent<PlayerStats>().TakeDamage(m_damage);
    m_attackChargeTimer = m_attackChargeInterval;
  }
  
}
