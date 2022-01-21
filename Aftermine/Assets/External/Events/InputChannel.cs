using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace gamedev.utilities.events
{
    [CreateAssetMenu(menuName = "Utilities/Channels/Input channel")]
    public class InputChannel : Channel<InputAction.CallbackContext> {}

    [Serializable]
    public class InputChannelUnityEvent : UnityEvent<InputAction.CallbackContext> { }
}