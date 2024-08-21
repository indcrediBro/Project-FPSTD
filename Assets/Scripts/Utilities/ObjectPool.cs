using UnityEngine;
using System.Collections.Generic;

public class ObjectPool<T> where T : MonoBehaviour, IPoolableObject
{
    private readonly Queue<T> m_pool = new Queue<T>();
    private readonly T m_prefab;
    private readonly Transform m_parent;

    public ObjectPool(T _prefab, int _initialSize, Transform _parent = null)
    {
        this.m_prefab = _prefab;
        this.m_parent = _parent;

        for (int i = 0; i < _initialSize; i++)
        {
            T obj = GameObject.Instantiate(m_prefab, m_parent);
            obj.gameObject.SetActive(false);
            m_pool.Enqueue(obj);
        }
    }

    public T GetObject()
    {
        if (m_pool.Count > 0)
        {
            T obj = m_pool.Dequeue();
            obj.gameObject.SetActive(true);
            obj.OnObjectSpawn();
            return obj;
        }
        T newObj = GameObject.Instantiate(m_prefab, m_parent);
        newObj.OnObjectSpawn();
        return newObj;
    }

    public void ReturnObject(T obj)
    {
        obj.OnObjectDespawn();
        obj.gameObject.SetActive(false);
        m_pool.Enqueue(obj);
    }
}
