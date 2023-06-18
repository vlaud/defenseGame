using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Defense
{
    public class CustomSignUp : MonoBehaviour
    {
        [SerializeField] TMP_InputField _idInputField;
        [SerializeField] TMP_InputField _passwordInputField;
        [SerializeField] TMP_InputField _passwordConfirmInputField;
        [SerializeField] Button _signUpButton;

        System.Action<string, string> _signUp;

        void Awake()
        {
            _signUpButton.onClick.AddListener(OnSignUp);
        }

        public void Initialize(System.Action<string, string> signUp)
        {
            _signUp = signUp;
        }

        void OnSignUp()
        {
            string id = _idInputField.text;
            string password = _passwordInputField.text;
            string passwordConfirm = _passwordConfirmInputField.text;

            if (string.IsNullOrEmpty(id))
            {
                MessagePopup.ShowMessage("���̵� ��� �ֽ��ϴ�");
                return;
            }

            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(passwordConfirm))
            {
                MessagePopup.ShowMessage("��й�ȣ�� ��� �ֽ��ϴ�");
                return;
            }

            if (!password.Equals(passwordConfirm))
            {
                MessagePopup.ShowMessage("��й�ȣ�� ��ġ���� �ֽ��ϴ�");
                return;
            }

            _signUp?.Invoke(id, password);
        }
    }
}
