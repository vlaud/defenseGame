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
            if(unit.Level <= unitCount)
            {
                eventData.pointerDrag.transform.SetParent(transform);
                eventData.pointerDrag.transform.localPosition = Vector3.zero;
                unitCount -= unit.Level;
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
