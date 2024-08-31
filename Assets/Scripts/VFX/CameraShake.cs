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
        transform.DOShakePosition(_shakeDuration, _shakeMagnitude);

        //if (SettingsManager.Instance.cameraShakeEnabled)
        //{
        //    m_shakeElapsedTime = 0;
        //    m_cameraTransform.localPosition = m_initialPosition;
        //    m_shakeMagnitude = _shakeMagnitude;
        //    m_shakeElapsedTime = _shakeDuration;
        //}
    }

    void Update()
    {
        //Shake();
    }

    private void Shake()
    {
        if (m_shakeElapsedTime > 0)
        {
            m_cameraTransform.localPosition = m_initialPosition + new Vector3(
                Mathf.PerlinNoise(Time.time * m_shakeMagnitude, 0f) * 2 - 1,
                Mathf.PerlinNoise(0f, Time.time * m_shakeMagnitude) * 2 - 1,
                0) * m_shakeMagnitude;

            m_shakeElapsedTime -= Time.deltaTime * m_dampingSpeed;
        }
        else
        {
            m_shakeElapsedTime = 0f;
            m_cameraTransform.localPosition = m_initialPosition;
        }
    }
}
