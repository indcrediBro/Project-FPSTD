using UnityEngine;
using System.Collections;

public class StartState : IGameState
{
    private GameStateManager m_gameManager;

    public StartState(GameStateManager _gameManager)
    {
        this.m_gameManager = _gameManager;
    }

    public void Enter()
    {
        InitializeGameSettings();
        AudioManager.Instance.StopSound("BGM_Game");
        AudioManager.Instance.StopSound("BGM_Gameover");
        AudioManager.Instance.StopSound("BGM_Shop");
        AudioManager.Instance.PlaySound("BGM_MainMenu");
    }

    public void Update()
    {
    }

    public void Exit()
    {
        MenuManager.Instance.ShowHUD();
    }

    private void InitializeGameSettings()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        MenuManager.Instance.OpenMenu("MainMenu");
        MenuManager.Instance.HideHUD();
    }


}
