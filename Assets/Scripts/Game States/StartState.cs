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
    }

    public void Update()
    {
        //if (GameReferences.Instance.m_PlayerStats.GetPlayerHealthComponent().IsDead() || GameReferences.Instance.m_PlayerBase.IsDead())
        //{
        //    m_gameManager.SetState(new GameOverState(m_gameManager));
        //}
    }

    public void Exit()
    {
        MenuManager.Instance.ShowHUD();
    }

    private void InitializeGameSettings()
    {

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        MenuManager.Instance.HideHUD();
        MenuManager.Instance.OpenMenu("MainMenu");
    }


}
