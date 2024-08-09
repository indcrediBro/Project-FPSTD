using UnityEngine;
using System.Collections;

public class PauseState : IGameState
{
    private GameManager m_gameManager;

    public PauseState(GameManager _gameManager)
    {
        this.m_gameManager = _gameManager;
    }

    public void Enter()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        Debug.Log("Entering Pause State");
        // Pause game logic
        Time.timeScale = 0;
    }

    public void Update()
    {
        // Transition back to PlayState
        if (Input.GetKeyDown(KeyCode.P))
        {
            m_gameManager.SetState(new PlayState(m_gameManager));
        }
    }

    public void Exit()
    {
        Debug.Log("Exiting Pause State");
        // Resume game logic
        Time.timeScale = 1;
    }
}
