using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CharacterSquishState : ICharacterState
{
    private CharacterStateController stateController;

    public UnityEvent OnEnterState;
    public UnityEvent OnExitState;

    public void Init(CharacterStateController stateController)
    {
        this.stateController = stateController;
    }

    public CharacterState State => CharacterState.Squish;

    public void Update()
    {

    }

    public void LateUpdate()
    {
    }

    public void OnEnter()
    {
        OnEnterState?.Invoke();
    }

    public void OnExit()
    {
        OnExitState?.Invoke();
    }
}
