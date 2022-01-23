using gamedev.utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public GlobalPropertiesSO globalProperties;
    public CharacterStateController stateController;
    public CharacterMotor motor;
    public GridObject gridObject;

    [ReadOnly]
    public Side side;

    private void Start()
    {
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
}
