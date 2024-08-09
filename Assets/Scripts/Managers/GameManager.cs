using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private IGameState currentState;

    void Start()
    {
        SetState(new StartState(this));
    }

    void Update()
    {
        currentState?.Update();
    }

    public void SetState(IGameState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }
}
