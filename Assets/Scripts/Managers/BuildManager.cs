using UnityEngine;

public class BuildManager : Singleton<BuildManager>
{
    [SerializeField] private InventoryManager m_inventoryManager;
    [SerializeField] private GhostManager m_ghostManager;
    [SerializeField] private int m_floorLayer = 7;
    [SerializeField] private int m_blockLayer = 8;
    [SerializeField] private string m_selectedObject;
    [SerializeField] private LayerMask m_buildableLayers;
    private Transform m_lastTargetTransform;

    private void Update()
    {
        if (GameReferences.Instance.m_IsGameOver || GameReferences.Instance.m_IsPaused) return;

        HandleGhostPlacer();
    }

    private void HandleGhostPlacer()
    {
        InventoryManager.InventoryItem selectedItem = m_inventoryManager.GetBuildableItem(m_selectedObject);
        if (selectedItem == null || string.IsNullOrEmpty(m_selectedObject))
        {
            m_ghostManager.HideGhost();
            return;
        }

        Transform target = GetPlacementTarget();
        Vector3 placementPosition = CalculatePlacementPosition(target);

        bool isCorrectLayer = target && target.gameObject.layer == GetTargetLayer();
        m_ghostManager.UpdateGhost(m_selectedObject, placementPosition, isCorrectLayer);

        if (InputManager.Instance.m_AttackInput.WasReleasedThisFrame() && isCorrectLayer)
        {
            PlaceObject(target, selectedItem.Prefab);
            m_inventoryManager.UseBuildableItem(m_selectedObject);
            m_ghostManager.HideGhost();
        }
    }

    private int GetTargetLayer()
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

    private Transform GetPlacementTarget()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10f, m_buildableLayers))
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
        if (target == null)
        {
            return GetPlacementTarget().position;
        }

        Collider targetCollider = target.GetComponent<Collider>();
        if (targetCollider != null)
        {
            Vector3 targetPosition = targetCollider.bounds.center;
            targetPosition.y = targetCollider.bounds.max.y;
            return targetPosition;
        }
        return target.position;
    }

    private void PlaceObject(Transform target, GameObject prefab)
    {
        Instantiate(prefab, m_ghostManager.GetCurrentGhostPosition(), Quaternion.identity, target);
        if (target != null)
        {
            target.tag = "Untagged";
            target.gameObject.layer = 0;
        }
    }

    public void SetSelectedBuildable(string name)
    {
        m_selectedObject = name;
    }
}
