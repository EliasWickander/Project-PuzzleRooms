using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class InteractionController : MonoBehaviour
{
    [SerializeField] 
    private LayerMask m_interactableMask;

    [SerializeField] private float m_pickUpRadius = 5;
    
    private PlayerMovement m_movement = null;
    
    private Camera m_camera = null;

    private Interactable m_currentInteraction = null;

    public event Action<ItemData> m_onItemCollected;

    private void Awake()
    {
        m_movement = GetComponent<PlayerMovement>();
        m_camera = Camera.main;
    }

    private void Update()
    {
        if (CheckInteraction(out Interactable foundInteractable))
        {
            m_currentInteraction = foundInteractable;
            m_movement.MoveToPos(foundInteractable.transform.position);
        }

        if (m_currentInteraction != null)
        {
            if (IsInRangeOfCurrentInteraction())
            {
                Interact(m_currentInteraction);
            }
        }

    }

    private void Interact(Interactable interactable)
    {
        m_movement.StopMovement();
        m_currentInteraction.OnInteraction();
        
        if (interactable is Item item)
        {
            m_onItemCollected?.Invoke(item.Data);
        }
        
        m_currentInteraction = null;
        
        
    }
    
    public bool CheckInteraction(out Interactable foundInteractable)
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = m_camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, m_interactableMask))
            {
                foundInteractable = hit.collider.GetComponent<Interactable>();

                if (foundInteractable == null)
                {
                    Debug.LogError("Hit an object with interactable layer but it doesn't have the Interaction script on it");
                    return false;
                }

                if (!foundInteractable.CanInteract)
                {
                    Debug.LogError("Hit interactable object but its CanInteract is false");
                    return false;
                }
                
                return true;
            }
        }
        
        foundInteractable = null;
        return false;
    }

    private bool IsInRangeOfCurrentInteraction()
    {
        Collider[] interactionsInRange = Physics.OverlapSphere(transform.position, m_pickUpRadius, m_interactableMask);

        foreach (Collider interaction in interactionsInRange)
        {
            if (interaction.gameObject == m_currentInteraction.gameObject)
                return true;
        }

        return false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, m_pickUpRadius);
        Gizmos.color = Color.white;
    }
}
