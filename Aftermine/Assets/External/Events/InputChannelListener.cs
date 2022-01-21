using UnityEngine.InputSystem;

namespace gamedev.utilities.events
{
    public class InputChannelListener : ChannelListener<InputChannel, InputAction.CallbackContext>
    {
        public InputChannelUnityEvent Event;

        protected override void RaiseEvent(InputAction.CallbackContext value)
        {
            Event?.Invoke(value);
        }
    }
}