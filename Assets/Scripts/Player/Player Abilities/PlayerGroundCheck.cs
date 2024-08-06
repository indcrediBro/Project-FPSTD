using UnityEngine;
using System.Collections;

public class PlayerGroundCheck : MonoBehaviour
{
    PlayerStats m_stats;
    [SerializeField] private Transform m_groundCheckTF;
    [SerializeField] private float m_groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask m_groundLayer;

    private void Awake()
    {
        m_stats = GetComponent<PlayerStats>();
    }

    void Update()
    {
        m_stats.SetGrounded(IsGrounded());
    }

    private bool IsGrounded()
    {
        Collider[] hits = Physics.OverlapSphere(m_groundCheckTF.position, m_groundCheckRadius, m_groundLayer);

        if (hits.Length > 0)
        {
            return true;
        }

        return false;
    }
}
