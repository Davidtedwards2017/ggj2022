using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndConditionCheck : MonoBehaviour
{
    public Grid grid;

    public int UpperLosingRow => (grid.RowCount - 1);
    public int LowerLostingRow => 0;

    public UnityEvent OnLoseFromUpper;
    public UnityEvent OnLoseFromLower;

    public void Check(Brick brick)
    {
        if (!isActiveAndEnabled) return;

        if (brick == null) return;

        var row = brick.gridObject.Row;
        if (row == UpperLosingRow)
        {
            OnLoseFromUpper?.Invoke();
        } 
        else if (row == LowerLostingRow)
        {
            OnLoseFromLower?.Invoke();
        }
    }

    

}
