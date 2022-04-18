using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MiniGame
{
    protected MiniGameData m_data = null;
    
    public Action OnCompleted;

    public MiniGame(MiniGameData data)
    {
        this.m_data = data;
    }

    public abstract void OnEnter();

    public abstract void Update();

    public abstract void OnExit();
}
