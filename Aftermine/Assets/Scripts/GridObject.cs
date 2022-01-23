using gamedev.utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject : MonoBehaviour
{
    public Grid Grid;

    [ReadOnly]
    public int Row;
    [ReadOnly]
    public int Column;
    [ReadOnly]
    public Cell Cell;

    public CellEvent OnUpdateCell;

    public void SetGrid(Grid grid)
    {
        Grid = grid;
    }

    public void SnapToGrid()
    {
        Refresh();
        if (Cell != null)
        {
            transform.position = Cell.transform.position;
        }
    }

    public void Refresh()
    {
        var _Cell = GetClosestCell();
        if (Cell != _Cell)
        {
            Cell = _Cell;
            Row = Cell.Row;
            Column = Cell.Column;

            OnUpdateCell?.Invoke(Cell);
        }
    }

    private Cell GetClosestCell()
    {
        foreach (var cell in Grid.Cells)
        {
            var distance = Vector2.Distance(transform.position, cell.transform.position);
            if (distance < Grid.GlobalProperties.GridSnapDistance)
            {
                return cell;
            }
        }

        return null;
        
    }

}
