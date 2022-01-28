using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnyInputListener : MonoBehaviour
{
    public UnityEvent OnAnyInput;

    public void OnAny()
    {
         OnAnyInput?.Invoke();
    }
}
