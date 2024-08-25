using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : Singleton<GameStateManager>
{
    private IGameState m_currentState;

    void Start()
    {
        //SetState(new StartState(this));
    }

    void Update()
    {
        m_currentState?.Update();
    }

    public void SetState(IGameState _newState)
    {
        StartCoroutine(ChangeStateCO(_newState));
    }

    private IEnumerator ChangeStateCO(IGameState _newState)
    {
        m_currentState?.Exit();
        yield return new WaitForEndOfFrame();
        m_currentState = _newState;
        m_currentState.Enter();
        Debug.Log(m_currentState);
    }
}
