using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float m_dampingSpeed = 0.1f;

    private Transform m_cameraTransform;
    private Vector3 m_initialPosition;
    private float m_shakeMagnitude;
    private float m_shakeElapsedTime;

    void Awake()
    {
        if (m_cameraTransform == null)
        {
            m_cameraTransform = GetComponent<Transform>();
        }
    }

    void OnEnable()
    {
        m_initialPosition = m_cameraTransform.localPosition;
    }

    public void TriggerShake(float _shakeDuration = 0.25f, float _shakeMagnitude = .25f)
    {
        transform.DOShakePosition(_shakeDuration, _shakeMagnitude).OnComplete(() => transform.DOLocalMoveY(0.8f, 0f));
    }
}
