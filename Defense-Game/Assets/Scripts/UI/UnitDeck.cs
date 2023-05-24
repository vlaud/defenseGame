using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitDeck : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
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
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 1)
        {
            OnAction();
        }
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
