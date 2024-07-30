using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected string m_weaponName;
    [SerializeField] protected float m_attackRate;
    [SerializeField] protected float m_damage;
    [SerializeField] protected int m_maxAmmo;
    [SerializeField] protected int m_magazineSize;
    [SerializeField] protected float m_reloadTime;


    public void SetDamage(float _value) { m_damage = _value; }
    public string GetWeaponName() { return m_weaponName; }

    public abstract void StartAttack();
    public abstract void StopAttack();

    protected float m_nextAttackTime = 0f;
    protected int m_currentAmmo;
    protected int m_currentMagazineAmmo;
    protected bool m_isReloading = false;

    protected virtual void Start()
    {
        m_currentAmmo = m_maxAmmo;
        m_currentMagazineAmmo = m_magazineSize;
    }

    public virtual void Reload()
    {
        if (!m_isReloading && m_currentMagazineAmmo < m_magazineSize && m_currentAmmo > 0)
        {
            m_isReloading = true;
            Invoke(nameof(FinishReloading), m_reloadTime);
        }
    }

    protected void FinishReloading()
    {
        int ammoNeeded = m_magazineSize - m_currentMagazineAmmo;
        int ammoToReload = Mathf.Min(ammoNeeded, m_currentAmmo);

        m_currentMagazineAmmo += ammoToReload;
        m_currentAmmo -= ammoToReload;

        m_isReloading = false;
    }

    protected bool CanAttack()
    {
        return Time.time >= m_nextAttackTime && m_currentAmmo > 0 && !m_isReloading;
    }
}
