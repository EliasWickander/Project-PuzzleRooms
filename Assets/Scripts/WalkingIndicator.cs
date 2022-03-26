using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingIndicator : MonoBehaviour
{
    public void OnAnimationFinished()
    {
        gameObject.SetActive(false);
    }
}
