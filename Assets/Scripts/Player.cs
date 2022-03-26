using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    [SerializeField] 
    private float m_moveSpeed = 5;

    [SerializeField] 
    private GameObject m_walkingArrows = null;
    
    private CharacterController m_charController;
    private Camera m_camera;

    private Coroutine m_currentMoveRoutine = null;

    private void Awake()
    {
        m_charController = GetComponent<CharacterController>();
        m_camera = Camera.main;
        
        m_walkingArrows.SetActive(false);
    }

    // Update is called once per frame
    void Update()
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

    private void MoveToPos(Vector3 targetPos)
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
                    m_currentMoveRoutine = StartCoroutine(MoveAlongPath(path));
                }
                else
                {
                    Debug.LogError("Failed to calculate path");
                }
            }
        }
    }

    private IEnumerator MoveAlongPath(NavMeshPath path)
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
                    yield break;
                    //Finished moving
                }
            }
            else
            {
                dirToTarget.Normalize();
                m_charController.Move(dirToTarget * m_moveSpeed * Time.deltaTime);
            }
        }
    }

    public void Shrink()
    {
        StartCoroutine(Shrink_Internal());
    }

    private IEnumerator Shrink_Internal()
    {
        Vector3 startScale = transform.localScale;

        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(0.2f);
            
            transform.localScale -= startScale * 0.2f;
        }
    }
}
