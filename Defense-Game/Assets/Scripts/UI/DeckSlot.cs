using UnityEngine;
using UnityEngine.EventSystems;

public class DeckSlot : MonoBehaviour, IDropHandler
{
    [field: SerializeField]
    public int MaxCount
    {
        get;
        private set;
    }
    [field: SerializeField]
    public int unitCount
    {
        get;
        private set;
    }
    public GameObject text;
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
        SwitchText();
    }
    public void SetChildren(Transform child)
    {
        child.SetParent(transform);
        child.localPosition = Vector3.zero;
    }
    public void UnDrop(UnitDeck unit)
    {
        unitCount += unit.Level;
        SwitchText();
    }
    public void SwitchText()
    {
        bool v = (unitCount != MaxCount) ? false : true;
        text.SetActive(v);
    }
}
