using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private InventoryData m_inventory;

    [SerializeField] private LayerMask m_interactableMask;

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

        foreach (ItemUI itemUI in m_UIItems)
        {
            itemUI.OnItemAttemptInteract += OnItemAttemptInteract;
        }
    }

    private void OnDisable()
    {
        m_inventory.m_onItemAdded -= OnItemAdded;
        m_inventory.m_onItemRemoved -= OnItemRemoved;
        
        foreach (ItemUI itemUI in m_UIItems)
        {
            itemUI.OnItemAttemptInteract -= OnItemAttemptInteract;
        }
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

    public void OnItemAttemptInteract(ItemData itemData, Vector2 screenPos)
    {
        if (!AttemptItemWorldInteraction(itemData, screenPos))
        {
            AttemptCombination(itemData, screenPos);   
        }
    }

    private bool AttemptItemWorldInteraction(ItemData itemData, Vector2 screenPos)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPos);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, m_interactableMask))
        {
            Interactable foundInteractable = hit.collider.GetComponent<Interactable>();

            if (foundInteractable == null)
            {
                Debug.LogError("Hit an object with interactable layer but it doesn't have the Interaction script on it");
                return false;
            }

            foundInteractable.OnInteractionAttempt(itemData);
            return true;

        }
        
        return false;
    }
    
    private bool AttemptCombination(ItemData itemData, Vector3 screenPos)
    {
        foreach (ItemUI itemUI in m_UIItems)
        {
            if(itemUI.isActiveAndEnabled == false)
                continue;
            
            if(itemUI.IsSelected)
                continue;
            
            if (RectTransformUtility.RectangleContainsScreenPoint(itemUI.IconUI.RectTransform, screenPos))
            {
                return m_inventory.Combine(itemData, itemUI.ContainedItem);
            }
        }

        return false;
    }
}
