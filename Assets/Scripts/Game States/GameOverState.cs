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

        Debug.Log("Game Over State!");
    }

    public void Update()
    {
    }

    public void Exit()
    {
    }
}
