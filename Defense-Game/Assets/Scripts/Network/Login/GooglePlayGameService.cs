using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Defense
{
    public class GooglePlayGameService : IService
    {
        public LoginType type
        {
            get
            {
                return LoginType.GooglePlay;
            }
        }

        public string serialized
        {
            get;            
        }

        public void Login(System.Action<bool> callback)
        {
        }

        public void Logout()
        {
        }

        public void OnUpdate()
        {

        }
    }
}
