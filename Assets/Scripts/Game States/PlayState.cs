using UnityEngine;
using System.Collections;

public class PlayState : IGameState
{
    private GameStateManager m_gameManager;

    public PlayState(GameStateManager _gameManager)
    {
        this.m_gameManager = _gameManager;
    }

    public void Enter()
    {
        Debug.Log("Entering Play State");
        // Start game logic
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Update()
    {
        // Check for pause condition
        if (InputManager.Instance.m_PauseInput.WasPressedThisFrame())
        {
            m_gameManager.SetState(new PauseState(m_gameManager));
        }
        // Check for game over condition
        if (GameReferences.Instance.m_PlayerStats.GetPlayerHealthComponent().IsDead() && GameReferences.Instance.m_PlayerBase.IsDead())
        {
            m_gameManager.SetState(new GameOverState(m_gameManager));
        }
    }

    public void Exit()
    {
        Debug.Log("Exiting Play State");
    }
}
