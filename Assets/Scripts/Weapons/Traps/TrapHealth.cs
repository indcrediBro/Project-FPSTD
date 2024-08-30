using UnityEngine;
using System.Collections;

public class TrapHealth : Health
{
    protected override void Die(float _time)
    {
        transform.SetParent(null);
        base.Die(_time);
    }
}
