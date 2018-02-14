using UnityEngine;
using UnityEngine.UI;

public class HandTouch : MonoBehaviour
{
    public SkinTrigger ST;
    public StateManager SM;
    public ReadText RT;
    public Image TouchUI;

    //// left VRC
    //public VRController VRCLeft;
    //// moving
    //public SkinnedMeshRenderer LeftGribhand;
    //// normal
    //public SkinnedMeshRenderer LeftNormalHand;

    public bool isTouchPlay;
    public bool isDontTouchPlay;
    public bool isEndTouch;

    public bool isTouching;

    void Awake()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }

    void Update()
    {
        if (isDontTouchPlay)
            TouchElse();

        if (TouchUI.fillAmount >= 0.98f)
        {
            isEndTouch = true;
            isTouching = false;
        }
        else
            isEndTouch = false;

        if (isEndTouch)
        {
            ST.OffRubbingHand(); // 쓰다듬는 손 끄기
            //Debug.Log("손꺼손꺼");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("어딘가에 충돌함");  
        if (Equals(other.gameObject.tag, "TouchEvent"))
        {
            isDontTouchPlay = false;
            //Debug.Log("손 닿음");
        }
    }
    void OnTriggerExit(Collider other)
    {
        //Debug.Log("어딘가에 충돌함");
        //Debug.Log("손 안닿음");
        isDontTouchPlay = true;
    }
    void OnTriggerStay(Collider other)
    {
        //Debug.Log("손 닿음");

        if (isTouchPlay)
        {
            if (Equals(other.gameObject.tag, "TouchEvent"))
            {
                isDontTouchPlay = false;
                //Debug.Log("갸갸");

                ST.Part = other.gameObject.GetComponent<PartType>().Type;
                ST.m_CurTimer += Time.deltaTime;
                ST.isTouch = true;

                ST.TouchUI.GetComponent<Image>().fillAmount = ST.m_CurTimer / 5.0f;
                ST.UI_OFFS(false);
                ST.TouchUI.SetActive(true);
                // 추가: 쓰다듬는 손
                ST.OnRubbingHand();
                //// moving
                //if (VRCLeft.gripButtonPressed)
                //    LeftGribhand.enabled = false;
                //// normal
                //else if (!VRCLeft.gripButtonPressed)
                //    LeftNormalHand.enabled = false;
                isTouching = true;
                SM.JamesSurprised();
                SM.JamesSkinshipHead();
                SM.SfxSource2.volume = 0;

                if (Equals(RT.scriptIndex, 67))
                {
                    SM.PlayerFrustrated();
                }

                ST.TouchSphere.GetComponent<MeshRenderer>().enabled = false;
            }
            else// TouchEvent가 아닐 때
            {
                ST.m_CurTimer = 0;
                ST.TouchUI.SetActive(false);
                ST.isTouch = false;

                // 추가: 쓰다듬는 손
                ST.OffRubbingHand();
                //// moving
                //if (VRCLeft.gripButtonPressed)
                //    LeftGribhand.enabled = true;
                //// normal
                //else if (!VRCLeft.gripButtonPressed)
                //    LeftNormalHand.enabled = true;
                isTouching = false;
                SM.JamesSurprised_R();
                SM.JamesSkinshipHead_R();

                ST.TouchSphere.GetComponent<MeshRenderer>().enabled = true;

                if (Equals(RT.scriptIndex, 67) && ST.RubbingHand.activeInHierarchy)
                {
                    SM.PlayerFrustrated_R();
                }
            }
        }// if isTouchPlay
    }// OnTriggerStay

    void TouchElse()
    {
        //Debug.Log("손치워치워");
        ST.m_CurTimer = 0;
        //  TouchUI.SetActive(false);
        ST.isTouch = false;

        // 추가: 쓰다듬는 손
        ST.OffRubbingHand();
        isTouching = false;
        SM.JamesSurprised_R();
        SM.JamesSkinshipHead_R();

        ST.TouchSphere.GetComponent<MeshRenderer>().enabled = true;

        if (Equals(RT.scriptIndex, 67) && ST.RubbingHand.activeInHierarchy)
        {
            SM.PlayerFrustrated_R();
        }
    }

}
