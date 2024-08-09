using UnityEngine;
using System.Collections;

public class StartState : IGameState
{
    private GameManager m_gameManager;

    public StartState(GameManager _gameManager)
    {
        this.m_gameManager = _gameManager;
    }

    public void Enter()
    {
        Debug.Log("Entering Start State");
        // Initialize the game
        InitializeGameSettings();
    }

    public void Update()
    {
        // Transition to PlayState on some condition, e.g., user presses a key
        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_gameManager.SetState(new PlayState(m_gameManager));
        }
    }

    public void Exit()
    {
        Debug.Log("Exiting Start State");
    }

    private void InitializeGameSettings()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

    }


}
