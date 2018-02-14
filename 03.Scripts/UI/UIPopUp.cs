using UnityEngine;
using UnityEngine.UI;

public class UIPopUp : MonoBehaviour
{
    public Animator TriggerAnim;
    public Image TriggerImage;

    void Awake ()
    {
        //Invoke("OnTriggerImage", 6.0f);
        //Invoke("OffTriggerImage", 6.0f + 12.0f);
    }
	
    void OnTriggerImage()
    {
        TriggerAnim.enabled = true;
        TriggerImage.enabled = true;
    }

    void OffTriggerImage()
    {
        TriggerAnim.enabled = false;
        TriggerImage.enabled = false;
    }
}