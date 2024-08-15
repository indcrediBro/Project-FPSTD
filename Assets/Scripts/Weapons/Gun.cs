using UnityEngine;
using System.Collections;
using System.Runtime.ConstrainedExecution;

public class Gun : Weapon
{
    [SerializeField] private int m_maxAmmo = 2;
    [SerializeField] private float m_reloadTime = 1.5f;
    [SerializeField] private float m_bulletSpeed = 15f;
    [SerializeField] private GameObject m_bulletPrefab;
    [SerializeField] private Transform m_firePoint;
    [SerializeField] private Camera m_mainCamera;
    [SerializeField] private float m_fireCooldown = 0.5f;

    private int m_currentAmmo;
    private bool m_isReloading = false;
    private bool m_canFire = true;

    private void Start()
    {
        if (!m_mainCamera) { m_mainCamera = Camera.main; }
        m_currentAmmo = m_maxAmmo;
    }

    private void FixedUpdate()
    {
        if (InventoryManager.Instance.GetAmmoItem("Bullet") != null)
        {
            if (InventoryManager.Instance.GetAmmoItem("Bullet").Quantity > 0)
            {
                if (InputManager.Instance.m_AttackInput.WasPerformedThisFrame() && !m_isReloading && m_canFire)
                {
                    Attack();
                }
            }
        }
    }

    public override void Attack()
    {
        Fire();
    }

    private void Fire()
    {
        if (!CanAttack() || m_isReloading || !m_canFire) return;

        if (m_currentAmmo <= 0)
        {
            StartCoroutine(ReloadCO());
            return;
        }

        DisableCanAttack();
        StartCoroutine(FireCooldownCO());
        PlayAttackAnimation("Attack 0");
        GameObject bulletGO = Instantiate(m_bulletPrefab, m_firePoint.position, m_firePoint.rotation);
        bulletGO.GetComponent<PlayerWeaponProjectile>().SetDamage(m_damage);
        Rigidbody bulletRB = bulletGO.GetComponent<Rigidbody>();
        Ray ray = m_mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        bulletRB.AddForce(ray.direction.normalized * m_bulletSpeed, ForceMode.Impulse);
        m_weaponAudioSource.Play();
        m_currentAmmo--;
    }

    private IEnumerator ReloadCO()
    {
        m_isReloading = true;

        for (int i = 0; i < m_maxAmmo; i++)
        {
            if (InventoryManager.Instance.GetAmmoItem("Bullet") != null && InventoryManager.Instance.GetAmmoItem("Bullet").Quantity > 0)
            {
                InventoryManager.Instance.UseAmmoItem("Bullet");
                m_currentAmmo++;

                PlayAttackAnimation("Reload");
                yield return new WaitForSeconds(m_reloadTime / m_maxAmmo);
            }
            else
            {
                break;
            }
        }

        m_isReloading = false;
    }

    private IEnumerator FireCooldownCO()
    {
        m_canFire = false;
        yield return new WaitForSeconds(m_fireCooldown);
        m_canFire = true;
    }
}
