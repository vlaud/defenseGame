using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitDeck : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rect;
    private Transform prevParent;
    private int sortIndex;
    [field: SerializeField]
    public Transform Deck
    {
        get;
        private set;
    }
    public int Level = 1;
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
        GetComponent<Image>().raycastTarget = false;
        transform.SetParent(Deck);
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector3 vec = Camera.main.WorldToScreenPoint(rect.position);
        vec.x += eventData.delta.x;
        vec.y += eventData.delta.y;
        rect.position = Camera.main.ScreenToWorldPoint(vec);
        
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if(transform.parent == Deck) transform.SetParent(prevParent);
        transform.SetSiblingIndex(sortIndex);
        rect.localPosition = Vector3.zero;
        GetComponent<Image>().raycastTarget = true;
    }
    public void OnAction()
    {
        PopUpManager.Inst.PopupOnOff(true);
    }
}
