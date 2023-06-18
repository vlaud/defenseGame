using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Defense.Network
{
    public class Authentication
    {
        public enum Status
        {
            NotAuthorized,
            InProgress,
            Done,
        }

        static readonly IService _notAuthorized = new None();
        IService _active = _notAuthorized;
        IService _inProgress = null;
        

        public Status status
        {
            get
            {
                if (_inProgress != null)
                {
                    return Status.InProgress;
                }

                if (_active == _notAuthorized)
                {
                    return Status.NotAuthorized;
                }

                return Status.Done;
            }
        }

        System.Action<bool> _callback;

        public static void TryLogin(LoginType type, string id = "", string password = "", System.Action<bool> callback = null)
        {
            BackendManager.Instance.authorization.Login(type, id, password, callback);
        }

        public static void TryAutoLogin(System.Action<bool> callback)
        {            
            BackendManager.Instance.LoginWithTheBackendToken((success)=>
            {
                callback?.Invoke(success);
            });
        }

        public void Login(LoginType type, string id = "", string password = "", System.Action<bool> callback = null)
        {
            _callback = callback;

            _inProgress = CreateService(type, id, password);

            if (_inProgress != null)
            {
                _inProgress.Login(OnLogin);
            }
            else
            {
                callback?.Invoke(false);
            }
        }

        public void SignUp(string id, string password, System.Action<bool> callback)
        {
            BackendManager.Instance.SignUp(id, password, callback);
        }

        public void OnUpdate()
        {
            if (_inProgress != null)
            {
                _inProgress.OnUpdate();
            }
        }

        IService CreateService(LoginType type, string id = "", string password = "")
        {
            switch (type)
            {
                case LoginType.GooglePlay:
                    return new GooglePlayGameService();
                case LoginType.Apple:
                    return new Apple();
                case LoginType.Custom:
                    return new Custom(id, password);
                default:
                    Debug.LogFormat("{0} type is not create service", type.ToString());
                    return null;
            }
        }

        void OnLogin(bool success)
        {
            if (!success)
            {
                _inProgress = null;
                _callback?.Invoke(false);
                return;
            }                       
            
            LoginType type = _inProgress.type;

            switch(type)
            {
                case LoginType.Guest:
                    break;
                case LoginType.Apple:
                    break;
                case LoginType.GooglePlay:
                    break;
                case LoginType.Custom:
                    {
                        Custom custom = _inProgress as Custom;
                        BackendManager.Instance.CustomLogin(custom.id, custom.password, (success)=>
                        {
                            OnLoginComplete(success);
                        });
                    }
                    break;
            }
        }

        void OnLoginComplete(bool success)
        {
            if (success)
            {
                _active.Logout();
                _active = _inProgress;
            }
            else
            {
                _inProgress.Logout();
            }

            _inProgress = null;

            _callback?.Invoke(success);
        }
    }
}
