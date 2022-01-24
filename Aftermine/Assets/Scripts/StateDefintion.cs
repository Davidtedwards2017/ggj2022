using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class StateDefintion : MonoBehaviour
{
    public UnityEvent OnEnter;
    public UnityEvent OnExit;

    public State State;

    public virtual void Enter()
    {
        OnEnter?.Invoke();
    }

    public virtual void Exit()
    {
        OnExit?.Invoke();
    }
}
