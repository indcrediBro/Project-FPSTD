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
    private PlayerUIController m_playerUI;

    private void Awake()
    {
        m_playerUI = GetComponentInParent<PlayerUIController>();
    }

    private void OnEnable()
    {
        m_playerUI?.UpdateAmmoText("");
    }

    private void Update()
    {
        if (!CanAttack()) return;

        if (InputManager.Instance.m_AttackInput.WasPressedThisFrame()) PerformComboAttack();
    }

    //TODO: Use Better Detection System
    public override void Attack()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(m_weaponTransform.position, m_hitRadius, m_enemyLayer);
        foreach (Collider enemy in hitEnemies)
        {
            if (enemy.TryGetComponent(out EnemyStats enemyStats))
            {
                Vector3 closestPoint = enemy.ClosestPoint(transform.position);
                GameObject hitImpact = ObjectPoolManager.Instance.GetPooledObject("VFX_HitSword");
                if (hitImpact != null)
                {
                    hitImpact.transform.position = closestPoint;
                    hitImpact.SetActive(true);
                }
                enemyStats.GetHealth().TakeDamage(GetCurrentDamage() * (m_comboCounter + 1), false);
                //Vector3 knockbackDirection;
                //Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
                //knockbackDirection = ray.direction;
                //knockbackDirection.y += m_knockbackForce;
                //enemyStats.GetRigidbody().AddForce(knockbackDirection * m_knockbackForce, ForceMode.Impulse);
            }
        }
    }

    private void PerformComboAttack()
    {
        DisableCanAttack();
        //m_weaponAudioSource.Play();

        m_comboCounter = (m_comboCounter + 1) % 2;

        string animationName = "Attack " + m_comboCounter;
        PlayAnimation(animationName);


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
