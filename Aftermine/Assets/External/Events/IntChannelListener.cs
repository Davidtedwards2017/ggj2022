
namespace gamedev.utilities.events
{
    public class IntChannelListener : ChannelListener<IntChannel, int>
    {
        public IntUnityEvent Event;

        protected override void RaiseEvent(int value)
        {
            Event?.Invoke(value);
        }
    }
}
