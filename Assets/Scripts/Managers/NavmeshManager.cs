using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;

public class NavmeshManager : MonoBehaviour
{
    [SerializeField] private NavMeshSurface m_navMeshSurface;

    void Start()
    {
        BuildNavMesh();
    }

    public void BuildNavMesh()
    {
        m_navMeshSurface.BuildNavMesh();
    }

    public bool IsPathAvailable(Vector3 _startPosition, Vector3 _targetPosition)
    {
        NavMeshPath path = new NavMeshPath();
        NavMesh.CalculatePath(_startPosition, _targetPosition, NavMesh.AllAreas, path);
        return path.status == NavMeshPathStatus.PathComplete;
    }
}
