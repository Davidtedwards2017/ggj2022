using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace gamedev.utilities.events
{
    public abstract class Channel<T> : ScriptableObject 
    {
        [HideInInspector] public UnityEvent Event;

        [ReadOnly]
        [SerializeField]
        protected T _value;

        public T GetValue()
        {
            return _value;
        }

        public void Set(T value)
        {
            _value = value;
            Event?.Invoke();
        }
    }
}
