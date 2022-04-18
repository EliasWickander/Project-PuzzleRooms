using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine
{
    public Action<GameStateType> OnStateEnter;
    public Action<GameStateType> OnStateExit;

    public GameStateType m_currentState;
    
    public enum GameStateType
    {
        Startup,
        Playing,
    }

    public void EnterState(GameStateType state)
    {
        if (state == m_currentState)
        {
            Debug.LogError("Tried to set to same state as current state");
            return;   
        }

        OnStateExit?.Invoke(state);
        m_currentState = state;
        OnStateEnter?.Invoke(state);
    }
}
