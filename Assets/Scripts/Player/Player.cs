using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(InteractionController))]
public class Player : MonoBehaviour
{
    [SerializeField] private Inventory m_inventory;

    private InteractionController m_interactionController = null;

    private void Awake()
    {
        m_interactionController = GetComponent<InteractionController>();
    }

    private void OnEnable()
    {
        m_interactionController.m_onItemCollected += OnItemCollected;
    }

    private void OnDisable()
    {
        m_interactionController.m_onItemCollected -= OnItemCollected;
    }
    
    private void OnItemCollected(ItemData item)
    {
        m_inventory.Add(item);
    }
    
}
