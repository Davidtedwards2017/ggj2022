using gamedev.utilities;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

[ExecuteInEditMode]
public class Grid : MonoBehaviour
{
    public GlobalPropertiesSO GlobalProperties;

    public int RowCount = 5;
    public int ColumnCount = 5;
    public float Scale = 1.0f;

    public Cell CellPrefab;
    [ReadOnly]
    public List<Cell> Cells;

    [ReadOnly]
    public float Width;
    [ReadOnly]
    public float Height;

    // Start is called before the first frame update
    private void OnEnable()
    {
        Rebuild();
    }

    public void Rebuild()
    {
        DestroyGrid();
        BuildGrid();
    }

    private void BuildGrid()
    {
        Width = ColumnCount * Scale;
        Height = RowCount * Scale;

        var count = RowCount * ColumnCount;
        Cells = new List<Cell>();

        for (int c = 0; c < ColumnCount; c++)
        {
            for (int r = 0; r < RowCount; r++)
            {
                var cell = Instantiate(CellPrefab, transform);
                cell.Init(this, r, c);
                Cells.Add(cell);
            }
        }
    }

    public float GetXPosition(int column)
    {
        return -(Width / 2) + (column * Scale) + Scale / 2;
    }

    public float GetYPosition(int row)
    {
        return -(Height / 2) + (row * Scale) + Scale / 2;
    }

    private void DestroyGrid()
    {
        if (Cells == null) return;
        
        foreach (var cell in Cells)
        {
            DestroyImmediate(cell.gameObject);
        }

        Cells.Clear();
    }
}
