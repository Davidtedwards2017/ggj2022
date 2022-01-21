using UnityEngine;

namespace gamedev.utilities.events
{
    public class TransformChannelAccess : MonoBehaviour
    {
        public TransformChannel Channel;

        public void Raise(Transform tranform)
        {
            Channel.Set(tranform);
        }
    }
}