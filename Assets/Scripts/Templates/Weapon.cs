using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected string m_weaponName;
    [SerializeField] protected float m_damage;
    [SerializeField] protected Transform m_weaponTransform;
    [SerializeField] protected AudioSource m_weaponAudioSource;
    [SerializeField] protected Animator m_weaponAnimator;

    private bool canAttack;

    public abstract void Attack();
    public void EnableCanAttack()
    {
        canAttack = true;
    }
    public void DisableCanAttack()
    {
        canAttack = true;
    }

    protected virtual bool CanAttack()
    {
        return canAttack;
    }

    protected virtual void PlayAttackAnimation(string animationName)
    {
        m_weaponAnimator?.Play(animationName);
    }
}
