using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapCannon : TrapBase
{
    public GameObject projectilePrefab; // The cannonball or arrow prefab
    public float launchForce = 10f;
    public Transform target;

    private void Start()
    {
        trapName = "Cannon";
        damage = 50f;
        fireRate = 2f; // 1 shot per second
    }

    public override void ActivateTrap()
    {
        StartCoroutine(FireCannon());
    }

    private IEnumerator FireCannon()
    {
        while (true)
        {
            RotateTowardsTarget(target.position);
            FireProjectile();
            yield return new WaitForSeconds(fireRate);
        }
    }

    private void FireProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        Vector3 direction = (target.position - firePoint.position).normalized;
        rb.velocity = CalculateProjectileVelocity(target.position, firePoint.position, launchForce);
    }
}
