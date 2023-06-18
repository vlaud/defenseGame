using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Defense
{
    public class Apple : IService
    {
        public LoginType type
        {
            get
            {
                return LoginType.Apple;
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
