using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Defense
{
    public interface ILoginData
    {
        LoginType type { get; }
        string serialized { get; }
    }

    public interface IService : ILoginData
    {        
        void Login(System.Action<bool> callback);
        void Logout();
        void OnUpdate();
    }

    public class None : IService
    {
        public LoginType type
        {
            get
            {
                return (LoginType)(-1);
            }
        }

        public string serialized 
        {
            get
            {
                return string.Empty;
            }
        }

        public void Login(System.Action<bool> callback)
        {
            callback?.Invoke(false);
        }

        public void Logout()
        {

        }

        public void OnUpdate() 
        {
        }
    }
}
