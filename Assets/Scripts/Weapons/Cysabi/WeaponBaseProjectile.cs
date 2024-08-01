using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class WeaponBaseProjectile : MonoBehaviour
{

    public float damage;

    [HideInInspector]
    public float power = 1;

    protected virtual void Damage(EnemyStats enemy)
    {
        enemy.TakeDamage(Mathf.Ceil(damage / power));
    }

    protected virtual void Done(GameObject gameObject)
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        GetComponent<Rigidbody>().isKinematic = true;

        if (other.TryGetComponent(out EnemyStats enemy))
        {
            Damage(enemy);
        }
        Done(gameObject);
    }
}
