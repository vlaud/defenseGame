using UnityEngine;
using UnityEngine.UI;

public class ButtonAlphaHit : MonoBehaviour
{
    public float AlphaThreshold = 0.1f;
    public Image myImage;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Image>().alphaHitTestMinimumThreshold = AlphaThreshold;
    }
}
