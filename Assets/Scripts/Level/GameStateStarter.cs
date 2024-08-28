using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateStarter : MonoBehaviour
{
    void Start()
    {
        if (!GameStateManager.Instance)
        {
            SceneManager.LoadScene("Splash Screen");
        }
        else
        {
            if (SceneManager.GetActiveScene().name == "Main Menu")
                GameStateManager.Instance.SetState(new StartState(GameStateManager.Instance));
        }
    }
}
