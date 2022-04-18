using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameTrigger : Interactable
{
    [SerializeField] 
    private MiniGameData m_triggeredMiniGame;

    protected override void Awake()
    {
        base.Awake();

        if (m_triggeredMiniGame == null)
        {
            Debug.LogError("Selected mini game can not be null. Click to locate object", gameObject);
            return;
        }
    }

    public override void OnInteractionSuccess()
    {
        base.OnInteractionSuccess();

        if (MiniGameManager.Instance == null)
        {
            Debug.LogError("Attempted to trigger mini game but MiniGameManager instance is null");
            return;
        }
        
        MiniGameManager.Instance.StartMiniGame(m_triggeredMiniGame);
    }
}
