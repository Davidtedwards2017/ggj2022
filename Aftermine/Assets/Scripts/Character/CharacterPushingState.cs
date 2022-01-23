using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CharacterPushingState : ICharacterState
{
    private CharacterStateController stateController;

    public UnityEvent OnEnterState;
    public UnityEvent OnExitState;

    public UnityEvent OnUpperPush;
    public UnityEvent OnLowerPush;

    public void Init(CharacterStateController stateController)
    {
        this.stateController = stateController;
    }

    public CharacterState State => CharacterState.Pushing;

    public void Update()
    {

    }

    public void LateUpdate()
    {
    }

    public void OnEnter()
    {
        OnEnterState?.Invoke();

        var fromSide = stateController.controller.side;
        switch (fromSide) 
        {
            case Side.Upper:
                OnUpperPush?.Invoke();
                break;
            case Side.Lower:
                OnLowerPush?.Invoke();
                break;
        }

        stateController.GoIdle();
    }

    public void OnExit()
    {
        OnExitState?.Invoke();
    }
}
