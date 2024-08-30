using UnityEngine;
using System.Collections.Generic;

public class ChaseState : IEnemyState
{
    private List<Transform> _potentialTargets;

    public void EnterState(EnemyStateMachine stateMachine)
    {
        stateMachine.m_Animations.PlayAnimation(EnemyState.Chase);
        stateMachine.m_Stats.GetAudio().PlayChaseSound();
        DetermineTarget(stateMachine);
    }

    public void UpdateState(EnemyStateMachine stateMachine)
    {
        if (GameReferences.Instance.m_IsGameOver) return;

        // Ensure m_CurrentTarget is always set
        if (stateMachine.m_CurrentTarget == null)
        {
            DetermineTarget(stateMachine);
        }

        // Move towards the current target
        if (stateMachine.m_CurrentTarget != null)
        {
            if (stateMachine.m_CurrentTarget == stateMachine.m_BaseTarget)
            {
                if (stateMachine.m_NavigationBaseTarget == null)
                {
                    stateMachine.SetNavigationalBaseTarget(NavmeshManager.Instance.GetBasePositionFromArray(stateMachine.transform));
                }
                stateMachine.m_Movement.MoveToTarget(stateMachine.m_NavigationBaseTarget.position);

                if (stateMachine.m_Detection.IsInAttackRange(stateMachine.m_CurrentTarget) ||
                    Vector3.Distance(stateMachine.transform.position, stateMachine.m_NavigationBaseTarget.position) <= 1.75f)
                {
                    stateMachine.TransitionToState(stateMachine.m_AttackState);
                }

                if (!stateMachine.m_Detection.IsTargetInRange(stateMachine.m_CurrentTarget))
                {
                    DetermineTarget(stateMachine);
                }
            }
            else
            {
                stateMachine.m_Movement.MoveToTarget(stateMachine.m_CurrentTarget.position);

                if (stateMachine.m_Detection.IsInAttackRange(stateMachine.m_CurrentTarget))
                {
                    stateMachine.TransitionToState(stateMachine.m_AttackState);
                }

                if (!stateMachine.m_Detection.IsTargetInRange(stateMachine.m_CurrentTarget))
                {
                    DetermineTarget(stateMachine);
                }
            }
        }
        else
        {
            DetermineTarget(stateMachine);
        }
    }

    public void ExitState(EnemyStateMachine stateMachine)
    {
        stateMachine.m_Movement.StopMoving();
    }

    private void DetermineTarget(EnemyStateMachine stateMachine)
    {
        if (stateMachine.m_NavigationBaseTarget == null)
        {
            stateMachine.SetNavigationalBaseTarget(NavmeshManager.Instance.GetBasePositionFromArray(stateMachine.transform));
        }

        _potentialTargets = new List<Transform>
        {
            stateMachine.m_PlayerTarget,
            stateMachine.m_BaseTarget
        };

        // Add any valid targets within range
        Transform nearestTarget = stateMachine.m_Detection.GetNearestTarget();
        if (nearestTarget != null)
        {
            _potentialTargets.Add(nearestTarget);
        }

        Transform validTarget = GetValidTarget(stateMachine);
        if (validTarget != null)
        {
            stateMachine.m_CurrentTarget = validTarget;
        }
        else
        {
            // Default to the nearest target if no valid target is found
            stateMachine.m_CurrentTarget = nearestTarget;
        }

        if (stateMachine.m_CurrentTarget == stateMachine.m_BaseTarget && !NavmeshManager.Instance.CheckPathValidity())
        {
            stateMachine.m_CurrentTarget = validTarget;
        }

        // If no targets are valid, ensure we have a fallback to the base target
        if (stateMachine.m_CurrentTarget == null)
        {
            stateMachine.m_CurrentTarget = stateMachine.m_PlayerTarget;
        }

    }

    private Transform GetValidTarget(EnemyStateMachine stateMachine)
    {
        foreach (Transform target in _potentialTargets)
        {
            if (stateMachine.m_Detection.IsTargetInRange(target))
            {
                return target;
            }
        }
        return null;
    }
}
