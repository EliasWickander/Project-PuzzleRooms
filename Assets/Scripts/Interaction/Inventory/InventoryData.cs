using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "ScriptableObjects/InventorySystem/InventoryData")]
public class InventoryData : ScriptableObject
{
    [SerializeField] 
    private ItemCombinationLibraryData m_itemCombinationLibrary;
    
    private List<ItemData> m_items = new List<ItemData>();
    public List<ItemData> Items => m_items;

    public event Action<ItemData> m_onItemAdded; 
    public event Action<ItemData> m_onItemRemoved;

    private void OnEnable()
    {
        Clear();
        
        if(m_itemCombinationLibrary == null)
            Debug.LogError("Item combination library can not be null");
    }

    public void Add(ItemData data)
    {
        if (m_items.Contains(data))
            throw new Exception("Tried to add duplicate " + data.m_name + " to inventory");
        
        m_items.Add(data);
        m_onItemAdded?.Invoke(data);
    }

    public void Remove(ItemData data)
    {
        if (!m_items.Contains(data))
            throw new Exception("Tried to remove " + data.m_name + " but it doesn't exist in inventory");
        
        m_items.Remove(data);
        m_onItemRemoved?.Invoke(data);
    }

    public bool Combine(ItemData firstItem, ItemData secondItem)
    {
        if (!m_items.Contains(firstItem) || !m_items.Contains(secondItem))
        {
            Debug.LogError("Attempting to combine one or more items that are not present in inventory. Aborting...");
            return false;
        }

        if (!m_itemCombinationLibrary.CheckCombinationResult(firstItem, secondItem, out ItemData result))
        {
            //Can't combine these items
            return false;
        }
        
        Remove(firstItem);
        Remove(secondItem);
        Add(result);
        
        return true;
    }
    
    public void Clear()
    {
        m_items.Clear();
    }
}
