using UnityEngine;
using System.Collections;

public class PauseState : IGameState
{
    private GameStateManager m_gameManager;

    public PauseState(GameStateManager _gameManager)
    {
        this.m_gameManager = _gameManager;
    }

    public void Enter()
    {
        Debug.Log("Entering Pause State");
        // Pause game logic
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        Time.timeScale = 0;
    }

    public void Update()
    {
        // Transition back to PlayState
        if (InputManager.Instance.m_PauseInput.WasPressedThisFrame())
        {
            m_gameManager.SetState(new PlayState(m_gameManager));
        }
    }

    public void Exit()
    {
        Debug.Log("Exiting Pause State");
        // Resume game logic
    }
}
