using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapCrossbow : TrapBase
{
    public GameObject arrowPrefab;
    public float launchForce = 20f;
    public Transform target;

    private void Start()
    {
        trapName = "Crossbow";
        damage = 10f;
        fireRate = 0.2f; // 5 shots per second
    }

    public override void ActivateTrap()
    {
        StartCoroutine(FireArrows());
    }

    private IEnumerator FireArrows()
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
        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = arrow.GetComponent<Rigidbody>();
        Vector3 direction = (target.position - firePoint.position).normalized;
        rb.velocity = CalculateProjectileVelocity(target.position, firePoint.position, launchForce);
    }
}
