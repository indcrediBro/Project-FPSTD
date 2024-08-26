using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailResetter : MonoBehaviour
{
    [SerializeField] private TrailRenderer m_trailRenderer;

    private void Awake()
    {
        m_trailRenderer = GetComponent<TrailRenderer>();
    }

    private void OnDisable()
    {
        m_trailRenderer.Clear();
    }
}
