using System.Collections.Generic;
using UnityEngine;

public class BuildManager : Singleton<BuildManager>
{
    [SerializeField] private InventoryManager m_inventoryManager;
    [SerializeField] private GhostManager m_ghostManager;
    [SerializeField] private NavmeshManager m_navmeshManager;
    [SerializeField] private LayerMask m_floorLayer;
    [SerializeField] private LayerMask m_blockLayer;
    [SerializeField] private string m_selectedObject;
    private Transform m_lastTargetTransform;

    private void Update()
    {
        HandleGhostPlacer();
    }

    private void HandleGhostPlacer()
    {
        InventoryManager.InventoryItem selectedItem = m_inventoryManager.GetSelectedBuildableItem(m_selectedObject);
        if (selectedItem == null)
        {
            m_ghostManager.HideGhost();
            return;
        }

        if (string.IsNullOrEmpty(m_selectedObject))
        {
            m_ghostManager.HideGhost();
            return;
        }

        Transform target = GetPlacementTarget(GetTargetLayer());
        if (target != null)
        {
            Vector3 placementPosition = CalculatePlacementPosition(target);
            bool isValidPlacement = m_navmeshManager.CheckPathValidity();

            if (m_lastTargetTransform != target)
            {
                m_ghostManager.PlaceGhostObjectTemporarily(placementPosition);
                m_ghostManager.UpdateGhost(m_selectedObject, placementPosition, isValidPlacement);
                m_lastTargetTransform = target;
            }
            else
            {
                m_ghostManager.UpdateGhost(m_selectedObject, placementPosition, isValidPlacement);
            }

            if (isValidPlacement && InputManager.Instance.m_AttackInput.WasReleasedThisFrame())
            {
                PlaceObject(target,selectedItem.Prefab);
                m_inventoryManager.UseBuildableItem(m_selectedObject);
                m_ghostManager.HideGhost();
            }
        }
        else
        {
            m_ghostManager.HideGhost();
            m_lastTargetTransform = null;
        }
    }


    private LayerMask GetTargetLayer()
    {
        switch (m_selectedObject)
        {
            case "Cannon":
            case "Crossbow":
                return m_blockLayer;
            case "Spikes":
            case "Firepit":
            case "Block":
            default:
                return m_floorLayer;
        }
    }

    private Transform GetPlacementTarget(LayerMask _layer)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, _layer))
        {
            if (hit.transform.gameObject.activeInHierarchy && hit.transform.gameObject.activeSelf)
            {
                return hit.transform;
            }
        }
        return null;
    }

    private Vector3 CalculatePlacementPosition(Transform target)
    {
        Collider targetCollider = target.GetComponent<Collider>();
        if (targetCollider != null)
        {
            Vector3 targetPosition = targetCollider.bounds.center;
            targetPosition.y = targetCollider.bounds.max.y;
            return targetPosition;
        }
        return target.position;
    }

    private void PlaceObject(Transform _target, GameObject _prefab)
    {
        Instantiate(_prefab, m_ghostManager.GetCurrentGhostPosition(), Quaternion.identity, _target);

        _target.tag = "Untagged";
        _target.gameObject.layer = 0;

    }

    private int m_currentBuildableIndex = 0;

    public void SwitchToBuildable(int change)
    {
        m_currentBuildableIndex = (m_currentBuildableIndex + change + m_inventoryManager.GetBuildableInventoryItems().Count - 1) % m_inventoryManager.GetBuildableInventoryItems().Count - 1;
        m_selectedObject = m_inventoryManager.GetBuildableInventoryItems()[m_currentBuildableIndex].Name;
    }

    public void UnequipCurrentBuildable()
    {
        m_selectedObject = "";
    }

    public void EquipLastBuildable()
    {
        m_selectedObject = m_inventoryManager.GetBuildableInventoryItems()[m_currentBuildableIndex].Name;
    }
}
