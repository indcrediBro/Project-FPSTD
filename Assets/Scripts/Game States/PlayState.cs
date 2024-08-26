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
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
        GameReferences.Instance.m_IsPaused = false;
        GameReferences.Instance.m_IsGameOver = false;
    }

    public void Update()
    {
        MenuManager.Instance.ShowHUD();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (!InputManager.Instance || !GameReferences.Instance) { return; }

        if (InputManager.Instance.m_PauseInput.WasReleasedThisFrame() && GameReferences.Instance.m_IsPaused == false)
        {
            m_gameManager.SetState(new PauseState(m_gameManager));
        }
        if (GameReferences.Instance.m_PlayerStats && GameReferences.Instance.m_PlayerBase)
        {
            if (GameReferences.Instance.m_PlayerStats.GetPlayerHealthComponent().IsDead() || GameReferences.Instance.m_PlayerBase.IsDead())
            {
                m_gameManager.SetState(new GameOverState(m_gameManager));
            }
        }
    }

    public void Exit()
    {

    }
}
