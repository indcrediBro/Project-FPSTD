using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon
{
    public GameObject m_projectilePrefab;
    public Transform m_firePoint;
    [SerializeField] private float m_speed;

    public override void StartAttack()
    {
    }

    public override void StopAttack()
    {
        m_nextAttackTime = Time.time + 1f / m_attackRate;
        FireProjectile();
    }

    void FireProjectile()
    {
        if (m_currentMagazineAmmo > 0)
        {
            m_currentMagazineAmmo--;

            // calculate direction to aim
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

            Vector3 aim;
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f))
                aim = hit.point;
            else
                aim = ray.GetPoint(100f);
            Vector3 direction = aim - m_firePoint.position;

            // fire bullet
            GameObject projectile = Instantiate(m_projectilePrefab, m_firePoint.position, Quaternion.LookRotation(direction));
            //projectile.transform.forward = direction;
            Projectile proj = projectile.GetComponent<Projectile>();
            if (proj != null)
            {
                proj.Initialize(m_damage, m_speed, true);
            }
        }
        else
        {
            Reload();
        }
    }
}
