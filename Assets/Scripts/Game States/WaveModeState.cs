using UnityEngine;
using System.Collections;

public class WaveModeState : IGameState
{
    private GameManager m_gameManager;

    public WaveModeState(GameManager _gameManager)
    {
        this.m_gameManager = _gameManager;
    }

    public void Enter()
    {
        Debug.Log("Starting Wave...");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Update()
    {
        
    }

    public void Exit()
    {
        Debug.Log("Wave Completed.");
    }
}
