using UnityEngine;
using System.Collections;

public class GameOverState : IGameState
{
    private GameStateManager m_gameManager;

    public GameOverState(GameStateManager _gameManager)
    {
        this.m_gameManager = _gameManager;
    }

    public void Enter()
    {
        Debug.Log("Entering Game Over State");
        // Handle game over logic
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void Update()
    {
        // Transition back to StartState or PlayState
        if (Input.GetKeyDown(KeyCode.R))
        {
            m_gameManager.SetState(new StartState(m_gameManager));
        }
    }

    public void Exit()
    {
        Debug.Log("Exiting Game Over State");
    }
}
