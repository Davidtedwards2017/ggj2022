using gamedev.utilities.events;

public class GridChannelListener : ChannelListener<GridChannel, Grid>
{
    public GridChannelEvent Event;

    protected override void RaiseEvent(Grid value)
    {
        Event?.Invoke(value);
    }
}
