using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ItemUI : MonoBehaviour
{
    private ItemData m_containedItem = null;
    
    private Image m_image = null;

    private void Awake()
    {
        m_image = GetComponent<Image>();
        
        ClearItem();
    }

    public void AddItem(ItemData item)
    {
        gameObject.SetActive(true);
        m_image.sprite = item.m_iconSprite;
        m_containedItem = item;
    }

    public void ClearItem()
    {
        m_image.sprite = null;

        gameObject.SetActive(false);
    }
}
