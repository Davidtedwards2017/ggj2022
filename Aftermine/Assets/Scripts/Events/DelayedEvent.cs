using gamedev.utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DelayedEvent : MonoBehaviour
{
    public UnityEvent Event;
    public float Duration = 0.0f;

    [ReadOnly]
    public bool Executing;

    public void Raise()
    {
        if (!Executing)
        {
            StartCoroutine(Delay());
        }
    }

    public void Abort()
    {
        StopAllCoroutines();
        Executing = false;
    }

    private IEnumerator Delay()
    {
        Executing = true;
        yield return new WaitForSeconds(Duration);
        Executing = false;
        Event?.Invoke();
    }
}
