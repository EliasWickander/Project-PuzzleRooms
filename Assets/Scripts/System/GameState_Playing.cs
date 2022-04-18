using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState_Playing : GameState
{
    protected override void Awake()
    {
        base.Awake();
        
        if(GameManager.Instance == null)
            Debug.LogError("GameManager instance is null");
        
        GameManager.Instance.GameStateMachine.EnterState(GameStateMachine.GameStateType.Playing);
    }
}
