using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CharacterIdleState : ICharacterState
{
    public CharacterController controller;
    private CharacterStateController stateController;

    public UnityEvent OnEnterState;
    public UnityEvent OnExitState;

    public void Init(
        CharacterController controller,
        CharacterStateController stateController)
    {
        this.controller = controller;
        this.stateController = stateController;
    }

    public CharacterState State => CharacterState.Idle;

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
