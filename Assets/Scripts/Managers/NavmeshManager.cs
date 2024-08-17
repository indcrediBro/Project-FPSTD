using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;
using System.Collections;

public class NavmeshManager : Singleton<NavmeshManager>
{
    [SerializeField] private NavMeshSurface m_navMeshSurface;
    [SerializeField] private Transform m_pathStart;
    [SerializeField] private Transform[] m_pathValidationTargets;
    private bool m_isBaking;

    void Start()
    {
        //BuildNavMesh();
    }



    public void BuildNavMesh()
    {
        Debug.Log("BuildingMesh!");
        if (!m_isBaking)
        {
            StartCoroutine(BuildNavMeshCoroutine());
        }
    }

    private IEnumerator BuildNavMeshCoroutine()
    {
        m_isBaking = true;
        yield return new WaitForEndOfFrame(); 
        m_navMeshSurface.BuildNavMesh();
        m_isBaking = false;
    }

    public bool CheckPathValidity()
    {
        return CheckPathValidityFromArray();
    }

    private bool VerifyPath(Vector3 _endPos)
    {
        NavMeshPath path = new NavMeshPath();
        Vector3 startPos = GetStartPathPosition();

        bool hasPath = NavMesh.CalculatePath(startPos, _endPos, NavMesh.AllAreas, path);
        return path.status == NavMeshPathStatus.PathComplete;
    }
    private bool CheckPathValidityFromArray()
    {
        for (int i = 0; i < m_pathValidationTargets.Length; i++)
        {
            if (VerifyPath(m_pathValidationTargets[i].position))
                return true;
        }
        return false;
    }

    private Vector3 GetStartPathPosition()
    {
        return m_pathStart ? m_pathStart.position : new Vector3(75f, 0f, -24f);
    }

    public Transform GetBasePositionFromArray(Transform _startTF)
    {
        Transform basePos = null;
        float closestDistance = Mathf.Infinity;

        for (int i = 0; i < m_pathValidationTargets.Length; i++)
        {
            if (VerifyPath(m_pathValidationTargets[i].position))
            {
                float currentDistance = Vector3.Distance(m_pathValidationTargets[i].position, _startTF.position);
                if(currentDistance < closestDistance)
                {
                    basePos = m_pathValidationTargets[i];
                    closestDistance = currentDistance;
                }
            }
        }

        return basePos;
    }
}
