using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : Singleton<GameStateManager>
{
    private IGameState m_currentState;

    void Start()
    {
        SetState(new StartState());
    }

    void Update()
    {
        m_currentState?.Update();
    }

    public void SetState(IGameState newState)
    {
        m_currentState?.Exit();
        m_currentState = newState;
        m_currentState?.Enter();
    }
}
