using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapCrossbow : TrapBase
{
    protected override void Fire()
    {
        m_fireCooldown = m_fireRate;
        ApplyRecoil();

        if (m_currentTarget != null)
        {
            Vector3 targetPosition = m_currentTarget.position;
            GameObject projectile = InstantiateProjectile();
            projectile.GetComponent<Projectile>().Launch(targetPosition, m_damage);
        }
    }

    private GameObject InstantiateProjectile()
    {
        GameObject projectile = ObjectPoolManager.Instance.GetPooledObject("Ammo_ArrowCrossbow");
        projectile.transform.position = m_firePoint.position;
        projectile.transform.rotation = m_firePoint.rotation;
        return projectile;
    }
}
