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
            projectile.SetActive(true);
            projectile.GetComponent<Projectile>().Launch(m_currentTarget, m_damage);
            m_audioSource.pitch = RandomNumber.Instance.NextFloat(1.2f, 1.75f);
            m_audioSource.Play();
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
