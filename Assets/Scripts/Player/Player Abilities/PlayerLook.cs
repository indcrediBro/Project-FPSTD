using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerStats))]
public class PlayerLook : MonoBehaviour
{
    private PlayerStats m_stats;
    private float m_xRotation = 0f;
    private float m_joystickSensitivityMultiplier = 20f;

    private void Start()
    {
        m_stats = GetComponent<PlayerStats>();
    }

    public void Look(Vector2 _lookInput)
    {
        if (GameReferences.Instance.m_IsPaused) return;

        float mouseX = _lookInput.x * GetSensitivity() * Time.deltaTime;
        float mouseY = _lookInput.y * GetSensitivity() * Time.deltaTime;

        m_xRotation -= mouseY;
        m_xRotation = Mathf.Clamp(m_xRotation, -90f, 90f);

        m_stats.GetVirtualCameraComponent().transform.localRotation = Quaternion.Euler(m_xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    private float GetSensitivity()
    {
        if (Input.GetJoystickNames().Length != 0)
            return m_stats.GetLookSensitivity() * m_joystickSensitivityMultiplier;

        return m_stats.GetLookSensitivity();
    }
}
