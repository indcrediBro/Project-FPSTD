using UnityEngine;
using System.Collections;

public interface IPoolableObject
{
    void OnObjectSpawn();
    void OnObjectDespawn();
}
