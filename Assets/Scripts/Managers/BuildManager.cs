using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    [System.Serializable]
    private class BuildableObject
    {
        public string Name;
        public GameObject Prefab;
    }

    [SerializeField] private GhostManager m_ghostManager;
    [SerializeField] private NavmeshManager m_navmeshManager;
    [SerializeField] private LayerMask m_floorLayer;
    [SerializeField] private LayerMask m_blockLayer;
    [SerializeField] private BuildableObject[] m_buildableObjects;
    [SerializeField] private string selectedObject = "Block";
    private Transform lastTargetTransform;

    private void Start()
    {
        if (m_ghostManager == null) m_ghostManager = FindObjectOfType<GhostManager>();
        if (m_navmeshManager == null) m_navmeshManager = FindObjectOfType<NavmeshManager>();
    }

    private void Update()
    {
        HandleGhostPlacer();
    }

    private void HandleGhostPlacer()
    {
        if (string.IsNullOrEmpty(selectedObject) || !BuildableObjectsContains(selectedObject))
        {
            m_ghostManager.HideGhost();
            return;
        }

        Transform target = GetPlacementTarget(GetTargetLayer());
        if (target != null)
        {
            Vector3 placementPosition = CalculatePlacementPosition(target);
            bool isValidPlacement; // Declare the variable here

            // Check if the target has changed
            if (lastTargetTransform != target)
            {
                // Temporarily place the ghost object and check the NavMesh validity
                m_ghostManager.PlaceGhostObjectTemporarily(placementPosition);

                // Update the ghost object appearance based on the validity
                m_ghostManager.UpdateGhost(selectedObject, placementPosition, out isValidPlacement);

                // Update the last target transform
                lastTargetTransform = target;
            }
            else
            {
                // Only update the ghost appearance if the target hasn't changed
                m_ghostManager.UpdateGhost(selectedObject, placementPosition, out isValidPlacement);
            }

            if (isValidPlacement && Input.GetMouseButtonDown(0))
            {
                PlaceObject(target);
                m_ghostManager.HideGhost();
            }
        }
        else
        {
            m_ghostManager.HideGhost();
            lastTargetTransform = null; // Reset last target if no target is found
        }
    }


    private LayerMask GetTargetLayer()
    {
        switch (selectedObject)
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

    private void PlaceObject(Transform _target)
    {
        BuildableObject buildable = GetBuildableObjectByName(selectedObject);
        if (buildable != null)
        {
            Instantiate(buildable.Prefab, m_ghostManager.GetCurrentGhostPosition(), Quaternion.identity, _target);
            m_navmeshManager.BuildNavMesh(); // Rebuild the NavMesh after placing the object
        }
    }

    private BuildableObject GetBuildableObjectByName(string _name)
    {
        foreach (BuildableObject obj in m_buildableObjects)
        {
            if (obj.Name == _name)
                return obj;
        }
        return null;
    }

    private bool BuildableObjectsContains(string _name)
    {
        foreach (BuildableObject obj in m_buildableObjects)
        {
            if (obj.Name == _name)
                return true;
        }
        return false;
    }
}
