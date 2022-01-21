using UnityEngine;

namespace gamedev.utilities
{
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        private static T instance { get; set; }
        public bool IsPresistant = true;

        public static T Instance
        {
            get
            {
                return instance;
            }
        }

        protected virtual void Awake()
        {
            if (!instance)
            {
                instance = this as T;
            }
            else
            {
                Destroy(gameObject);
            }

            if (IsPresistant)
            {
                DontDestroyOnLoad(transform.root.gameObject);
            }
        }
    }
}