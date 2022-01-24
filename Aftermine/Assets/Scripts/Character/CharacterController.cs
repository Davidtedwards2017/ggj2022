using gamedev.utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController : MonoBehaviour
{
    public GlobalPropertiesSO globalProperties;
    public CharacterStateController stateController;
    public CharacterMotor motor;
    public SideSwap SideSwap;
    public GridObject gridObject;
    public int Column => gridObject.Column;

    [ReadOnly]
    public Side side;

    public UnityEvent OnInit;
    public SideEvent OnSideSet;

    public void Init()
    {
        OnInit?.Invoke();

        SetSide(side);
        stateController.GoIdle();
    }

    public void UpdateCell(Cell cell)
    {
        side = cell.Side;
    }

    public void RequestMove(int column)
    {
        stateController.RequestMove(column);
    }

    public void RequestPerformPush()
    {
        stateController.PeformPush();
    }

    public void ChangeSide(Side side)
    {
        if (this.side == side) return;
        RequestSwapSide();
    }

    public void RequestSwapSide()
    {
        stateController.SwapSide();
    }

    public void RequestSquish()
    {
        stateController.GetSquished();
    }

    public void SetSide(Side side)
    {
        this.side = side;
        OnSideSet?.Invoke(side);
    }
}
