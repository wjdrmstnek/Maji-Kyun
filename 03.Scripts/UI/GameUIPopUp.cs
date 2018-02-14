using UnityEngine;
using UnityEngine.UI;

public class GameUIPopUp : MonoBehaviour
{
    public Animator TriggerAnim;
    public Image TriggerImage;

    public Animator GrabAnim;
    public Image GrabImage;

    void Awake ()
    {
        //Invoke("OnGribImage", 4.0f + 6.0f);
        //Invoke("OffGribImage", 6.0f + 16.0f);
    }
	
    void OnGribImage()
    {
        GrabAnim.enabled = true;
        GrabImage.enabled = true;
    }

    void OffGribImage()
    {
        GrabAnim.enabled = false;
        GrabImage.enabled = false;
    }
}