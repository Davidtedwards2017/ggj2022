using UnityEngine;
using UnityEngine.InputSystem;

namespace gamedev.utilities.events
{
    public class InputChannelAccess : MonoBehaviour
    {
        public InputChannel Channel;

        public void Raise(InputAction.CallbackContext context)
        {
            Channel.Set(context);
        }
    }
}
