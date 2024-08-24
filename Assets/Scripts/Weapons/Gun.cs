using UnityEngine;
using System.Collections;
using System.Runtime.ConstrainedExecution;
using UnityEditor.Rendering;

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
    private PlayerUIController m_playerUI;

    private void Awake()
    {
        m_playerUI = GetComponentInParent<PlayerUIController>();
        UpdateAmmoUI();
    }

    private void OnEnable()
    {
        m_isReloading = false;
        if (HasBullets()) UpdateAmmoUI();
    }

    private void Start()
    {
        if (!m_mainCamera) { m_mainCamera = Camera.main; }
        //m_currentAmmo = m_maxAmmo;
    }

    private void FixedUpdate()
    {
        if (HasBullets())
        {
            if (InputManager.Instance.m_AttackInput.WasPerformedThisFrame() && !m_isReloading && m_canFire)
            {
                Attack();
            }

            if (InputManager.Instance.m_ReloadInput.WasReleasedThisFrame() && !m_isReloading)
            {
                if (m_currentAmmo < m_maxAmmo) StartCoroutine(ReloadCO());
            }
        }
    }

    private bool HasBullets()
    {
        if (InventoryManager.Instance.GetAmmoItem("Bullet") != null && InventoryManager.Instance.GetAmmoItem("Bullet").Quantity > 0)
        {
            return true;
        }
        return false;
    }
    private void UpdateAmmoUI()
    {
        if (m_playerUI)
        {
            if (HasBullets())
            {
                m_playerUI.UpdateAmmoText(m_currentAmmo + "/" + (InventoryManager.Instance.GetAmmoItem("Bullet").Quantity).ToString());
            }
            else
            {
                m_playerUI.UpdateAmmoText("0/0");
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

        GameObject bulletGO = ObjectPoolManager.Instance.GetPooledObject("Ammo_BulletPlayer");
        bulletGO.transform.position = m_firePoint.position;
        bulletGO.transform.rotation = m_firePoint.rotation;
        bulletGO.SetActive(true);

        bulletGO.GetComponent<PlayerWeaponProjectile>().SetDamage(GetCurrentDamage());
        Rigidbody bulletRB = bulletGO.GetComponent<Rigidbody>();
        Ray ray = m_mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        bulletRB.AddForce(ray.direction.normalized * m_bulletSpeed, ForceMode.Impulse);

        //m_weaponAudioSource.Play();
        m_currentAmmo--;
        UpdateAmmoUI();
    }

    private IEnumerator ReloadCO()
    {
        m_isReloading = true;

        PlayAttackAnimation("Reload");
        for (int i = 0; i < m_maxAmmo; i++)
        {
            if (HasBullets())
            {
                InventoryManager.Instance.UseAmmoItem("Bullet");
                m_currentAmmo++;

                yield return new WaitForSeconds((m_reloadTime / m_maxAmmo) / 2);
                UpdateAmmoUI();
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
