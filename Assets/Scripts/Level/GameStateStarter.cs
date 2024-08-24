using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateStarter : MonoBehaviour
{
    void Start()
    {
        GameStateManager.Instance.SetState(new StartState(GameStateManager.Instance));
    }
}
