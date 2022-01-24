using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Relay : MonoBehaviour
{
    public UnityEvent Event;

    public void Raise()
    {
        Event?.Invoke();
    }
}
