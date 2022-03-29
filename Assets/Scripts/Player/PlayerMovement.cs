using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] 
    private GameObject m_walkingArrows = null;
    
    [SerializeField] 
    private float m_moveSpeed = 5;

    private Camera m_camera = null;
    private CharacterController m_charController = null;
    private Coroutine m_currentMoveRoutine = null;
    
    public delegate void ReachedDestinationDelegate();

    private void Awake()
    {
        m_charController = GetComponent<CharacterController>();
        m_camera = Camera.main;
        
        m_walkingArrows.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = m_camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
            {
                MoveToPos(hit.point);
            }
        }
    }

    public void MoveToPos(Vector3 targetPos, ReachedDestinationDelegate onReachedDest = null)
    {
        Vector3 playerPos = transform.position;
        if (NavMesh.SamplePosition(targetPos, out NavMeshHit navTargetHit, 10, NavMesh.AllAreas))
        {
            if (NavMesh.SamplePosition(playerPos, out NavMeshHit navStartHit, 10, NavMesh.AllAreas))
            {
                NavMeshPath path = new NavMeshPath();
                if (NavMesh.CalculatePath(navStartHit.position, navTargetHit.position, NavMesh.AllAreas, path))
                {
                    if(m_currentMoveRoutine != null)
                        StopCoroutine(m_currentMoveRoutine);

                    m_walkingArrows.SetActive(false);
                    
                    m_walkingArrows.transform.position = navTargetHit.position;
                    m_walkingArrows.SetActive(true);
                    m_currentMoveRoutine = StartCoroutine(MoveAlongPath(path, onReachedDest));
                }
                else
                {
                    Debug.LogError("Failed to calculate path");
                }
            }
        }
    }

    public void StopMovement()
    {
        if (m_currentMoveRoutine != null)
        {
            StopCoroutine(m_currentMoveRoutine);
            m_walkingArrows.SetActive(false);
        }
    }
    private IEnumerator MoveAlongPath(NavMeshPath path, ReachedDestinationDelegate onReachedDest)
    {
        int currentNode = 0;
        int numNodes = path.corners.Length;

        while (true)
        {
            yield return new WaitForEndOfFrame();
            Vector3 targetPos = path.corners[currentNode];
            targetPos.y = transform.position.y;
            
            Vector3 dirToTarget = targetPos - transform.position;

            if (dirToTarget.magnitude <= 0.2f)
            {
                if (currentNode < numNodes - 1)
                {
                    currentNode++;   
                }
                else
                {
                    //Finished moving
                    onReachedDest?.Invoke();
                    yield break;
                }
            }
            else
            {
                dirToTarget.Normalize();
                m_charController.Move(dirToTarget * m_moveSpeed * Time.deltaTime);
            }
        }
    }
}
