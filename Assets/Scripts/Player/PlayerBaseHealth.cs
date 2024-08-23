using UnityEngine;
using System.Collections;

public class PlayerBaseHealth : Health
{
    protected override void Die(float _timeBeforeRemoving)
    {
        base.Die(_timeBeforeRemoving);

        GameStateManager.Instance.SetState(new GameOverState(GameStateManager.Instance));
    }
}
