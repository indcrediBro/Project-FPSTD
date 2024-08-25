using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public string m_weaponName;
    [SerializeField] protected float m_damage;
    [SerializeField] protected int m_level = 1;
    [SerializeField] protected Transform m_weaponTransform;
    //[SerializeField] protected AudioSource m_weaponAudioSource;
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

    public void UpgradeLevel()
    {
        m_level++;
    }

    public int GetLevelNumber()
    {
        return m_level;
    }

    public float GetCurrentDamage()
    {
        return m_damage * (1 + m_level * 0.2f);
    }

    protected virtual bool CanAttack()
    {
        return canAttack;
    }

    protected virtual void PlayAnimation(string animationName)
    {
        m_weaponAnimator?.Play(animationName);
    }
}
