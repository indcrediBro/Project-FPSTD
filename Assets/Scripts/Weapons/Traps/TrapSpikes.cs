using System.Collections;
using UnityEngine;

public class TrapSpikes : MonoBehaviour
{
    [SerializeField] private float m_damage = 10f;
    [SerializeField] private float m_interval = 2f; // Time interval between movements
    [SerializeField] private float m_spikeSpeed = 2f; // Speed at which spikes move
    [SerializeField] private Transform m_spikesTransform;
    [SerializeField] private Vector3 m_raisedPosition;
    [SerializeField] private Vector3 m_loweredPosition;
    [SerializeField] private AudioSource m_audioSource;

    private Collider m_collider;
    private bool isRaised = false;

    private void Start()
    {
        m_spikesTransform.localPosition = m_loweredPosition;
        m_collider = GetComponent<Collider>();
        StartCoroutine(SpikeMovementRoutineCO());
    }

    private IEnumerator SpikeMovementRoutineCO()
    {
        while (true)
        {
            isRaised = !isRaised;
            Vector3 startPosition = isRaised ? m_loweredPosition : m_raisedPosition;
            Vector3 targetPosition = isRaised ? m_raisedPosition : m_loweredPosition;
            float elapsedTime = 0f;

            while (elapsedTime < m_interval)
            {
                m_spikesTransform.localPosition = Vector3.Lerp(startPosition, targetPosition, elapsedTime / m_interval);
                elapsedTime += Time.deltaTime * m_spikeSpeed;
                yield return null;
            }

            m_spikesTransform.localPosition = targetPosition; // Ensure it reaches the target position

            yield return new WaitForSeconds(isRaised ? m_interval / 2 : m_interval);
            m_collider.enabled = isRaised; // Collider is enabled only when spikes are raised
            m_audioSource.pitch = RandomNumber.Instance.NextFloat(0.85f, 1.25f);
            m_audioSource.Play();
        }
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (m_collider.enabled && _other.TryGetComponent(out EnemyHealth enemyHealth))
        {
            enemyHealth.TakeDamage(m_damage, false);
        }
    }
}
