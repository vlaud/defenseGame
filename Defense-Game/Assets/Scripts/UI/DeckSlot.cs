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
        if (eventData.pointerDrag.transform.TryGetComponent(out UnitDeck unit))
        {
            Transform tr = unit.itemBeginDragged.transform;
            if (unit.Level <= unitCount)
            {
                tr.SetParent(transform);
                tr.localPosition = Vector3.zero;
                tr.GetComponent<CanvasGroup>().blocksRaycasts = true;
                unitCount -= unit.Level;
                tr.GetComponent<UnitDeck>().curSlot = this;
            }
        }
    }
    public void SetChildren(Transform child)
    {
        child.SetParent(transform);
        child.localPosition = Vector3.zero;
    }
    public void UnDrop(UnitDeck unit)
    {
        unitCount += unit.Level;
    }
}
