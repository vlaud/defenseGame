using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Defense
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static bool Verify()
        {
            return (_instance != null);
        }

        public static T Instance
        {
            get
            {
                if (_applicationQuitting)
                {
                    return null;
                }

                if (_instance == null)
                {
                    _instance = GameObject.FindObjectOfType(typeof(T)) as T;
                    if (_instance == null)
                    {
                        var obj = new GameObject(typeof(T).ToString());
                        _instance = obj.AddComponent<T>();
                    }

                    return _instance;
                }

                return _instance;
            }
        }

        protected static T _instance = null;

        static bool _applicationQuitting = false;

        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        protected virtual void OnDestroy()
        {
            Destroyed(this as T);
        }

        void OnApplicationQuit()
        {
            _applicationQuitting = true;

            Destroyed(this as T);
        }

        void Destroyed(T instance)
        {
            if (_instance == instance)
            {
                _instance = null;
            }
        }
    }
}
