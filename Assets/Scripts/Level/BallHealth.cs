using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHealth : Health
{
    protected override void Die(float _timeBeforeRemoving)
    {
        AudioManager.Instance.PlaySound("SFX_BallExplode");
        GameObject effect = ObjectPoolManager.Instance.GetPooledObject("VFX_BallExplode");
        effect.transform.position = transform.position;
        effect.SetActive(true);
        base.Die(_timeBeforeRemoving);
    }
}
