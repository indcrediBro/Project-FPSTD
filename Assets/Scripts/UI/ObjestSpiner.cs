using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private float m_rotateSpeed;
    [SerializeField] private Vector3 m_rotation;
    void Update()
    {
        transform.Rotate(m_rotation*m_rotateSpeed*Time.deltaTime);
    }
}
