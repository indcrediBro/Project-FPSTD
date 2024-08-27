using UnityEngine;
using System.Collections;
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

    [SerializeField] private Material m_validMaterial;
    [SerializeField] private Material m_invalidMaterial;
    [SerializeField] private NavmeshManager m_navmeshManager;
    [SerializeField] private GhostObject[] m_ghostObjects;

    private Dictionary<string, GameObject> m_instanceGhosts;
    private GameObject m_currentGhostObject;
    private Vector3 m_lastPosition;
    private float m_hoverTime;

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
            obj.Instance.SetActive(false);
            m_instanceGhosts.Add(obj.Name, obj.Instance);
        }
    }

    private GameObject GetGhostObjectByName(string _name)
    {
        foreach (var obj in m_instanceGhosts)
        {
            obj.Value.SetActive(false);
        }
        m_instanceGhosts.TryGetValue(_name, out GameObject _instance);
        _instance.SetActive(true);
        return _instance;
    }

    public void UpdateGhost(string _name, Vector3 _position, bool _isValid)
    {
        if (m_currentGhostObject == null || m_currentGhostObject.name != _name)
        {
            m_currentGhostObject = GetGhostObjectByName(_name);
        }

        if (m_currentGhostObject == null)
        {
            _isValid = false;
            return;
        }

        if (_position != m_lastPosition)
        {
            m_lastPosition = _position;
            m_hoverTime = 0f;
            _isValid = false;
        }
        else
        {
            m_hoverTime += Time.deltaTime;
            if (m_hoverTime >= 0.2f)
            {
                //m_navmeshManager.BuildNavMesh();
                _isValid = IsPathValid();
                UpdateGhostMaterial(_isValid);
            }
            else
            {
                _isValid = false;
                UpdateGhostMaterial(false);
            }
        }

        m_currentGhostObject.transform.position = _position;
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

    public bool IsPathValid()
    {
        return m_navmeshManager.CheckPathValidity();
    }

    public void PlaceGhostObjectTemporarily(Vector3 _position)
    {
        if (m_currentGhostObject == null) return;

        m_currentGhostObject.transform.position = _position;
        m_currentGhostObject.SetActive(true);

        //m_navmeshManager.BuildNavMesh();

        bool isPathValid = IsPathValid();
        UpdateGhostMaterial(isPathValid);
    }

    private void UpdateGhostMaterial(bool _isValid)
    {
        MeshRenderer _meshRenderer = m_currentGhostObject.GetComponentInChildren<MeshRenderer>();
        Material[] materials = _meshRenderer.materials;

        for (int i = 0; i < materials.Length; i++)
        {
            materials[i] = _isValid ? m_validMaterial : m_invalidMaterial;
        }

        _meshRenderer.materials = materials;
    }

    public Vector3 GetCurrentGhostPosition()
    {
        return m_currentGhostObject.transform.position;
    }
}
