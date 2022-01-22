using UnityEngine;

public class GridChannelAccess : MonoBehaviour
{
    public GridChannel Channel;

    public void Raise(Grid grid)
    {
        Channel.Set(grid);
    }
}
