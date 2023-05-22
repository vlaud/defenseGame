using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpManager : MonoBehaviour
{
    public static PopUpManager Inst = null;
    public GameObject noTouch = null;
    private void Awake()
    {
        Inst = this;
    }
}
