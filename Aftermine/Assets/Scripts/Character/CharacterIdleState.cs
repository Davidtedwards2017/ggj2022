using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CharacterIdleState : ICharacterState
{
    private CharacterStateController stateController;

    public UnityEvent OnEnterState;
    public UnityEvent OnExitState;

    public void Init(CharacterStateController stateController)
    {
        this.stateController = stateController;
    }

    public CharacterState State => CharacterState.Idle;

    public void Update()
    {
        if (!stateController.controller.groundCheck.Grounded)
        {
            stateController.StartFalling();
        }
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
