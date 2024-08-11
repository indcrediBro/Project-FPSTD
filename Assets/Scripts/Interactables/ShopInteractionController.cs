using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInteractionController : Interactable
{
    [SerializeField] private GameObject m_shopUI;

    private void LateUpdate()
    {
        if (m_interactGFX && m_interactGFX.activeSelf)
        {
            LookAtPlayer();
        }
    }

    public override void Interact()
    {
        m_shopUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Debug.Log("Interacted With Shop!");
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
