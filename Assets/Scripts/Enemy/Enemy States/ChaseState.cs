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
                HandleBaseTarget(stateMachine);
            }
            else
            {
                HandleOtherTargets(stateMachine);
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

        // Initialize potential targets list
        _potentialTargets = new List<Transform>
        {
            stateMachine.m_PlayerTarget, // Priority to the player
            stateMachine.m_BaseTarget    // Always consider the base
        };

        // Add any valid targets within range (like turrets, barricades)
        Transform nearestToBase = NavmeshManager.Instance.GetBasePositionFromArray(stateMachine.m_BaseTarget);
        if (nearestToBase != null)
        {
            _potentialTargets.Add(nearestToBase);
        }

        // Prioritize targets
        stateMachine.m_CurrentTarget = GetValidTarget(stateMachine);

        // Ensure the path to the base is valid, otherwise choose the nearest target to the base
        if (stateMachine.m_CurrentTarget == stateMachine.m_BaseTarget && !NavmeshManager.Instance.CheckPathValidity())
        {
            stateMachine.m_CurrentTarget = nearestToBase;
        }

        // Fallback to the base if no other valid target is found
        if (stateMachine.m_CurrentTarget == null)
        {
            stateMachine.m_CurrentTarget = stateMachine.m_BaseTarget;
        }
    }

    private Transform GetValidTarget(EnemyStateMachine stateMachine)
    {
        // Prioritize player if in range, else consider other targets
        foreach (Transform target in _potentialTargets)
        {
            if (target != null && stateMachine.m_Detection.IsTargetInRange(target))
            {
                return target;
            }
        }
        return null;
    }

    private void HandleBaseTarget(EnemyStateMachine stateMachine)
    {
        // Use the navigation target for movement but attack the base directly
        if (stateMachine.m_NavigationBaseTarget == null)
        {
            stateMachine.SetNavigationalBaseTarget(NavmeshManager.Instance.GetBasePositionFromArray(stateMachine.transform));
        }

        stateMachine.m_Movement.MoveToTarget(stateMachine.m_NavigationBaseTarget.position);

        if (stateMachine.m_Detection.IsInAttackRange(stateMachine.m_BaseTarget) ||
            Vector3.Distance(stateMachine.transform.position, stateMachine.m_NavigationBaseTarget.position) <= 1.75f)
        {
            stateMachine.TransitionToState(stateMachine.m_AttackState);
        }

        if (!stateMachine.m_Detection.IsTargetInRange(stateMachine.m_CurrentTarget))
        {
            DetermineTarget(stateMachine);
        }
    }

    private void HandleOtherTargets(EnemyStateMachine stateMachine)
    {
        // Move towards and attack the current target if it's not the base
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
