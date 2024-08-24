using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideOnPause : MonoBehaviour
{

    void LateUpdate()
    {
        if (GameReferences.Instance.m_IsPaused)
            gameObject.SetActive(false);
        else
            gameObject.SetActive(true);
    }
}
