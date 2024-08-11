using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSpikes : TrapBase
{
    private bool isOpen;

    private void Start()
    {
        trapName = "Spikes";
        damage = 30f;
        fireRate = 2f; // Open/Close every 2 seconds
    }

    public override void ActivateTrap()
    {
        // Logic to open and close spikes
        StartCoroutine(OpenAndCloseSpikes());
    }

    private IEnumerator OpenAndCloseSpikes()
    {
        while (true)
        {
            isOpen = !isOpen;
            // Open/Close logic here
            Debug.Log($"{trapName} is now {(isOpen ? "open" : "closed")}!");
            yield return new WaitForSeconds(fireRate);
        }
    }
}
