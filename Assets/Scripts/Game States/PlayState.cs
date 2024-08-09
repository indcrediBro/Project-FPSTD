using UnityEngine;
using System.Collections;

public class PlayState : IGameState
{
    private GameManager m_gameManager;

    public PlayState(GameManager _gameManager)
    {
        this.m_gameManager = _gameManager;
    }

    public void Enter()
    {
        Debug.Log("Entering Play State");
        // Start game logic
    }

    public void Update()
    {
        // Check for pause condition
        if (Input.GetKeyDown(KeyCode.P))
        {
            m_gameManager.SetState(new PauseState(m_gameManager));
        }
        // Check for game over condition
        if (/* game over condition */ false)
        {
            //m_gameManager.SetState(new GameOverState(m_gameManager));
        }
    }

    public void Exit()
    {
        Debug.Log("Exiting Play State");
    }
}
