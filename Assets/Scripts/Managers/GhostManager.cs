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

    [SerializeField] private Material validMaterial;
    [SerializeField] private Material invalidMaterial;
    [SerializeField] private NavmeshManager m_navmeshManager;
    [SerializeField] private GhostObject[] m_ghostObjects;

    private Dictionary<string, GameObject> m_instanceGhosts;
    private GameObject currentGhostObject;
    private Vector3 lastPosition;
    private float hoverTime;

    private void Start()
    {
        if (m_navmeshManager == null) m_navmeshManager = FindObjectOfType<NavmeshManager>();
        InitializeGhostObjects();
    }

    private void Update()
    {
        //if (currentGhostObject) UpdateGhostMaterial(IsPathValid());
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
        m_instanceGhosts.TryGetValue(_name, out GameObject _instance);
        return _instance;
    }

    //public void UpdateGhost(string _name, Vector3 _position, out bool _isValid)
    //{
    //    currentGhostObject = GetGhostObjectByName(_name);
    //    if (currentGhostObject == null)
    //    {
    //        _isValid = false;
    //        return;
    //    }
    //    currentGhostObject.transform.position = _position;
    //    _isValid = IsPathValid(); // Validate against existing NavMesh
    //    UpdateGhostMaterial(_isValid);
    //    currentGhostObject.SetActive(true);
    //}

    public void UpdateGhost(string _name, Vector3 _position, bool _isValid)
    {
        if (currentGhostObject == null || currentGhostObject.name != _name)
        {
            currentGhostObject = GetGhostObjectByName(_name);
        }

        if (currentGhostObject == null)
        {
            _isValid = false;
            return;
        }

        if (_position != lastPosition)
        {
            lastPosition = _position;
            hoverTime = 0f;
            _isValid = false;
        }
        else
        {
            hoverTime += Time.deltaTime;
            if (hoverTime >= 0.1f)
            {
                //m_navmeshManager.BuildNavMesh();
                _isValid = IsPathValid();
                UpdateGhostMaterial(_isValid);
            }
        }

        currentGhostObject.transform.position = _position;
        currentGhostObject.SetActive(true);
    }


    public void HideGhost()
    {
        if (currentGhostObject != null)
        {
            currentGhostObject.SetActive(false);
            currentGhostObject = null;
        }
    }

    public bool IsPathValid()
    {
        return m_navmeshManager.CheckPathValidity();
    }

    public void PlaceGhostObjectTemporarily(Vector3 _position)
    {
        if (currentGhostObject == null) return;

        currentGhostObject.transform.position = _position;
        currentGhostObject.SetActive(true);

        //m_navmeshManager.BuildNavMesh();

        bool isPathValid = IsPathValid();
        UpdateGhostMaterial(isPathValid);
    }

    private void UpdateGhostMaterial(bool isValid)
    {
        MeshRenderer _meshRenderer = currentGhostObject.GetComponentInChildren<MeshRenderer>();
        Material[] materials = _meshRenderer.materials;

        for (int i = 0; i < materials.Length; i++)
        {
            materials[i] = isValid ? validMaterial : invalidMaterial;
        }

        _meshRenderer.materials = materials;
    }

    public Vector3 GetCurrentGhostPosition()
    {
        return currentGhostObject.transform.position;
    }
}
