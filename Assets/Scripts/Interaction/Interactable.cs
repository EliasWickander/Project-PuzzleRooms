using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] 
    private ItemData m_requiredItem = null;

    protected virtual void Awake()
    {
        
    }

    protected virtual void Start()
    {
        
    }
    
    protected virtual void Update()
    {

    }

    
    public virtual bool OnInteractionAttempt(ItemData selectedItem)
    {
        if (selectedItem == m_requiredItem)
        {
            OnInteractionSuccess();
            return true;
        }
        else
        {
            OnInteractionFailed();
            return false;
        }
    }

    public virtual void OnInteractionSuccess()
    {
        
    }

    public virtual void OnInteractionFailed()
    {
        
    }
}
