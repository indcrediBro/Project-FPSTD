using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected bool m_isPlayerOwner;
    [SerializeField] protected float m_damage;
    [SerializeField] protected float m_speed;
    [SerializeField] private float m_lifetime = 5f;

    protected virtual void Start()
    {
        //TODO:Use Object Pooling
        Destroy(gameObject, m_lifetime);
        GetComponent<Rigidbody>().AddForce(transform.forward * m_speed * Time.deltaTime,ForceMode.Impulse);
    }

    public virtual void Initialize(float _damageAmount, float _speed, bool _isPlayer)
    {
        m_damage = _damageAmount;
        m_speed = _speed;
        m_isPlayerOwner = _isPlayer;
    }

    protected abstract void OnImpact(Collider _other);

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (m_isPlayerOwner && other.tag != "Player")
            OnImpact(other);
    }
}
