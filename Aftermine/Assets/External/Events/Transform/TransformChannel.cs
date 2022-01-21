using System;
using UnityEngine;
using UnityEngine.Events;

namespace gamedev.utilities.events
{
    [CreateAssetMenu(menuName = "Utilities/Channels/Transform")]
    public class TransformChannel : Channel<Transform> {}

    [Serializable]
    public class TransformChannelUnityEvent : UnityEvent<Transform> { }

}
