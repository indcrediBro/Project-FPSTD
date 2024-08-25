using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapCannon : TrapBase
{
    [SerializeField] private float blastRadius;

    protected override void Fire()
    {
        m_fireCooldown = m_fireRate;
        ApplyRecoil();

        if (m_currentTarget != null)
        {
            Vector3 targetPosition = m_currentTarget.position;
            targetPosition.y += m_yOffset;
            GameObject projectile = InstantiateProjectile();
            projectile.GetComponent<Projectile>().Launch(targetPosition, m_damage, blastRadius);
        }
    }

    private GameObject InstantiateProjectile()
    {
        GameObject projectile = ObjectPoolManager.Instance.GetPooledObject("Ammo_CannonBall");
        projectile.transform.position = m_firePoint.position;
        projectile.transform.rotation = m_firePoint.rotation;
        projectile.SetActive(true);
        return projectile;
    }


}
