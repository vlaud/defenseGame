using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;

namespace Defense.Network
{
    public class BackendManager : PersistentSingleton<BackendManager>
    {
        const string SERVER_SETTINGS_PATH_NAME = "ScriptableObject/BackendServerSettings";
        const int TIME_OUT_SECOND = 30; // 30�� ���Ϸ� ���� �� ����

        Authentication _authorization;
        public Authentication authorization
        {
            get
            {
                if (_authorization == null)
                {
                    _authorization = new Authentication();
                }
                return _authorization;
            }
        }

        BackendServerSettings _severSettings;

        bool _initialized = false;

        public void Initialize()
        {
            if (_initialized)
            {
                return;
            }

            BackendCustomSetting setting = null;
            BackendServerInfo serverInfo = GetServerInfo(ServerType.Development);
            if (serverInfo != null)
            {
                setting = new BackendCustomSetting();
                setting.packageName = Application.identifier;
                setting.clientAppID = serverInfo.clientAppId;
                setting.signatureKey = serverInfo.signatureKey;
                setting.rateCountActive = true;
                setting.timeOutSec = TIME_OUT_SECOND;
                setting.useAsyncPoll = true;
            }

            if (setting != null)
            {
                Backend.InitializeAsync(setting, (callback)=>
                {
                    if (callback.IsSuccess())
                    {                        
                        Debug.Log("backend initialize success!");

                        CreateSendQueueManager();
                        SetErrorHandler();
                    }
                });
            }
            else
            {
                Backend.InitializeAsync(true, (callback) =>
                {
                    if (callback.IsSuccess())
                    {
                        Debug.Log("backend initialize success!");

                        CreateSendQueueManager();
                        SetErrorHandler();
                    }
                });
            }
        }

        public bool IsInitialized()
        {
            return Backend.IsInitialized;
        }        

        public void GetSeverTime(System.Action<bool, System.DateTime> callback)
        {
            Backend.Utils.GetServerTime((backendCallback)=>
            {
                bool success = backendCallback.IsSuccess();
                System.DateTime parsedDate = default(System.DateTime);
                if (success)
                {
                    string time = backendCallback.GetReturnValuetoJSON()["utcTime"].ToString();
                    parsedDate = System.DateTime.Parse(time);                    
                }

                callback?.Invoke(success, parsedDate);
            });
        }

        public void LoginWithTheBackendToken(System.Action<bool> callback)
        {
            SendQueue.Enqueue(Backend.BMember.LoginWithTheBackendToken, (backendCallback)=>
            {
                callback?.Invoke(backendCallback.IsSuccess());
            });
        }


        public void CustomLogin(string id, string password, System.Action<bool> callback)
        {
            SendQueue.Enqueue(Backend.BMember.CustomLogin, id, password, backendCallback => 
            {
                bool success = backendCallback.IsSuccess();
                if (!success)
                {
                    string message = backendCallback.GetMessage();
                    MessagePopup.ShowMessage(message);
                }

                callback?.Invoke(success);
            });
        }

        public void SignUp(string id, string password, System.Action<bool> callback)
        {
            SendQueue.Enqueue(Backend.BMember.CustomSignUp, id, password, backendCallback => 
            {
                bool success = backendCallback.IsSuccess();
                if (!success)
                {
                    string message = backendCallback.GetMessage();
                    MessagePopup.ShowMessage(message);
                }

                callback?.Invoke(success);
            });
        }

        void Update()
        {
            if (IsInitialized())
            {
                Backend.AsyncPoll();
                Backend.ErrorHandler.Poll();
            }

            if (authorization != null)
            {
                authorization.OnUpdate();
            }
        }

        BackendServerInfo GetServerInfo(ServerType type)
        {
            if (_severSettings == null)
            {
                _severSettings = ResourcesLoader.Load<BackendServerSettings>(SERVER_SETTINGS_PATH_NAME);
                if (_severSettings == null)
                {
                    Debug.LogErrorFormat("{0} not loaded", SERVER_SETTINGS_PATH_NAME);
                }
            }

            return _severSettings != null ? _severSettings.GetServerInfo(type) : null;
        }

        void CreateSendQueueManager()
        {
            var obj = new GameObject();
            obj.name = "SendQueueMgr";
            obj.transform.SetParent(this.transform);
            obj.AddComponent<SendQueueMgr>();
        }

        void SetErrorHandler()
        {
            Backend.ErrorHandler.InitializePoll(true);

            Backend.ErrorHandler.OnMaintenanceError = () => 
            {
                // ���� ���� �߻�
                // ���� ������, ���� ���� �������Դϴ�. Ÿ��Ʋ�� ���ư��ϴ�
            };

            Backend.ErrorHandler.OnTooManyRequestError = () =>
            {
                // 403 �����߻���
                // ���������� �ൿ ����, ���������� �ൿ�� �����Ǿ����ϴ�. Ÿ��Ʋ�� ���ư��ϴ�.
            };

            Backend.ErrorHandler.OnOtherDeviceLoginDetectedError = () =>
            {
                // �ٸ� ��� ���� ����
                // �ٸ� ��⿡�� �α����� �����Ǿ����ϴ�. Ÿ��Ʋ�� ���ư��ϴ�.
            };
        }        
    }
}
