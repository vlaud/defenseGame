using UnityEngine;
using UnityEngine.EventSystems;

public class DeckSlot : MonoBehaviour, IDropHandler
{
    [field: SerializeField]
    public int unitCount
    {
        get;
        private set;
    }
    public void OnDrop(PointerEventData eventData)
    {
        UnitDeck deck = transform.GetComponentInChildren<UnitDeck>();

        eventData.pointerDrag.transform.SetParent(transform);
        eventData.pointerDrag.transform.localPosition = Vector3.zero;
    }
    public void SetChildren(Transform child)
    {
        child.SetParent(transform);
        child.localPosition = Vector3.zero;
    }
}
