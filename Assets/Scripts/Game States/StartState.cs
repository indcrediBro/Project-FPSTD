using UnityEngine;
using System.Collections;

public class StartState : IGameState
{
    public void Enter()
    {
        InitializeGameSettings();
    }

    public void Update()
    {

    }

    public void Exit()
    {

    }

    private void InitializeGameSettings()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }


}
