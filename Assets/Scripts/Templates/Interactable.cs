using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] protected GameObject m_interactGFX;

    public abstract void Interact();

    public virtual void EnableInteractGFX()
    {
        m_interactGFX.SetActive(true);
    }

    public virtual void DisableInteractGFX()
    {
        if (m_interactGFX)
            m_interactGFX.SetActive(false);
    }
}