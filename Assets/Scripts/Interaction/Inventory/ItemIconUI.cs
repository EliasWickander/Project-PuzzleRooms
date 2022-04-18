using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ItemIconUI : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private ItemData m_containedItem = null;
    
    private Image m_image = null;

    private RectTransform m_rectTransform;
    public RectTransform RectTransform => m_rectTransform;

    private Vector3 m_startPos = Vector3.zero;
    private bool m_isDragging = false;
    public bool IsDragging => m_isDragging;

    public event Action<ItemData, Vector2> OnItemAttemptInteract;
    
    private void Awake()
    {
        m_image = GetComponent<Image>();
        m_rectTransform = GetComponent<RectTransform>();
    }

    public void UpdateIcon(ItemData itemData)
    {
        m_containedItem = itemData;

        m_image.sprite = itemData != null ? itemData.m_iconSprite : null;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(m_rectTransform, eventData.position))
        {
            m_startPos = m_rectTransform.position;
            m_isDragging = true;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (m_isDragging)
        {
            OnItemAttemptInteract?.Invoke(m_containedItem, m_rectTransform.position);
            m_rectTransform.position = m_startPos;
            m_isDragging = false;
        }
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        if (m_isDragging)
        {
            m_rectTransform.position = eventData.position;   
        }
    }
}
