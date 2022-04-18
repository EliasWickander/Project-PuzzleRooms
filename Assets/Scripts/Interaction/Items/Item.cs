using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Interactable
{
    [SerializeField]
    private ItemData m_data;

    public ItemData Data => m_data;
    
    [SerializeField]
    private float m_lifeTimeOnPickup = 0;

    private float m_lifeTimer = 0;

    private bool m_isPickedUp = false;

    protected override void Awake()
    {
        base.Awake();
        
        if(m_data == null)
            Debug.LogError("Data is null. Click to locate", gameObject);
    }

    protected override void Update()
    {
        base.Update();

        if (m_isPickedUp)
        {
            if (m_lifeTimer >= m_lifeTimeOnPickup)
            {
                Destroy(gameObject);
            }
            else
            {
                m_lifeTimer += Time.deltaTime;   
            }
        }
    }

    public override void OnInteractionSuccess()
    {
        base.OnInteractionSuccess();
        
        m_isPickedUp = true;
    }
}
