using UnityEngine;

public abstract class TrapBase : MonoBehaviour
{
    public string trapName;
    public float damage;
    public float fireRate;
    public Transform firePoint; // Point where the projectile is spawned

    public abstract void ActivateTrap();

    protected Quaternion CalculateFiringAngle(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - firePoint.position;
        float distance = direction.magnitude;
        float angle = Mathf.Atan2(direction.y, distance) * Mathf.Rad2Deg;
        return Quaternion.Euler(0, 0, angle);
    }

    protected void RotateTowardsTarget(Vector3 targetPosition)
    {
        Quaternion rotation = CalculateFiringAngle(targetPosition);
        firePoint.rotation = rotation;
    }

    protected Vector3 CalculateProjectileVelocity(Vector3 target, Vector3 origin, float speed)
    {
        Vector3 direction = target - origin;
        float heightDifference = direction.y;
        direction.y = 0;
        float distance = direction.magnitude;
        direction.y = distance * Mathf.Tan(CalculateFiringAngle(target).eulerAngles.z * Mathf.Deg2Rad);

        float velocity = Mathf.Sqrt(distance * Physics.gravity.magnitude / Mathf.Sin(2 * CalculateFiringAngle(target).eulerAngles.z * Mathf.Deg2Rad));

        return direction.normalized * velocity;
    }
}

