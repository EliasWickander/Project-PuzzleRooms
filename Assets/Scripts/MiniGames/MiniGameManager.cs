using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    public static MiniGameManager Instance { get; private set; }
    
    public MiniGame CurrentMiniGame => m_currentMiniGame;
    private MiniGame m_currentMiniGame = null;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (m_currentMiniGame != null)
        {
            m_currentMiniGame.Update();
        }
    }

    public void StartMiniGame(MiniGameData miniGameData)
    {
        if (m_currentMiniGame != null)
        {
            Debug.LogError("Trying to start a mini game even though there is already one running. Complete or abort it first.");
            return;
        }

        if (miniGameData == null)
        {
            Debug.LogError("Started mini game data can not be null");
            return;
        }

        m_currentMiniGame = miniGameData.GetMiniGame();
        
        m_currentMiniGame.OnCompleted += CompleteCurrentMiniGame;
        m_currentMiniGame.OnEnter();
    }
    
    public void CompleteCurrentMiniGame()
    {
        if(m_currentMiniGame == null)
            return;
        
        m_currentMiniGame.OnExit();
        m_currentMiniGame.OnCompleted -= CompleteCurrentMiniGame;
        m_currentMiniGame = null;
    }

    public void AbortCurrentMiniGame()
    {
        if(m_currentMiniGame == null)
            return;
        
        m_currentMiniGame.OnExit();
        m_currentMiniGame.OnCompleted -= CompleteCurrentMiniGame;
        m_currentMiniGame = null;
        
        Debug.Log("Aborted mini game");
    }
}
