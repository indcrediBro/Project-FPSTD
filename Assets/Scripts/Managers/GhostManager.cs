using System.Collections.Generic;
using UnityEngine;

public class GhostManager : MonoBehaviour
{
    [System.Serializable]
    private class GhostObject
    {
        public string Name;
        public GameObject Prefab;
        [HideInInspector] public GameObject Instance;
        [HideInInspector] public MeshRenderer Renderer;
    }

    [SerializeField] private Material m_validMaterial;
    [SerializeField] private Material m_invalidMaterial;
    [SerializeField] private GhostObject[] m_ghostObjects;

    private Dictionary<string, GameObject> m_instanceGhosts;
    private GameObject m_currentGhostObject;
    private Vector3 m_lastPosition;

    private void Start()
    {
        InitializeGhostObjects();
    }

    private void InitializeGhostObjects()
    {
        m_instanceGhosts = new Dictionary<string, GameObject>();
        foreach (GhostObject obj in m_ghostObjects)
        {
            obj.Instance = Instantiate(obj.Prefab);
            obj.Renderer = obj.Instance.GetComponentInChildren<MeshRenderer>();
            obj.Instance.SetActive(false);
            m_instanceGhosts.Add(obj.Name, obj.Instance);
        }
    }

    private GameObject GetGhostObjectByName(string name)
    {
        foreach (KeyValuePair<string, GameObject> obj in m_instanceGhosts)
        {
            obj.Value.SetActive(false);
        }

        GameObject instance;
        m_instanceGhosts.TryGetValue(name, out instance);
        if (instance != null)
        {
            instance.SetActive(true);
        }
        return instance;
    }

    public void UpdateGhost(string name, Vector3 position, bool isCorrectLayer)
    {
        if (m_currentGhostObject == null || m_currentGhostObject.name != name)
        {
            m_currentGhostObject = GetGhostObjectByName(name);
        }

        if (m_currentGhostObject == null)
        {
            return;
        }

        m_currentGhostObject.transform.position = position;
        bool isOverlappingPlayer = IsOverlappingPlayer(position);
        UpdateGhostMaterial(isCorrectLayer, isOverlappingPlayer);
        m_currentGhostObject.SetActive(true);
    }

    public void HideGhost()
    {
        if (m_currentGhostObject != null)
        {
            m_currentGhostObject.SetActive(false);
            m_currentGhostObject = null;
        }
    }

    private void UpdateGhostMaterial(bool isCorrectLayer, bool isOverlappingPlayer)
    {
        MeshRenderer meshRenderer = m_currentGhostObject.GetComponentInChildren<MeshRenderer>();
        Material[] materials = meshRenderer.materials;

        Material materialToApply = isOverlappingPlayer ? m_invalidMaterial :
            isCorrectLayer ? m_validMaterial : m_invalidMaterial;

        for (int i = 0; i < materials.Length; i++)
        {
            materials[i] = materialToApply;
        }

        meshRenderer.materials = materials;
    }

    private bool IsOverlappingPlayer(Vector3 position)
    {
        Collider[] colliders = Physics.OverlapSphere(position, 0.5f);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
    }

    public Vector3 GetCurrentGhostPosition()
    {
        return m_currentGhostObject.transform.position;
    }
}
