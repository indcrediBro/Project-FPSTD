using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateStarter : MonoBehaviour
{
    void Start()
    {
        if (GameStateManager.Instance)
        {
            GameStateManager.Instance.SetState(new StartState(GameStateManager.Instance));
        }
        else
        {
            SceneManager.LoadScene("Splash Screen");
        }
    }
}
