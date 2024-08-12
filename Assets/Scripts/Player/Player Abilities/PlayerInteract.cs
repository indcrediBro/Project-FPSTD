using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private LayerMask m_interactableLayers;
    [SerializeField] private Transform m_interactCenter;
    [SerializeField] private float m_interactRange;
    private float m_currentInteractableDistance;
    private Interactable m_currentInteractable;
    private Transform m_currentInteractableTF;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        m_currentInteractableDistance = m_interactRange;
    }

    private void Update()
    {
        HandleInteract();
    }

    public void Interact()
    {
        m_currentInteractable?.Interact();
    }

    private void HandleInteract()
    {
        const int maxColliders = 20;
        Collider[] colliders = new Collider[maxColliders];
        int size = Physics.OverlapSphereNonAlloc(m_interactCenter.position, m_interactRange, colliders, m_interactableLayers);

        Interactable closestInteractable = null;
        float distanceToCurrentInteractable = m_currentInteractableDistance;

        for (int i = 0; i < size; i++)
        {
            if (!colliders[i].TryGetComponent(out Transform colliderTF) ||
               !colliders[i].TryGetComponent(out Interactable interactable))
                continue;

            float distanceToPlayer = Vector3.Distance(colliderTF.position, m_interactCenter.position);
            bool closerThanCurrentInteractable = distanceToPlayer < distanceToCurrentInteractable;
            if (closerThanCurrentInteractable)
            {
                closestInteractable = interactable;
                distanceToCurrentInteractable = distanceToPlayer;
            }
        }

        if (closestInteractable)
        {
            SetInteractable(closestInteractable, closestInteractable.transform);
        }

        UpdateInteractableDistance();
        CheckIfInteractableIsInRange();
    }

    private void UpdateInteractableDistance()
    {
        if (m_currentInteractableTF)
        {
            m_currentInteractableDistance = Vector3.Distance(m_currentInteractableTF.position, m_interactCenter.position);
            return;
        }

        ClearInteractable();
    }

    private void CheckIfInteractableIsInRange()
    {
        if (m_currentInteractableDistance > m_interactRange)
        {
            ClearInteractable();
        }
    }

    private void SetInteractable(Interactable _interactable, Transform _interactableTF)
    {
        ClearInteractable();
        m_currentInteractable = _interactable;
        m_currentInteractableTF = _interactableTF;
        m_currentInteractable?.EnableInteractGFX();
    }

    public void ClearInteractable()
    {
        m_currentInteractable?.DisableInteractGFX();
        m_currentInteractable = null;
        m_currentInteractableTF = null;
        m_currentInteractableDistance = m_interactRange;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
    }
}
