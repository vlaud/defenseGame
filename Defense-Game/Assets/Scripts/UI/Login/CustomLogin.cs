using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Defense
{
    public class CustomLogin : MonoBehaviour
    {        
        [SerializeField] TMP_InputField _idInputField;
        [SerializeField] TMP_InputField _passwordInputField;
        [SerializeField] Button _loginButton;
        [SerializeField] Button _signupButton;

        System.Action<string, string> _login;
        System.Action _signUp;

        void Awake()
        {
            _idInputField.onValueChanged.AddListener(OnInputId);
            _passwordInputField.onValueChanged.AddListener(OnInputPassword);
            _loginButton.onClick.AddListener(OnLogin);
            _signupButton.onClick.AddListener(OnSignUp);
        }

        public void Initialize(System.Action<string, string> login, System.Action signUp)
        {
            _login = login;
            _signUp = signUp;
        }

        void OnInputId(string text)
        {            
        }

        void OnInputPassword(string text)
        {
        }

        void OnLogin()
        {
            string id = _idInputField.text;
            string password = _passwordInputField.text;

            if (string.IsNullOrEmpty(id))
            {
                MessagePopup.ShowMessage("아이디가 비어 있습니다");
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                MessagePopup.ShowMessage("비밀번호가 비어 있습니다");
                return;
            }
            
            _login?.Invoke(id, password);            
        }

        void OnSignUp()
        {
            _signUp?.Invoke();
        }
    }
}
