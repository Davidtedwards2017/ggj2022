using gamedev.utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SpawnerGroup : MonoBehaviour
{
    public Side Side;

    public Grid Grid;
    public Spawner SpawnerPrefab;
    [ReadOnly]
    public List<Spawner> Spawners;
    public GlobalPropertiesSO GlobalProperties;
    public Vector2 Direction => GlobalProperties.GetBrickMovementDirection(Side);

    public void RebuildSpawners()
    {
        DeleteSpawners();
        CreateSpawners();
    }

    private int GetRow()
    {
        return Side == Side.Upper ? Grid.RowCount - 1 : 0;
    }

    public void CreateSpawners()
    {
        var row = GetRow();
        var columnCount = Grid.ColumnCount;
        for (int c = 0; c < columnCount; c++)
        {
            var spawner = Instantiate(SpawnerPrefab, transform);
            spawner.Init(this, row, c);
            Spawners.Add(spawner);
        }
    }

    public void DeleteSpawners()
    {
        if (Spawners == null) return;
        
        foreach (var spawner in Spawners)
        {
            DestroyImmediate(spawner.gameObject);
        }

        Spawners.Clear();
    }
}
