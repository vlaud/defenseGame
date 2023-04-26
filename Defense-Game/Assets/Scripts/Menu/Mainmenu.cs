using UnityEngine;

public class Mainmenu : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void openSinglePlay()
    {
        anim.Play("Mainmenu_off");
    }
    public void openWorld()
    {
        anim.Play("To_World");
    }
    public void backSinglePlay()
    {
        anim.Play("Back_Single");
    }
    public void backMainmenu()
    {
        anim.Play("Mainmenu_on");
    }
}
