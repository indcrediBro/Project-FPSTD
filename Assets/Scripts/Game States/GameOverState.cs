﻿using UnityEngine;
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
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        Time.timeScale = 0;
        GameReferences.Instance.m_IsPaused = true;
        GameReferences.Instance.m_IsGameOver = true;
        MenuManager.Instance.HideHUD();
        MenuManager.Instance.OpenMenu("GameOverPanel");
        AudioManager.Instance.StopSound("BGM_Game");
        AudioManager.Instance.StopSound("BGM_Shop");
        AudioManager.Instance.StopSound("BGM_MainMenu");
        AudioManager.Instance.PlaySound("BGM_Gameover");
    }

    public void Update()
    {

    }

    public void Exit()
    {
        ScoreManager.Instance.ResetScore();
        MenuManager.Instance.SetGameOverText("");
    }
}
