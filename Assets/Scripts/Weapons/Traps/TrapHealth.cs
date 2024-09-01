using UnityEngine;
using System.Collections;

public class TrapHealth : Health
{
    protected override void Die(float _time)
    {
        Transform parent = transform.GetComponentInParent<FloorRandomiser>()?.transform;
        parent.gameObject.layer = 7;
        parent.tag = "Floor";

        transform.SetParent(null);
        base.Die(_time);
    }
}
