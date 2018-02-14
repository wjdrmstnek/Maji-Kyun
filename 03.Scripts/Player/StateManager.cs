using UnityEngine;

public class StateManager : MonoBehaviour
{
    public Animator PlayerAnim;
    public Animator SitJamesAnim;
    public Animator DoorAnim;
    public Animator HandAnim;
    public AudioSource VoiceSource;
    public AudioSource SfxSource;
    public AudioSource SfxSource2;
    public AudioClip[] VoiceDialogs;

    void Awake()
    {
        if (Equals(PlayerAnim, null))
            PlayerAnim = GameObject.FindGameObjectWithTag("JamesModel").GetComponent<Animator>();
        if (Equals(SitJamesAnim, null))
            SitJamesAnim = GameObject.FindGameObjectWithTag("SitJames").GetComponent<Animator>();
        if (Equals(DoorAnim, null))
            DoorAnim = GameObject.FindGameObjectWithTag("Door").GetComponent<Animator>();
        if (Equals(HandAnim, null))
            HandAnim = GameObject.FindGameObjectWithTag("LeftHand").GetComponent<Animator>();
    }

    #region < 제임스 애니메이션 트리거 >
    public void JamesBBaGGom()
    {
        PlayerAnim.SetBool("isBBaGGom", true);
    }
    public void JamesCenterToEnd()
    {
        PlayerAnim.SetBool("isCenterToEnd", true);
    }
    public void JamesMove()
    {
        PlayerAnim.SetBool("isMove", true);
    }
    public void JamesBedRecline()
    {
        PlayerAnim.SetBool("isBedRecline", true);
    }
    public void JamesBedIdle()
    {
        PlayerAnim.SetBool("isBedIdle", true);
    }
    public void JamesKuengKueng()
    {
        PlayerAnim.SetBool("isKuengKueng", true);
    }
    public void JamesSmellEnd()
    {
        PlayerAnim.SetBool("isSmellEnd", true);
    }
    public void JamesBedBreastTalk1()
    {
        PlayerAnim.SetBool("isBedBreastTalk1", true);
    }
    public void JamesBedBreastTalkIdle()
    {
        PlayerAnim.SetBool("isBedBreastTalkIdle", true);
    }
    public void JamesBedBreastTalkIdle2()
    {
        PlayerAnim.SetBool("isBedBreastTalkIdle2", true);
    }
    public void JamesBedBreastTalkEnd()
    {
        PlayerAnim.SetBool("isBedBreastTalkEnd", true);
    }
    public void JamesBedBreastTalkEnd2()
    {
        PlayerAnim.SetBool("isBedBreastTalkEnd2", true);
    }
    public void JamesBedBreastTalkEnd3()
    {
        PlayerAnim.SetBool("isBedBreastTalkEnd3", true);
    }
    public void JamesBedBreastTalkEnd4()
    {
        PlayerAnim.SetBool("isBedBreastTalkEnd4", true);
    }
    public void JamesBedSmile()
    {
        PlayerAnim.SetBool("isBedSmile", true);
    }
    public void JamesRepeatBedBreastTalk()
    {
        PlayerAnim.SetBool("isRepeatBedBreastTalk", true);
    }
    public void NotJamesRepeatBedBreastTalk()
    {
        PlayerAnim.SetBool("isRepeatBedBreastTalk", false);
    }
    public void JamesBedIdle2()
    {
        PlayerAnim.SetBool("isBedIdle2", true);
    }
    public void JamesHeadAttack()
    {
        PlayerAnim.SetBool("isBedTwoPunch", true);
    }
    public void JamesBedIdle3()
    {
        PlayerAnim.SetBool("isBedIdle3", true);
    }
    public void JamesBedIdle4()
    {
        PlayerAnim.SetBool("isBedIdle4", true);
    }   
    public void JamesCheekAttackT()
    {
        PlayerAnim.SetTrigger("isCheekAttackT");
    }
    public void JamesCheekAttack()
    {
        PlayerAnim.SetBool("isCheekAttack", true);
    }
    public void JamesWhiningStart()
    {
        PlayerAnim.SetBool("isWhiningStart", true);
    }
    public void JamesWhiningEnd()
    {
        PlayerAnim.SetBool("isWhiningEnd", true);
    }
    public void JamesAfterAttack()
    {
        PlayerAnim.SetBool("isAfterAttack", true);
    }
    public void JamesFingerStep()
    {
        SitJamesAnim.SetBool("isFingerStep", true);   
    }
    public void JamesShy()
    {
        SitJamesAnim.SetBool("isShy", true);
    }
    public void JamesShy2()
    {
        SitJamesAnim.SetBool("isShy2", true);
    }
    public void JamesShyTell()
    {
        SitJamesAnim.SetBool("isShyTell", true);
    }
    public void JamesSkinshipHead()
    {
        SitJamesAnim.SetBool("isSkinshipHead", true);
    }
    public void JamesSkinshipHead_R()
    {
        SitJamesAnim.SetBool("isSkinshipHead", false);
    }
    public void JamesSurprised()
    {
        SitJamesAnim.SetBool("isSurprised", true);
    }
    public void JamesSurprised_R()
    {
        SitJamesAnim.SetBool("isSurprised", false);
    }
    public void JamesHeadEnd()
    {
        SitJamesAnim.SetBool("isHeadEnd", true);
    }
    public void JamesSpoon()
    {
        SitJamesAnim.SetBool("isSpoon", true);
    }
    public void JamesWaiting()
    {
        SitJamesAnim.SetBool("isWaiting", true);
    }
    public void JamesExpect()
    {
        SitJamesAnim.SetBool("isExpect", true);
    }
    public void JamesExpectTell()
    {
        SitJamesAnim.SetBool("isExpectTell", true);
    }
    public void JamesWicked()
    {
        SitJamesAnim.SetBool("isWicked", true);
    }
    public void JamesFrusrated()
    {
        SitJamesAnim.SetBool("isFrustrated", true);
    }
    public void JamesFrusratedTell()
    {
        SitJamesAnim.SetBool("isFrustratedTell", true);
    }
    public void JamesFrusratedTell2()
    {
        SitJamesAnim.SetBool("isFrustratedTell2", true);
    }
    public void JamesFrusratedIdle()
    {
        SitJamesAnim.SetBool("isFrustratedIdle", true);
    }
    public void JamesSkinshipFrustrated1()
    {
        SitJamesAnim.SetBool("isSkinshipFrustrated1", true);
    }
    public void JamesSkinshipFrustrated3()
    {
        SitJamesAnim.SetBool("isSkinshipFrustrated3", true);
    }
    public void JamesArmIdle()
    {
        SitJamesAnim.SetBool("isArmIdle", true);
    }
    public void JamesArmTell()
    {
        SitJamesAnim.SetBool("isArmTell", true);
    }
    public void JamesArmIdle2()
    {
        SitJamesAnim.SetBool("isArmIdle2", true);
    }
    public void JamesAngry()
    {
        SitJamesAnim.SetBool("isAngry", true);
    }
    public void JamesAngryTell1()
    {
        SitJamesAnim.SetBool("isAngryTell", true);
    }
    public void JamesAngryTell2()
    {
        SitJamesAnim.SetBool("isAngryTell2", true);
    }
    public void JamesAngryIdle()
    {
        SitJamesAnim.SetBool("isAngryIdle", true);
    }
    public void JamesForgive1()
    {
        SitJamesAnim.SetBool("isForgive1", true);
    }
    public void JamesForgive2()
    {
        SitJamesAnim.SetBool("isForgive2", true);
    }
    #endregion
    #region < 플레이어 애니메이션 >
    public void PlayerFrustratedStart()
    {
        HandAnim.SetBool("PlayerFrustrated", true);
    }
    public void PlayerFrustrated()
    {
        HandAnim.SetBool("isPlayerFrustrated", true);
    }
    public void PlayerFrustrated_R()
    {
        HandAnim.SetBool("isPlayerFrustrated", false);
    }
    #endregion
    #region < 오브젝트 애니메이션 >
    public void DoorOpen()
    {
        DoorAnim.SetBool("isOpen", true);
    }
    public void DoorOpen_R()
    {
        DoorAnim.SetBool("isOpen", false);
    }
    public void DoorSmash()
    {
        DoorAnim.SetBool("isSmash", true);
    }
    public void DoorForcedClose()
    {
        DoorAnim.SetBool("isClose", true);
    }
    #endregion
}