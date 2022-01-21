using UnityEngine;

namespace gamedev.utilities.events
{
    public class TransformChannelListener : ChannelListener<TransformChannel, Transform>
    {
        public TransformChannelUnityEvent Event;

        protected override void RaiseEvent(Transform value)
        {
            Event?.Invoke(value);
        }
    }
}
