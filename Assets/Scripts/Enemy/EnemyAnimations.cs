using UnityEngine;
using System.Collections;

public class EnemyAnimations : MonoBehaviour
{
    private Animator m_animator;
    [SerializeField] private bool m_childAnimator;
    //For Debugging.
    [SerializeField] private EnemyState m_currentAnimationState;
    private void Awake()
    {
        if(m_childAnimator) m_animator = GetComponentInChildren<Animator>();
        else m_animator = GetComponent<Animator>();
    }

    public void PlayAnimation(EnemyState _state)
    {
        m_currentAnimationState = _state;
        switch (_state)
        {
            case EnemyState.Spawn:
                m_animator.Play("Spawn");
                break;
            case EnemyState.Idle:
                m_animator.Play("Idle");
                break;
            case EnemyState.ChaseBase:
                m_animator.Play("Move");
                break;
            case EnemyState.ChasePlayer:
                m_animator.Play("Move");
                break;
            case EnemyState.Attack:
                m_animator.Play("Attack "+ RandomNumber.Instance.NextInt(3));
                break;
            case EnemyState.Hurt:
                m_animator.Play("Hurt");
                break;
            case EnemyState.Burn:
                m_animator.Play("Burn");
                break;
            case EnemyState.Dead:
                m_animator.Play("Dead");
                break;
        }
    }

    public bool IsAnimationNotInProgress(string _clipName)
    {
        AnimatorStateInfo currentAnimationState = m_animator.GetCurrentAnimatorStateInfo(0);
        return currentAnimationState.IsName(_clipName) && currentAnimationState.normalizedTime >= 1.0f;
    }
}
