using gamedev.utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CharacterSwapSideState : ICharacterState
{
    private CharacterStateController stateController;

    public UnityEvent OnEnterState;
    public UnityEvent OnExitState;

    public void Init(CharacterStateController stateController)
    {
        this.stateController = stateController;
    }

    public CharacterState State => CharacterState.SwapingSide;

    public void Update()
    {

    }

    public void LateUpdate()
    {
    }

    public void OnEnter()
    {
        OnEnterState?.Invoke();
        stateController.controller.SideSwap.SwapSide();
        stateController.RequestMove(stateController.controller.Column);
    }

    public void OnExit()
    {
        OnExitState?.Invoke();
    }
}
