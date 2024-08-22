using UnityEngine;
using System.Collections.Generic;
using System;

[System.Serializable]
public class ObjectPool
{
    public string m_Name;
    [HideInInspector] public List<GameObject> m_PooledObjects;
    public GameObject m_PooledObject;
    public int m_PooledAmount;
    public bool m_WillGrow;
}

public class ObjectPoolManager: Singleton<ObjectPoolManager>
{
    public ObjectPool[] objectPools;

    private void Start()
    {
        for (int i = 0; i < objectPools.Length; i++)
        {
            objectPools[i].m_PooledObjects = new List<GameObject>();
        }

        for (int i = 0; i < objectPools.Length; i++)
        {
            for (int t = 0; t < objectPools[i].m_PooledAmount; t++)
            {

                GameObject obj = Instantiate(objectPools[i].m_PooledObject);

                obj.SetActive(false);
                objectPools[i].m_PooledObjects.Add(obj);
            }
        }
    }

    public GameObject GetPooledObject(string name)
    {
        ObjectPool op = Array.Find(objectPools, ObjectPools => ObjectPools.m_Name == name);

        for (int i = 0; i < op.m_PooledObjects.Count; i++)
        {
            if (!op.m_PooledObjects[i].activeInHierarchy)
            {
                return op.m_PooledObjects[i];
            }
        }

        if (op.m_WillGrow)
        {
            GameObject obj = Instantiate(op.m_PooledObject);
            op.m_PooledObjects.Add(obj);
            return obj;
        }

        return null;
    }
}
