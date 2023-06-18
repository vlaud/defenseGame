using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Defense
{
    public class UIManager : Singleton<UIManager>
    {
        const string CANVAS_PATH_NAME = "UI/Canvas";

        [SerializeField] Text _InputText;

        Camera _camera = null;
        public Camera cam
        {
            get
            {
                return _camera;
            }
        }

        public Canvas defaultCanvas
        {
            get
            {
                if (_canvases.ContainsKey(_defaultSortingOrder))
                {
                    return _canvases[_defaultSortingOrder];
                }
                return null;
            }
        }

        Dictionary<int/*sortingOrder*/, Canvas> _canvases = new Dictionary<int, Canvas>();

        int _defaultSortingOrder = 0;

        public void Initialize()
        {
            GameObject canvasObject = Resources.Load<GameObject>(CANVAS_PATH_NAME);
            if (canvasObject != null)
            {
                GameObject canvas = Instantiate(canvasObject);
                if (canvas != null)
                {
                    Initialize(canvas);
                }                
                else
                {
                    Debug.LogError("canas object not instantiate");
                }
            }    
            else
            {
                Debug.LogErrorFormat("{0} not loaded", CANVAS_PATH_NAME);
            }
        }
        
        public void Initialize(GameObject canvasObject)
        {
            if (canvasObject == null)
            {
                return;
            }
            
            Canvas[] canvases = canvasObject.GetComponentsInChildren<Canvas>() ;
            if(canvases != null && canvases.Length > 0)
            {
                _defaultSortingOrder = int.MaxValue;
                foreach (Canvas canvas in canvases)
                {
                    int sortingOrder = canvas.sortingOrder;
                    if (_defaultSortingOrder > sortingOrder)
                    {
                        _defaultSortingOrder = sortingOrder;
                    }

                    _canvases.Add(sortingOrder, canvas);
                }

                _camera = _canvases[_defaultSortingOrder].worldCamera;
            }        

            // 터치 민감도 설정
            UnityEngine.EventSystems.EventSystem eventSystem = UnityEngine.EventSystems.EventSystem.current;
            if (eventSystem != null)
            {
                float defaultValue = 0.5f;
                eventSystem.pixelDragThreshold = (int)Mathf.Max(defaultValue, (int)(defaultValue * Screen.dpi / 2.54f));
            }
        }

        public void AttachCanvas(GameObject ui, int siblingIndex)
        {
            Canvas canvas = GetCanvas(siblingIndex);
            if (canvas == null)
            {
                Debug.LogErrorFormat("{0} index is not attach canvas", siblingIndex);
                return;
            }

            Transform parent = canvas.transform.transform;

            ui.SetLayerInChildren(canvas.gameObject.layer);
            ui.transform.SetParent(parent, false);
            ui.transform.SetAsLastSibling();
        }
        
        public void UpdateText(string text)
        {
            _InputText.text = text;
        }

        Canvas GetCanvas(int index)
        {
            if (_canvases.ContainsKey(index))
            {
                return _canvases[index];
            }

            return null;
        }
    }
}
