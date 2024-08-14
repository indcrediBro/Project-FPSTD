using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSpikes : MonoBehaviour
{
    [SerializeField] private float m_damage = 10f;
    [SerializeField] private float m_interval = 2f;
    [SerializeField] private float m_spikeSpeed = 2f;
    [SerializeField] private Transform m_spikesTransform;
    [SerializeField] private Vector3 m_raisedPosition;
    [SerializeField] private Vector3 m_loweredPosition;

    private bool isRaised = false;

    private void Start()
    {
        StartCoroutine(SpikeMovementRoutineCO());
    }

    private IEnumerator SpikeMovementRoutineCO()
    {
        while (true)
        {
            isRaised = !isRaised;
            Vector3 targetPosition = isRaised ? m_raisedPosition : m_loweredPosition;
            float elapsedTime = 0f;

            while (elapsedTime < m_interval)
            {
                m_spikesTransform.localPosition = Vector3.Lerp(m_spikesTransform.localPosition, targetPosition, elapsedTime / m_interval);
                elapsedTime += Time.deltaTime * m_spikeSpeed;
                yield return null;
            }

            if (isRaised)
            {
                DamageEnemiesAbove();
            }

            yield return new WaitForSeconds(m_interval);
        }
    }

    private void DamageEnemiesAbove()
    {
        Collider[] hitColliders = Physics.OverlapBox(m_spikesTransform.position, m_spikesTransform.localScale / 2);
        foreach (Collider collider in hitColliders)
        {
            if (collider.TryGetComponent(out Health enemyHealth))
            {
                enemyHealth.TakeDamage(m_damage);
            }
        }
    }
}
