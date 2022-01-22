using UnityEngine;

namespace gamedev.utilities.events
{
    public abstract class ChannelListener<T, V> : MonoBehaviour where T : Channel<V>
    {
        public T Channel;

        public bool RefreshOnEnable = true;



        private void OnEnable()
        {
            Channel.Event.AddListener(Refresh);

            if (RefreshOnEnable)
            {
                Refresh();
            }

        }

        private void OnDisable()
        {
            Channel.Event.RemoveListener(Refresh);
        }

        public void Refresh()
        {
            RaiseEvent(Channel.GetValue());
        }

        protected abstract void RaiseEvent(V value);
    }
}