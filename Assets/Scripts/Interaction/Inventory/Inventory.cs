using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "ScriptableObjects/Inventory")]
public class Inventory : ScriptableObject
{
    private List<ItemData> m_items = new List<ItemData>();
    public List<ItemData> Items => m_items;

    public event Action<ItemData> m_onItemAdded; 
    public event Action<ItemData> m_onItemRemoved;

    private void OnEnable()
    {
        Clear();
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

    public void Clear()
    {
        m_items.Clear();
    }
}
