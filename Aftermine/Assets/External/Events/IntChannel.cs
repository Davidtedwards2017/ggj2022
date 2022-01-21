using System;
using UnityEngine;
using UnityEngine.Events;

namespace gamedev.utilities.events
{
    [CreateAssetMenu(menuName = "Utilities/Channels/int")]
    public class IntChannel : Channel<int> { }

    [Serializable]
    public class IntUnityEvent : UnityEvent<int> { }
}