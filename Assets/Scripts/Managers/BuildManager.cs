using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    [System.Serializable]
    private class BuildableObject
    {
        public string Name;
        public GameObject Prefab;
        [HideInInspector] public GameObject ObjectToBuild;
    }

    [SerializeField] private GhostManager m_ghostManager;
    [SerializeField] private LayerMask m_floorLayer;
    [SerializeField] private LayerMask m_blockLayer;
    [SerializeField] private BuildableObject[] m_buildableObjects;
    private string selectedObject = "Cannon";
    private Transform lastTarget;

    private void Start()
    {
        if (m_ghostManager == null) m_ghostManager = FindObjectOfType<GhostManager>();
    }

    private void Update()
    {
        HandleGhostPlacer();
    }

    private void HandleGhostPlacer()
    {
        if (!string.IsNullOrEmpty(selectedObject) && BuildableObjectsContains(selectedObject))
        {
            Transform target = GetPlacementTarget(GetTargetLayer());
            if (target != null)
            {
                bool isValidPlacement = IsValidPlacement(target);
                Vector3 placementPosition = CalculatePlacementPosition(target);

                m_ghostManager.UpdateGhost(selectedObject, placementPosition, isValidPlacement);

                if (isValidPlacement && Input.GetMouseButtonDown(0))
                {
                    PlaceObject(placementPosition);
                    m_ghostManager.HideGhost();
                }
            }
            else
            {
                m_ghostManager.HideGhost();
            }
        }
        else
        {
            m_ghostManager.HideGhost();
        }
    }

    private LayerMask GetTargetLayer()
    {
        switch (selectedObject)
        {
            case "Block":
                return m_floorLayer;
            case "Cannon":
                return m_blockLayer;
            case "Crossbow":
                return m_blockLayer;
            case "Spikes":
                return m_floorLayer;
            case "Firepit":
                return m_floorLayer;
            default:
                return m_floorLayer;
        }
    }

    private Vector3 CalculatePlacementPosition(Transform target)
    {
        Collider targetCollider = target.GetComponent<Collider>();
        Debug.Log(targetCollider.name);
        if (targetCollider != null)
        {
            Vector3 targetPosition = targetCollider.bounds.center;
            //targetPosition.y = targetCollider.bounds.max.y;
            return targetPosition;
        }
        return target.position;
    }

    private bool IsValidPlacement(Transform target)
    {
        bool isValidTag = IsValidTag(target);

        if (isValidTag && lastTarget != target)
        {
            lastTarget = target;
            return true;
        }
        return false;
    }

    private bool IsValidTag(Transform target)
    {
        switch (selectedObject)
        {
            case "Block":
                return target.CompareTag("Floor");
            case "Cannon":
                return target.CompareTag("Floor");
            case "Crossbow":
                return target.CompareTag("Block");
            case "Spikes":
                return target.CompareTag("Floor");
            case "Firepit":
                return target.CompareTag("Floor");
            default:
                return false;
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


    private void PlaceObject(Vector3 position)
    {
        BuildableObject buildable = GetBuildableObjectByName(selectedObject);
        if (buildable != null)
        {
            Instantiate(buildable.Prefab, position, Quaternion.identity);
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
