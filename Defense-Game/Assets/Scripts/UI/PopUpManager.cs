using UnityEngine;

public class PopUpManager : MonoBehaviour
{
    public static PopUpManager Inst = null;
    public GameObject noTouch = null;
    public GameObject UnitPopup = null;
    private void Awake()
    {
        Inst = this;
    }
    private void Start()
    {
        noTouch.SetActive(false);
        UnitPopup.SetActive(false);
    }
    public void OpenPopup()
    {
        noTouch.SetActive(true);
        UnitPopup.SetActive(true);
    }
}
