using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;
using System.Collections;

public class NavmeshManager : MonoBehaviour
{
    [SerializeField] private NavMeshSurface m_navMeshSurface;
    [SerializeField] private Transform m_base, m_pathStart;
    private bool isBaking;

    void Start()
    {
        BuildNavMesh();
    }

    public void BuildNavMesh()
    {
        Debug.Log("BuildingMesh!");
        if (!isBaking)
        {
            StartCoroutine(BuildNavMeshCoroutine());
        }
    }

    private IEnumerator BuildNavMeshCoroutine()
    {
        isBaking = true;
        yield return new WaitForEndOfFrame(); 
        m_navMeshSurface.BuildNavMesh();
        isBaking = false;
    }

    public bool CheckPathValidity()
    {
        NavMeshPath path = new NavMeshPath();
        Vector3 startPos = GetStartPathPosition();
        Vector3 endPos = GetBasePosition();

        bool hasPath = NavMesh.CalculatePath(startPos, endPos, NavMesh.AllAreas, path);
        return path.status == NavMeshPathStatus.PathComplete;
    }

    private Vector3 GetStartPathPosition()
    {
        return m_pathStart ? m_pathStart.position : new Vector3(75f, 0f, -24f);
    }

    private Vector3 GetBasePosition()
    {
        return m_base ? m_base.position : new Vector3(25f,0f,24.5f);
    }
}
