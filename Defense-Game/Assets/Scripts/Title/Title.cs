using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Defense
{
    public class Title : MonoBehaviour
    {
        [SerializeField] CustomLogin _customLogin;
        [SerializeField] CustomSignUp _customSignUp;
        [SerializeField] Button _gameStart;
        [SerializeField] TextMeshProUGUI _gameStartText;
        [SerializeField] GameObject _canvas;

        bool _isGameStart = false;

        void Awake()
        {            
            _gameStart.onClick.AddListener(OnGameStart);
        }

        void Start()
        {
            UIManager.Instance.Initialize(_canvas);

            StartCoroutine(Process());
        }

        IEnumerator Process()
        {
            Network.BackendManager.Instance.Initialize();
            yield return new WaitUntil(() => Network.BackendManager.Instance.IsInitialized());

            _customLogin.Initialize(OnCustomLogin, OnShowSignUp);
            _customSignUp.Initialize(OnSignUp);
            _customLogin.gameObject.SetActive(true);            
            yield return new WaitUntil(() => Network.BackendManager.Instance.authorization.status == Network.Authentication.Status.Done);
        }

        void OnCustomLogin(string id, string password)
        {
            Network.Authentication.TryLogin(LoginType.Custom, id, password, (success)=>
            {
                if (success)
                {
                    _customLogin.gameObject.SetActive(false);
                    _gameStart.gameObject.SetActive(true);
                    _gameStartText.gameObject.SetActive(true);
                }                
            });
        }

        void OnShowSignUp()
        {
            _customLogin.gameObject.SetActive(false);            
            _customSignUp.gameObject.SetActive(true);
        }

        void OnSignUp(string id, string password)
        {
            Network.BackendManager.Instance.authorization.SignUp(id, password, (success)=>
            {
                if (success)
                {
                    _customSignUp.gameObject.SetActive(false);
                    _gameStart.gameObject.SetActive(true);
                    _gameStartText.gameObject.SetActive(true);
                }
            });
        }

        void OnGameStart()
        {
            if (_isGameStart)
            {
                return;
            }

            _isGameStart = true;

            UnityEngine.SceneManagement.SceneManager.LoadScene("BackGround");
        }
    }
}
