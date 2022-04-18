using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    [SerializeField] 
    private float m_timeToOpen = 1;
    
    [SerializeField] 
    private float m_targetYaw = -45;

    public Transform m_doorFrame = null;
    
    public override void OnInteractionSuccess()
    {
        base.OnInteractionSuccess();

        StartCoroutine(OpenDoor());
    }

    private IEnumerator OpenDoor()
    {
        float timer = 0;
        float startYaw = m_doorFrame.eulerAngles.y;
        
        while (timer < m_timeToOpen)
        {
            yield return new WaitForEndOfFrame();

            float currentYaw = CustomLerp.Lerp(startYaw, m_targetYaw, timer / m_timeToOpen, LerpMode.EaseInQuart);
            
            m_doorFrame.rotation = Quaternion.Euler(m_doorFrame.eulerAngles.x, currentYaw, m_doorFrame.eulerAngles.z);

            timer += Time.deltaTime;
        }
    }
}
