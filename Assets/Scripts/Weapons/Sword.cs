using UnityEngine;
using System.Collections;

public class Sword : Weapon
{
    [SerializeField] private float m_hitRadius = 1.5f;
    [SerializeField] private float m_knockbackForce = 1500f;
    [SerializeField] private LayerMask m_enemyLayer;

    private int m_comboCounter = 1;
    private float m_comboResetTime = 1f;
    private float m_lastAttackTime;

    private void Update()
    {
        if (!CanAttack())
            return;
        if (InputManager.Instance.m_AttackInput.WasPressedThisFrame())
            PerformComboAttack();
    }

    //TODO: Use Better Detection System
    public override void Attack()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(m_weaponTransform.position, m_hitRadius, m_enemyLayer);
        foreach (Collider enemy in hitEnemies)
        {
            if (enemy.TryGetComponent(out EnemyStats enemyStats))
            {
                enemyStats.GetHealth().TakeDamage(m_damage * (m_comboCounter + 1));

                Vector3 knockbackDirection;
                Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
                knockbackDirection = ray.direction;
                knockbackDirection.y += m_knockbackForce;
                enemyStats.GetRigidbody().AddForce(knockbackDirection * m_knockbackForce, ForceMode.Impulse);
                Debug.Log("Collided with " + enemyStats.name + " dealing damage of " + m_damage);
            }
        }
    }

    private void PerformComboAttack()
    {
        DisableCanAttack();
        m_weaponAudioSource.Play();

        m_comboCounter = (m_comboCounter + 1) % 2;

        string animationName = "Attack " + m_comboCounter;
        PlayAttackAnimation(animationName);


        m_lastAttackTime = Time.time;
        Invoke(nameof(ResetCombo), m_comboResetTime);
    }

    private void ResetCombo()
    {
        if (Time.time - m_lastAttackTime > m_comboResetTime)
        {
            m_comboCounter = 0;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(m_weaponTransform.position, m_hitRadius);
    }
}
