using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BrickJiggle : MonoBehaviour
{
    public UnityEvent OnJiggleStart;
    public UnityEvent OnJiggleEnd;

    public void StartJiggle()
    {
        OnJiggleStart?.Invoke();
    }

    public void StopJiggle()
    {
        OnJiggleEnd?.Invoke();
    }
}
