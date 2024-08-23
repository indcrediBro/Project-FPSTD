using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpinner : MonoBehaviour
{
    [SerializeField] private float m_rotateSpeed = 50f;
    [SerializeField] private Vector3 m_rotation = Vector3.up;

    private void LateUpdate()
    {
        transform.Rotate(m_rotation*m_rotateSpeed*Time.deltaTime);
    }
}
