using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Defense
{
    public class PersistentSingleton<T> : Singleton<T> where T : MonoBehaviour
    {
        protected override void Awake()
        {
            base.Awake();

            if (this != _instance)
            {
                return;
            }

            DontDestroyOnLoad(gameObject);
        }
    }
}
