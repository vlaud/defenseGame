using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitDeck : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 2)
        {
            OnAction();
        }

    }
    public void OnAction()
    {

    }
}
