using gamedev.utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnSelector : MonoBehaviour
{
    public Grid Grid;

    public IntEvent OnColumnSelected;

    [ReadOnly]
    public int Column;
    
    public void Init(int column)
    {
        this.Column = column;
        UpdateColumnSelection();
    }

    public void RequestMoveLeft()
    {
        Column--;
        if (Column < 0)
        {
            Column = 0;
        }
        else
        {
            UpdateColumnSelection();
        }
    }

    public void RequestMoveRight()
    {
        Column++;
        if (Column > Grid.ColumnCount - 1)
        {
            Column = Grid.ColumnCount - 1;
        }
        else
        {
            UpdateColumnSelection();
        }
    }

    private void UpdateColumnSelection()
    {
        OnColumnSelected?.Invoke(Column);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        var x = Grid.GetXPosition(Column);
        var y = Grid.transform.position.y;
        var center = new Vector2(x, y);

        var width = Grid.Scale;
        var height = Grid.Scale * Grid.RowCount;
        var size = new Vector2(width, height);

        Gizmos.DrawWireCube(center, size);
    }
}
