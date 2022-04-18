using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    [SerializeField] 
    private ItemIconUI m_iconUI;

    public ItemIconUI IconUI => m_iconUI;

    private ItemData m_containedItem = null;
    public ItemData ContainedItem => m_containedItem;

    public bool IsSelected => m_iconUI.IsDragging;
    public Action<ItemData, Vector2> OnItemAttemptInteract;

    private void Awake()
    {
        if (m_iconUI == null)
        {
            Debug.LogError("IconUI cannot be null. Disabling script", gameObject);
            enabled = false;
            return;
        }

        ClearItem();
    }

    private void OnEnable()
    {
        m_iconUI.OnItemAttemptInteract += OnItemAttemptInteractCallback;
    }

    private void OnDisable()
    {
        m_iconUI.OnItemAttemptInteract -= OnItemAttemptInteractCallback;
    }

    public void AddItem(ItemData item)
    {
        gameObject.SetActive(true);
        m_iconUI.UpdateIcon(item);
        
        m_containedItem = item;
    }

    public void ClearItem()
    {
        m_iconUI.UpdateIcon(null);
        gameObject.SetActive(false);
    }

    private void OnItemAttemptInteractCallback(ItemData itemData, Vector2 screenPos)
    {
        OnItemAttemptInteract?.Invoke(itemData, screenPos);
    }
}
