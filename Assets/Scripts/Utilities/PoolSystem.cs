using System.Collections;
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
    [SerializeField] private List<GameObject> m_poolObjects;
    [SerializeField] private List<GameObject> m_currentActivObjects;


    // Start is called before the first frame update
    void Start()
    {
        m_currentActivObjects = new List<GameObject>();
        CreatePool();
    }

    // Update is called once per frame
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
        }
    }

    public void SpawnObject()
    {
        //if there is only 1 spawn point
        GameObject _object = GetObjectInPool();
        _object.transform.position = m_spawnPoints[0].position;
        _object.transform.rotation = m_spawnPoints[0].rotation;

        //if multiple, and random

        //if multiple, and in order
    }

    private GameObject GetObjectInPool()
    {
        for (int i = 0; i < m_poolObjects.Count; i++)
        {
            if (!m_poolObjects[i].activeInHierarchy)
            {
                m_poolObjects[i].SetActive(true);
                m_currentActivObjects.Add(m_poolObjects[i]);
                return m_poolObjects[i];
            }
        }
        return null;
    }
}
