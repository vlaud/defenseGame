using UnityEngine;
using UnityEngine.EventSystems;

public class DeckSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private int MaxCount;
    [SerializeField] private int unitcount;
    public int unitCount
    {
        get => unitcount;
        private set
        {
            unitcount = Mathf.Clamp(value, 0, MaxCount);
        }
    }
    public GameObject text;
    private void Start()
    {
        MaxCount = transform.GetSiblingIndex() + 1;
        unitCount = MaxCount;
        text.GetComponent<TMPro.TMP_Text>().text = MaxCount.ToString() + "Æ¼¾î ½½·Ô";
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.transform.TryGetComponent(out UnitDeck unit))
        {
            if (unit.isDuplicate) return;
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
