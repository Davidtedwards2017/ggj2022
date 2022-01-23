using gamedev.utilities;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [ReadOnly]
    public Side Side;

    [ReadOnly]
    public int Row;
    [ReadOnly]
    public int Column;

    public void Init(SpawnerGroup group, int row, int column)
    {
        gameObject.SetActive(true);

        Row = row;
        Column = column;
        Side = group.Side;

        var x = group.Grid.GetXPosition(Column);
        var y = group.Grid.GetYPosition(Row);

        transform.localPosition = new Vector2(x, y);

        gameObject.name = string.Format("Spawner ({0},{1})", row, column);
    }

    public void Spawn(BrickGroup prefab, BrickType type, Transform parent)
    {
        var brickGroup = Instantiate(prefab, transform.position, Quaternion.identity, parent);
        brickGroup.Init(Side, type);
    }

}
