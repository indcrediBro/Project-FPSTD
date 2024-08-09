using UnityEngine;
using System.Collections.Generic;

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

    [SerializeField] private Material validMaterial;
    [SerializeField] private Material invalidMaterial;

    [SerializeField] private NavmeshManager m_navmeshManager;
    [SerializeField] private GhostObject[] m_ghostObjects;

    private GameObject currentGhostObject;
    private Dictionary<string, GameObject> m_instanceGhosts;

    private void Start()
    {
        if (m_navmeshManager == null) m_navmeshManager = FindObjectOfType<NavmeshManager>();
        InitializeGhostObjects();
    }

    private void InitializeGhostObjects()
    {
        m_instanceGhosts = new Dictionary<string, GameObject>();
        foreach (GhostObject obj in m_ghostObjects)
        {
            obj.Instance = Instantiate(obj.Prefab);
            obj.Renderer = obj.Instance.GetComponentInChildren<MeshRenderer>();
            obj.Instance.SetActive(false); // Set inactive by default
            m_instanceGhosts.Add(obj.Name, obj.Instance);
        }
    }

    private GameObject GetGhostObjectByName(string _name)
    {
        if (m_instanceGhosts.TryGetValue(_name, out GameObject _instance))
        {
            return _instance;
        }
        return null;
    }

    public void UpdateGhost(string _name, Vector3 _position, bool _isValid)
    {
        currentGhostObject = GetGhostObjectByName(_name);
        if (currentGhostObject == null) return;

        currentGhostObject.transform.position = _position;
        MeshRenderer _meshRenderer = currentGhostObject.GetComponentInChildren<MeshRenderer>();
        Material[] materials = _meshRenderer.materials;
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i] = _isValid ? validMaterial : invalidMaterial;
        }
        _meshRenderer.materials = materials;
        currentGhostObject.SetActive(true);
    }

    public void HideGhost()
    {
        if (currentGhostObject != null)
        {
            currentGhostObject.SetActive(false);
            currentGhostObject = null;
            m_navmeshManager.BuildNavMesh();
        }
    }
}
