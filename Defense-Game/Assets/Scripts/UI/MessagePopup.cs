using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Defense
{
    public class MessagePopup : MonoBehaviour
    {
        const string MESSAGE_POPUP_PATH_NAME = "Prefabs/UI/MessagePopup";

        [SerializeField] TextMeshProUGUI _message;
        [SerializeField] Button _ok;
        [SerializeField] Button _cancel;

        static MessagePopup _messagePopup = null;

        System.Action<bool> _callback;

        void Awake()
        {
            if (_ok != null)
            {
                _ok.onClick.AddListener(OnOk);
            }

            if (_cancel != null)
            {
                _cancel.onClick.AddListener(OnCancel);
            }
        }

        public static void ShowMessage(string message, System.Action<bool> callback = null)
        {
            if (_messagePopup == null)
            {
                _messagePopup = GameObject.FindObjectOfType<MessagePopup>();
                if (_messagePopup == null)
                {
                    GameObject loadObject = Resources.Load<GameObject>(MESSAGE_POPUP_PATH_NAME);
                    if (loadObject != null)
                    {
                        GameObject messagePopupObject = GameObject.Instantiate(loadObject);                        
                        UIManager.Instance.AttachCanvas(messagePopupObject, 0);

                        _messagePopup = messagePopupObject.GetComponent<MessagePopup>();
                    }                    
                }                
            }

            if (_messagePopup != null)
            {
                _messagePopup.Show(message, callback);
            }
        }       

        public void Show(string message, System.Action<bool> callback = null)
        {
            _message.text = message;
            _callback = callback;

            gameObject.SetActive(true);
        }

        void OnOk()
        {
            Hide();

            _callback?.Invoke(true);
        }

        void OnCancel()
        {
            Hide();

            _callback?.Invoke(false);
        }

        void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
