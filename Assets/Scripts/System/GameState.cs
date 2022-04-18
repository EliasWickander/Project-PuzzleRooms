using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public bool Paused { get; set; }
    
    public static GameState Current { get; private set; }

    protected virtual void Awake()
    {
        Current = this;
    }

    protected virtual void OnEnable()
    {
        
    }

    protected virtual void OnDisable()
    {
        
    }
    
    protected virtual void Update()
    {
        
    }
}
