using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInteractionController : Interactable
{
    private void LateUpdate()
    {
        if (m_interactGFX && m_interactGFX.activeSelf)
        {
            LookAtPlayer();
        }
    }

    public override void Interact()
    {
        GameStateManager.Instance.SetState(new ShopState(GameStateManager.Instance));
    }

    private void LookAtPlayer()
    {
        Vector3 direction = Camera.main.transform.position - transform.position;

        direction.y = 0;
        if (direction != Vector3.zero)
        {
            m_interactGFX.transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
