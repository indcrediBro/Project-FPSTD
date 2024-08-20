using UnityEngine;
using System.Collections;

public class ShopState : IGameState
{
    private GameStateManager m_gameManager;

    public ShopState(GameStateManager _gameManager)
    {
        this.m_gameManager = _gameManager;
    }

    public void Enter()
    {
        Debug.Log("Entering Shop State");
        // Shop game logic
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
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
    }
}
