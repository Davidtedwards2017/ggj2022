using UnityEngine;
using UnityEngine.Events;

namespace gamedev.utilities.events
{
    public class OnAwakeEvent : MonoBehaviour
    {
        public UnityEvent Event;

        private void Awake()
        {
            Event?.Invoke();   
        }
    }
}