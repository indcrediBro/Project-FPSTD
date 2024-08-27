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
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        Time.timeScale = 0;
        GameReferences.Instance.m_IsPaused = true;
        MenuManager.Instance.OpenMenu("ShopPanel");

        AudioManager.Instance.StopSound("BGM_Game");
        AudioManager.Instance.StopSound("BGM_Gameover");
        AudioManager.Instance.StopSound("BGM_MainMenu");
        AudioManager.Instance.PlaySound("BGM_Shop");
    }

    public void Update()
    {
        if (InputManager.Instance.m_PauseInput.WasReleasedThisFrame())
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            m_gameManager.SetState(new PlayState(m_gameManager));
        }
    }

    public void Exit()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
        GameReferences.Instance.m_IsPaused = false;
        //MenuManager.Instance.CloseMenu("ShopPanel");
        MenuManager.Instance.ShowHUD();
    }
}
