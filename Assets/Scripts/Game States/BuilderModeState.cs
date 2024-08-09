using UnityEngine;
using System.Collections;

public class BuilderModeState : IGameState
{
    private GameManager m_gameManager;

    public BuilderModeState(GameManager _gameManager)
    {
        this.m_gameManager = _gameManager;
    }

    public void Enter()
    {
        Debug.Log("Entering Builder Mode...");
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        // Enable building UI and logic
        // Provide initial resources

    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // Exit build mode
        {
            m_gameManager.SetState(new WaveModeState(m_gameManager));
        }
    }

    public void Exit()
    {
        Debug.Log("Exiting Builder Mode...");
    }
}
