using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    Spawn,
    Idle,
    Chase,
    //ChaseBase,
    //ChasePlayer,
    Attack,
    Hurt,
    Burn,
    Dead,
}

public class EnemyStateMachine : MonoBehaviour
{
    public IEnemyState m_CurrentState { get; private set; }

    // Public states
    public readonly IEnemyState m_SpawnState = new SpawnState();
    public readonly IEnemyState m_IdleState = new IdleState();
    public readonly IEnemyState m_ChaseState = new ChaseState();
    //public readonly IEnemyState m_ChaseBaseState = new ChaseBaseState();
    //public readonly IEnemyState m_ChasePlayerState = new ChasePlayerState();
    public readonly IEnemyState m_AttackState = new AttackState();
    public readonly IEnemyState m_DeadState = new DeadState();
    public readonly IEnemyState m_HurtState = new HurtState();
    public readonly IEnemyState m_BurnState = new BurnState();

    // References to other components
    public EnemyStats m_Stats { get; private set; }
    public EnemyHealth m_Health { get; private set; }
    public EnemyMovement m_Movement { get; private set; }
    public EnemyAttack m_Attack { get; private set; }
    public EnemyAnimations m_Animations { get; private set; }
    public EnemyDetection m_Detection { get; private set; }
    public EnemyBurn m_Burn { get; private set; }

    public Transform m_CurrentTarget { get; set; }
    public Transform m_PlayerTarget { get; private set; }
    public Transform m_BaseTarget { get; private set; }
    public Transform m_NavigationBaseTarget { get; private set; }

    private void Awake()
    {
        m_Stats = GetComponent<EnemyStats>();
        m_Health = GetComponent<EnemyHealth>();
        m_Movement = GetComponent<EnemyMovement>();
        m_Attack = GetComponentInChildren<EnemyAttack>();
        m_Animations = GetComponent<EnemyAnimations>();
        m_Detection = GetComponent<EnemyDetection>();
        m_Burn = GetComponent<EnemyBurn>();
        m_PlayerTarget = GameReferences.Instance.m_PlayerStats.transform;
        m_BaseTarget = GameReferences.Instance.m_PlayerBase?.transform;
    }

    private void Start()
    {
        //TransitionToState(m_SpawnState);
    }

    private void Update()
    {
        m_CurrentState.UpdateState(this);
    }

    public void TransitionToState(IEnemyState _newState)
    {
        m_CurrentState?.ExitState(this);
        m_CurrentState = _newState;
        m_CurrentState.EnterState(this);

        Debug.Log("New State: " + m_CurrentState);
    }

    public void SetNavigationalBaseTarget(Transform _navigationalTargetTile)
    {
        m_NavigationBaseTarget = _navigationalTargetTile;
    }
}
