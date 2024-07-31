using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerStats))]
public class PlayerLook : MonoBehaviour
{
    private PlayerStats m_stats;
    private float m_xRotation = 0f;

    private void Start()
    {
        m_stats = GetComponent<PlayerStats>();
    }

    public void Look(Vector2 _lookInput)
    {
        float mouseX = _lookInput.x * m_stats.GetLookSensitivity() * Time.deltaTime;
        float mouseY = _lookInput.y * m_stats.GetLookSensitivity() * Time.deltaTime;

        m_xRotation -= mouseY;
        m_xRotation = Mathf.Clamp(m_xRotation, -90f, 90f);

        m_stats.GetVirtualCamera().transform.localRotation = Quaternion.Euler(m_xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}
