using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideOnPause : MonoBehaviour
{

    void Update()
    {
        if (GameReferences.Instance.m_IsPaused || GameReferences.Instance.m_IsGameOver)
            gameObject.SetActive(false);
        else
            gameObject.SetActive(true);
    }
}
