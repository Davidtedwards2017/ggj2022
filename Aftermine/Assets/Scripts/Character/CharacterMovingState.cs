using gamedev.utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CharacterMovingState : ICharacterState
{
    private CharacterController controller;
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

    [ReadOnly]
    public int TargetColumn;

    public CharacterState State => CharacterState.Moving;

    public void Update()
    {

    }

    public void LateUpdate()
    {
    }

    public void OnEnter()
    {
        OnEnterState?.Invoke();

        controller.motor.PerformMove(TargetColumn);    
    }

    public void OnExit()
    {
        OnExitState?.Invoke();
    }
}
