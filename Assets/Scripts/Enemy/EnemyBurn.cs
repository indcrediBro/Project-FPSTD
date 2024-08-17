using UnityEngine;
using System.Collections;

public class EnemyBurn : MonoBehaviour
{
    public float m_burnDamagePerSecond = 5f;
    private bool m_isBurning = false;
    private EnemyHealth m_health;

    private void Awake()
    {
        m_health = GetComponent<EnemyHealth>();
    }

    public void StartBurning()
    {
        m_isBurning = true;
        InvokeRepeating(nameof(ApplyBurnDamage), 0f, 1f);
    }

    public void StopBurning()
    {
        m_isBurning = false;
        CancelInvoke(nameof(ApplyBurnDamage));
    }

    private void ApplyBurnDamage()
    {
        if (m_isBurning)
        {
            m_health.TakeDamage(m_burnDamagePerSecond);
        }
    }
}
