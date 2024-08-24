using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoLoadLevel : MonoBehaviour
{
    [SerializeField] private string m_levelToLoad = "Main Menu";
    [SerializeField] private bool m_autoLoad = true;

    void Start()
    {
        if (m_autoLoad) Invoke(nameof(LoadScene), 1f);
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(m_levelToLoad);
    }
}
