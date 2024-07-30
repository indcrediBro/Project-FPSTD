using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Arrow : Projectile
{
    protected override void OnImpact(Collider _other)
    {
        GetComponent<Rigidbody>().isKinematic = true;

        Debug.Log("Collided with " + _other.name);

        if(_other.TryGetComponent(out Health _health))
        {
            _health.TakeDamage(m_damage);
        }
    }
}
