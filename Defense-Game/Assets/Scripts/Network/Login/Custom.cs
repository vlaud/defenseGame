using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Defense
{
    public class Custom : IService
    {
        public LoginType type
        {
            get
            {
                return LoginType.Custom;
            }
        }      

        public string serialized
        {
            get;
            private set;
        }

        string _id;
        public string id
        {
            get
            {
                if (string.IsNullOrEmpty(_id))
                {
                    Debug.Assert(!string.IsNullOrEmpty(serialized));
                    return serialized;
                }

                return _id;
            }
        }

        string _password;
        public string password
        {
            get
            {
                if (string.IsNullOrEmpty(_password))
                {
                    Debug.Assert(!string.IsNullOrEmpty(serialized));
                    return serialized;
                }

                return _password;
            }
        }

        public Custom()
        {
            serialized = System.Guid.NewGuid().ToString();
            _id = serialized;
            _password = serialized;
        }

        public Custom(string id, string password)
        {
            _id = id;
            _password = password;
        }

        public void Login(System.Action<bool> callback)
        {
            callback?.Invoke(true);
        }

        public void Logout()
        {

        }

        public void OnUpdate()
        {

        }
    }
}
