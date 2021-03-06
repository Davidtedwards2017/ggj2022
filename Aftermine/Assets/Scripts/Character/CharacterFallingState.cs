using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CharacterFallingState : ICharacterState
{
    private CharacterStateController stateController;

    public CharacterFalling Falling;

    public UnityEvent OnEnterState;
    public UnityEvent OnExitState;

    public void Init(CharacterStateController stateController)
    {
        this.stateController = stateController;
    }

    public CharacterState State => CharacterState.Falling;

    public void Update()
    {

    }

    public void LateUpdate()
    {
        Falling.UpdateMovement();
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
