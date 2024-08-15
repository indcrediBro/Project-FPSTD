using UnityEngine;
using System.Collections;

public class Knife : Weapon
{
    [SerializeField] private float m_hitRadius = 1f;
    [SerializeField] private float m_knockbackForce = 10f;
    [SerializeField] private LayerMask m_enemyLayer;

    private void Update()
    {
        if (!CanAttack())
            return;
        if(InputManager.Instance.m_AttackInput.WasPressedThisFrame())
            PerformAttack();
    }

    public override void Attack()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(m_weaponTransform.position, m_hitRadius, m_enemyLayer);
        foreach (Collider enemy in hitEnemies)
        {
            if (enemy.TryGetComponent(out EnemyStats enemyStats))
            {
                enemyStats.TakeDamage(m_damage);
                Vector3 knockbackDirection;
                Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
                knockbackDirection = ray.direction;
                knockbackDirection.y += m_knockbackForce;
                enemyStats.GetRigidbody().AddForce(knockbackDirection * m_knockbackForce, ForceMode.Impulse);
                Debug.Log("Collided with " + enemyStats.name + " dealing damage of " + m_damage);
            }
        }
    }

    private void PerformAttack()
    {
        DisableCanAttack();
        m_weaponAudioSource.Play();
        if(m_weaponAnimator) PlayAttackAnimation("Attack 0");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(m_weaponTransform.position, m_hitRadius);
    }
}
