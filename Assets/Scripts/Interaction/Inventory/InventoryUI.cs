using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Inventory m_inventory;

    private ItemUI[] m_UIItems;

    private void Awake()
    {
        if(m_inventory == null)
            Debug.LogError("Inventory is null. Click to locate", gameObject);

        m_UIItems = GetComponentsInChildren<ItemUI>(true);
    }

    private void Start()
    {
        foreach (ItemData item in m_inventory.Items)
        {
            OnItemAdded(item);
        }
    }

    private void OnEnable()
    {
        m_inventory.m_onItemAdded += OnItemAdded;
        m_inventory.m_onItemRemoved += OnItemRemoved;
    }

    private void OnDisable()
    {
        m_inventory.m_onItemAdded -= OnItemAdded;
        m_inventory.m_onItemRemoved -= OnItemRemoved;
    }

    private void OnItemAdded(ItemData item)
    {
        if (m_inventory.Items.Count > m_UIItems.Length)
        {
            //More items added than there are UI elements for
            return;   
        }
        
        m_UIItems[m_inventory.Items.Count - 1].AddItem(item);
    }

    private void OnItemRemoved(ItemData item)
    {
        if (m_UIItems.Length <= 0)
        {
            //No item UI objects in canvas, so there is nothing to remove
            return;   
        }
        
        m_UIItems[m_inventory.Items.Count].ClearItem();
    }
}
