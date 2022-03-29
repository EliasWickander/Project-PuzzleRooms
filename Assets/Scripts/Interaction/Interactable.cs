using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField]
    private bool m_canInteract = true;
    public bool CanInteract => m_canInteract;

    protected virtual void Awake()
    {
        
    }

    protected virtual void Start()
    {
        
    }
    
    protected virtual void Update()
    {

    }

    
    public virtual void OnInteraction()
    {
        
    }
}
