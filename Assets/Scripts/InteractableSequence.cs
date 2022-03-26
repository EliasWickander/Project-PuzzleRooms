using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//TODO: Fix so that this works even when first segment's time is 0. (Currently starts next segment two times in that case)
public class InteractableSequence : MonoBehaviour
{
    [Serializable]
    public class Sequence
    {
        public string m_name = "Sequence";
        public SequenceType m_type;
        public List<SequenceSegment> m_segments;
    }
    [Serializable]
    public class SequenceSegment
    {
        public float m_time = 0;
        public UnityEvent m_events;
    }

    public enum SequenceType
    {
        Automatic,
        Manual
    }
		
    public List<Sequence> m_sequences;

    private Sequence m_currentSequence = null;
    private int m_currentSegmentIndex = -1;

    private bool m_isRunningSequence = false;

    private float m_targetTime = 0;
    private float m_timer = 0;

    private Camera m_camera;

    private void Awake()
    {
        m_camera = Camera.main;
    }
    
    private void Update()
    {
        if(IsClickedAt() && !m_isRunningSequence)
            StartSequence("OnClicked");
        
        if (m_isRunningSequence)
        {
            if (m_currentSequence.m_type != SequenceType.Automatic)
                return;
				
            if (m_timer > m_targetTime)
            {
                StartNextSegment();
            }
            else
            {
                m_timer += Time.deltaTime;	
            }
        }
    }
    
    public void StartSequence(string name)
    {
        foreach (Sequence sequence in m_sequences)
        {
            if (sequence.m_name == name)
            {
                m_currentSequence = sequence;
                break;
            }
        }
        
        if (m_currentSequence == null)
        {
            Debug.LogError("No sequence associated with name " + name + ". Aborting...");
            return;
        }

        m_isRunningSequence = true;
        m_timer = 0;
        m_targetTime = m_currentSequence.m_segments[0].m_time;
        m_currentSegmentIndex = -1;

        if(m_currentSequence.m_type == SequenceType.Manual)
            StartNextSegment();
    }

    public void StartNextSegment()
    {
        if (m_currentSegmentIndex + 1 < m_currentSequence.m_segments.Count)
        {
            m_currentSequence.m_segments[m_currentSegmentIndex + 1].m_events?.Invoke();

            if (m_currentSegmentIndex + 2 < m_currentSequence.m_segments.Count)
                m_targetTime += m_currentSequence.m_segments[m_currentSegmentIndex + 2].m_time;
            else
                m_isRunningSequence = false;
            
            m_currentSegmentIndex++;
        }
    }

    private bool IsClickedAt()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = m_camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {
                return hit.collider.gameObject == gameObject;
            }   
        }

        return false;
    }
}
