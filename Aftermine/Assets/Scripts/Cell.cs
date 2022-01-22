using gamedev.utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [ReadOnly]
    public int Row;
    [ReadOnly]
    public int Column;

    [ReadOnly]
    public float Scale;
    
    public void Init(Grid grid, int row, int column)
    {
        gameObject.SetActive(true);
        
        Row = row;
        Column = column;
        Scale = grid.Scale;

        var x = grid.GetXPosition(column);
        var y = grid.GetYPosition(row);

        transform.localPosition = new Vector2(x, y);
        gameObject.name = string.Format("Cell ({0}, {1})", Row, Column);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.grey;
        Gizmos.DrawWireCube(transform.position, new Vector3(Scale, Scale, 0.0f));
    }
}
