using UnityEngine;

public class VRController : MonoBehaviour
{
    // 컨트롤러
    SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    SteamVR_TrackedObject trackedObj;
    
    // 트리거 버튼
    Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
    // 트리거 버튼 플래그
    public bool triggerButtonPressed = false;

    // 쥐는 버튼
    Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
    // 쥐는 버튼 플래그
    public bool gripButtonPressed = false;

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    void Update()
    {
        if (controller == null)
        {
            Debug.Log("컨트롤러를 찾을 수 없습니다.");
            return;
        }
     
        triggerButtonPressed = controller.GetPress(triggerButton);
        gripButtonPressed = controller.GetPress(gripButton);

        if (controller.GetPressDown(triggerButton))
        {
            Debug.Log("트리거 버튼 눌림");
            Debug.Log("triggerButtonPressed : " + triggerButtonPressed);
        }
        if (controller.GetPressUp(triggerButton))
        {
            Debug.Log("트리거 버튼 손 뗌");
            Debug.Log("triggerButtonPressed : " + triggerButtonPressed);
        }
        if (controller.GetPressDown(gripButton))
        {
            Debug.Log("쥐는 버튼 눌림");
            Debug.Log("gripButton : " + gripButton);
        }
        if (controller.GetPressUp(gripButton))
        {
            Debug.Log("쥐는 버튼 손 뗌");
            Debug.Log("gripButton : " + gripButton);
        }
    }
}