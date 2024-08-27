using UnityEngine;
using System.Collections;

public class Knife : Weapon
{
    [SerializeField] private float m_hitRadius = 1f;
    //[SerializeField] private float m_knockbackForce = 10f;
    [SerializeField] private LayerMask m_enemyLayer;
    private PlayerUIController m_playerUI;

    private void Awake()
    {
        m_playerUI = GameReferences.Instance.m_PlayerStats.GetComponent<PlayerUIController>();
    }

    private void OnEnable()
    {
        m_playerUI.UpdateAmmoText("");
    }

    private void Update()
    {
        if (!CanAttack() || GameReferences.Instance.m_IsPaused) return;

        if (InputManager.Instance.m_AttackInput.WasPressedThisFrame()) PerformAttack();
    }

    public override void Attack()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(m_weaponTransform.position, m_hitRadius, m_enemyLayer);
        foreach (Collider enemy in hitEnemies)
        {
            if (enemy.TryGetComponent(out EnemyStats enemyStats))
            {
                Vector3 closestPoint = enemy.ClosestPoint(transform.position);
                GameObject hitImpact = ObjectPoolManager.Instance.GetPooledObject("VFX_HitKnife");
                if (hitImpact != null)
                {
                    hitImpact.transform.position = closestPoint;
                    hitImpact.SetActive(true);
                }
                enemyStats.GetHealth().TakeDamage(GetCurrentDamage(), false);
                //Vector3 knockbackDirection;
                //Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
                //knockbackDirection = ray.direction;
                //knockbackDirection.y += m_knockbackForce;
                //enemyStats.GetRigidbody().AddForce(knockbackDirection * m_knockbackForce, ForceMode.Impulse);
            }
        }
        if (hitEnemies.Length > 0)
        {
            GameReferences.Instance.m_CameraShake.TriggerShake();
        }
    }

    private void PerformAttack()
    {
        DisableCanAttack();
        //m_weaponAudioSource.Play();
        if (m_weaponAnimator) PlayAnimation("Attack 0");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(m_weaponTransform.position, m_hitRadius);
    }
}
