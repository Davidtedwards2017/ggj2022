using gamedev.utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterState
{
    Falling,
    Idle,
    Moving,
    SwapingSide,
}

public class CharacterStateController : MonoBehaviour
{
    public CharacterController controller;

    [ReadOnly]
    public ICharacterState Current;

    public CharacterIdleState Idle = new CharacterIdleState();
    public CharacterFallingState Falling = new CharacterFallingState();
    public CharacterMovingState Moving = new CharacterMovingState();
    public CharacterSwapSideState SwapingSide = new CharacterSwapSideState();

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        Idle.Init(this);
        Falling.Init(this);
        Moving.Init(this);
        SwapingSide.Init(this);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Current != null) Current.Update();
    }

    private void LateUpdate()
    {
        if (Current != null) Current.LateUpdate();
    }

    public void GoIdle()
    {
        ChangeState(CharacterState.Idle);
    }

    public void RequestMove(int column)
    {
        Moving.TargetColumn = column;
        ChangeState(CharacterState.Moving);
    }

    public void SwapSide()
    {
        ChangeState(CharacterState.SwapingSide);
    }

    public void ChangeState(CharacterState state)
    {
        if (Current != null && Current.State == state) return;

        if (Current != null) Current.OnExit();

        switch (state)
        {
            case CharacterState.Falling:
                Current = Falling;
                break;
            case CharacterState.Idle:
                Current = Idle;
                break;
            case CharacterState.Moving:
                Current = Moving;
                break;
            case CharacterState.SwapingSide:
                Current = SwapingSide;
                break;
        }

        Current.OnEnter();
    }

}
