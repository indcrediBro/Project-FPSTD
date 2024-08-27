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
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        Time.timeScale = 0;
        GameReferences.Instance.m_IsPaused = true;
        MenuManager.Instance.HideHUD();
        MenuManager.Instance.OpenMenu("PauseMenu");
        AudioManager.Instance.StopSound("BGM_Game");
        AudioManager.Instance.StopSound("BGM_Gameover");
        AudioManager.Instance.StopSound("BGM_Shop");
        AudioManager.Instance.PlaySound("BGM_MainMenu");
    }

    public void Update()
    {
        if (InputManager.Instance.m_PauseInput.WasReleasedThisFrame() && GameReferences.Instance.m_IsPaused == true)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            m_gameManager.SetState(new PlayState(m_gameManager));
        }
    }

    public void Exit()
    {
        MenuManager.Instance.ShowHUD();
    }
}
