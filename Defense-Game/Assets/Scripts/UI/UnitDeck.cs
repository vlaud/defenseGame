using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;

public class UnitDeck : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, Observer
{
    public Subject mySubject;
    public float timeLimit = 0.25f;
    public PointerEventData.InputButton button;

    [System.Serializable]
    public class OnSingleClick : UnityEvent { };
    public OnSingleClick onSingleClick;

    [System.Serializable]
    public class OnDoubleClick : UnityEvent { };
    public OnDoubleClick onDoubleClick;

    private int clickCount;
    private float firstClickTime;
    private float currentTime;

    private UnitDeck()
    {
        clickCount = 0;
    }
    public void Notified()
    {
        Debug.Log(this + ", " + mySubject);
    }
    public void SetMySubject(Subject s)
    {
        mySubject = s;
    }
    public void onClick(BaseEventData data)
    {
        PointerEventData pointerData = data as PointerEventData;

        if (this.button != pointerData.button)
        {
            return;
        }

        this.clickCount++;

        if (this.clickCount == 1)
        {
            firstClickTime = pointerData.clickTime;
            currentTime = firstClickTime;
            StartCoroutine(ClickRoutine());
        }
    }

    private IEnumerator ClickRoutine()
    {
        while (clickCount != 0)
        {
            yield return new WaitForEndOfFrame();

            currentTime += Time.deltaTime;

            if (currentTime >= firstClickTime + timeLimit)
            {
                if (clickCount == 1)
                {
                    onSingleClick.Invoke();
                }
                else
                {
                    onDoubleClick.Invoke();
                }
                clickCount = 0;
            }
        }
    }
    private RectTransform rect;
    private Transform prevParent;
    private int sortIndex;
    [field: SerializeField]
    public bool isDuplicate
    {
        get;
        private set;
    }
    public GameObject itemBeginDragged;

    [field: SerializeField]
    public Transform Deck
    {
        get;
        private set;
    }
    public int Level = 1;
    public DeckSlot curSlot;
    private void Start()
    {
        rect = GetComponent<RectTransform>();
        prevParent = transform.parent;
        sortIndex = transform.GetSiblingIndex();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isDuplicate)
        {
            transform.SetParent(Deck);
            transform.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
        else
        {
            GameObject obj = Instantiate(gameObject, transform.parent);
            itemBeginDragged = obj;
            itemBeginDragged.GetComponent<UnitDeck>().SwitchDuplicate(true);
            mySubject.AddObserver(itemBeginDragged.GetComponent<Observer>());
            RectTransform tmpRT = transform.GetComponent<RectTransform>();

            RectTransform rt = itemBeginDragged.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(tmpRT.sizeDelta.x, tmpRT.sizeDelta.y);
            rt.localScale = tmpRT.localScale;
            GetComponent<CanvasGroup>().blocksRaycasts = false;
            itemBeginDragged.transform.SetParent(Deck);
            itemBeginDragged.GetComponent<CanvasGroup>().blocksRaycasts = false;
            itemBeginDragged.transform.position = rect.position;
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (isDuplicate) rect.position = DragMovement(rect.position, eventData);
        else itemBeginDragged.transform.position = DragMovement(itemBeginDragged.transform.position, eventData);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        // 복제본
        if (isDuplicate)
        {
            if (transform.parent.TryGetComponent(out DeckSlot slot))
            {
                curSlot = slot;
            }
            if (transform.parent == Deck)
            {
                transform.SetParent(prevParent);
                transform.SetSiblingIndex(sortIndex);
                curSlot?.UnDrop(this);
                mySubject.RemoveObserver(this);
                Destroy(gameObject);
            }
        }
        else // 원본
        {
            Transform tr = itemBeginDragged.transform;
            if (tr.parent.TryGetComponent(out DeckSlot slot))
            {
                curSlot = slot;
            }
            if (tr.parent == Deck)
            {
                Debug.Log("duplicate delete");
                mySubject.RemoveObserver(itemBeginDragged.GetComponent<Observer>());
                Destroy(itemBeginDragged);
            }
            GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
    }
    public void OnAction()
    {
        PopUpManager.Inst.PopupOnOff(true);
    }
    public void SwitchDuplicate(bool v)
    {
        isDuplicate = v;
    }
    public Vector3 DragMovement(Vector3 pos, PointerEventData eventData)
    {
        Vector3 vec = Camera.main.WorldToScreenPoint(pos);
        vec.x += eventData.delta.x;
        vec.y += eventData.delta.y;
        return Camera.main.ScreenToWorldPoint(vec);
    }
}
