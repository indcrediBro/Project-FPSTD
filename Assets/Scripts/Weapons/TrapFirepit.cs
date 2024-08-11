using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapFirepit : TrapBase
{
    private void Start()
    {
        trapName = "Firepit";
        damage = 5f; // Damage over time
        fireRate = 1f; // Damage interval
    }

    public override void ActivateTrap()
    {
        // Logic to continuously deal damage over time
        StartCoroutine(DealDamageOverTime());
    }

    private IEnumerator DealDamageOverTime()
    {
        while (true)
        {
            // Damage over time logic here
            Debug.Log($"{trapName} dealing {damage} damage over time!");
            yield return new WaitForSeconds(fireRate);
        }
    }
}
