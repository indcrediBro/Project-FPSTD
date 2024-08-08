using System.Collections.Generic;
using UnityEngine;

public class PoolSystem : MonoBehaviour
{

    [Header("=====Manual Settings=====")]
    [SerializeField] private GameObject m_mainObject;
    [SerializeField] private int m_maxPoolCount;
    [SerializeField] private int m_maxAciveCount;
    [SerializeField] private Transform[] m_spawnPoints;

    [Header("=====Debug Purpose=====")]
    public List<GameObject> CurrentActivObjects;
    [SerializeField] private List<GameObject> m_poolObjects;


    void Start()
    {
        InitiateActiveList();
        CreatePool();
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            SpawnObject();
        }
    }

    private void CreatePool()
    {
        m_poolObjects = new List<GameObject>();

        for (int i = 0; i < m_maxPoolCount; i++)
        {
            GameObject _object = Instantiate(m_mainObject);
            m_poolObjects.Add(_object);
            _object.SetActive(false);
            if (_object.GetComponent<PoolObject>())
            {
                _object.GetComponent<PoolObject>().PoolSystem = this;
            }
        }
    }

    public void SpawnObject()
    {
        GameObject _object = GetObjectInPool();
        if (_object != null)
        {

            //if there is only 1 spawn point
            _object.transform.position = m_spawnPoints[0].position;
            _object.transform.rotation = m_spawnPoints[0].rotation;

            //if multiple, and random

            //if multiple, and in order
        }
    }

    private GameObject GetObjectInPool()
    {
        for (int i = 0; i < m_poolObjects.Count; i++)
        {
            if (m_poolObjects[i].activeInHierarchy)
            {
                continue;
            }

            m_poolObjects[i].SetActive(true);
            CurrentActivObjects.Add(m_poolObjects[i]);
            return m_poolObjects[i];
        }
        return null;
    }

    private void InitiateActiveList()
    {
        CurrentActivObjects = new List<GameObject>();
    }
}
