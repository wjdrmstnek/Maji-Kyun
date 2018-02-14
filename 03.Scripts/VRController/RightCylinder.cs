using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class RightCylinder : MonoBehaviour
{
    public VRController VRC;
    public MainUI mainUI;
    public Image GameStartImage;
    public Image ExitImage;
    public Image ExplanationImage;
    public Image TrackpadImage;
    public Image TriggerImage;

    public Sprite GameStartSprite;
    public Sprite GameStartTouchSprite;
    public Sprite GameStartSelectedSprite;
    public Sprite ExitSprite;
    public Sprite ExitTouchSprite;
    public Sprite ExitSelectedSprite;

    public Text ExplanationText;

    public float fadeTime = 0.01f;
    bool isFadeOut;

    void Awake()
    {
        GetComponent<MeshRenderer>().enabled = true;
        ExplanationImage.enabled = true;
    }

    void Update()
    {
        //Debug.Log("TrackpadImage.color.a : " + TrackpadImage.color.a);
        FadeOutUI();
        FadeInUI();
        if (isFadeOut)
        {
            //Fade(TrackpadImage);
            Fade(TriggerImage);
        }
        else if (!isFadeOut)
        {
            //Fade(TrackpadImage);
            Fade(TriggerImage);
        }
    }

    void GameExit()
    {
        Application.Quit();
    }

    void GameLoad()
    {
        SceneManager.LoadSceneAsync(1);
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("어딘가에 충돌함");

    }
    void OnTriggerExit(Collider other)
    {
        //Debug.Log("어딘가에 충돌함");
        GameStartImage.sprite = GameStartSprite;
        ExitImage.sprite = ExitSprite;
        ExplanationText.text = "버튼을 향해 레이저를 쏘세요";
    }
    void OnTriggerStay(Collider other)
    {
        Debug.Log("어딘가에 충돌함");
        if (Equals(other.gameObject.tag, "Start"))
        {
            GameStartImage.sprite = GameStartTouchSprite;
            ExplanationText.text = "트리거 버튼을 누르세요";
            Debug.Log("게임 시작 버튼 커짐");
        }
        else
        {
            GameStartImage.sprite = GameStartSprite;
            ExplanationText.text = "버튼을 향해 레이저를 쏘세요";
        }

        if (Equals(other.gameObject.tag, "End"))
        {
            ExitImage.sprite = ExitTouchSprite;
            Debug.Log("게임 종료 버튼 커짐");
        }
        if (Equals(other.gameObject.tag, "End") && VRC.triggerButtonPressed)
        {
            ExitImage.sprite = ExitSelectedSprite;
            SteamVR_Fade.View(Color.black, 2.0f);
            Invoke("GameExit", 2.0f);
            //AutoFade.LoadLevel("background_test 1", 2.0f, 2.0f, Color.black);

            Debug.Log("게임 종료");
        }

        if (Equals(other.gameObject.tag, "Start") && VRC.triggerButtonPressed)
        {
            GameStartImage.sprite = GameStartSelectedSprite;
            //AutoFade.LoadLevel("background_test 1", 2.0f, 2.0f, Color.black);
            ExplanationImage.enabled = false;
            ExplanationText.enabled = false;
            SteamVR_Fade.View(Color.black, 0.4f);
            Invoke("GameLoad", 2.0f);
            Debug.Log("게임 시작");
        }
        else if (Equals(other.gameObject.tag, "Setting") && VRC.triggerButtonPressed)
        {
            //세팅화면
            mainUI.MainCanvas.SetActive(false);
            mainUI.SettingCanvas.SetActive(true);
            //Debug.Log("세팅");
        }
        else if (Equals(other.gameObject.tag, "Quit") && VRC.triggerButtonPressed)
        {
            //종료 //돌아가기
            mainUI.MainCanvas.SetActive(true);
            mainUI.SettingCanvas.SetActive(false);
            Debug.Log("종료");
        }
        //자막 OnOFF
        else if (Equals(other.gameObject.tag, "Yes") && VRC.triggerButtonPressed)
        {
            if (mainUI.UITimer <= mainUI.Timer)
            {
                //Debug.Log("이");
                // UIrender( MainMng.I.voice, VoiceON, VoiceOFF);
                mainUI.UISetting(ref MainMng.I.voice);
                mainUI.UIrender(MainMng.I.voice, mainUI.VoiceON, mainUI.VoiceOFF);

                mainUI.Timer = 0;
            }
            //Debug.Log("자막");
            return;
        }
        //보이스 ONOFF
        else if (Equals(other.gameObject.tag, "No") && VRC.triggerButtonPressed)
        {
            if (mainUI.UITimer2 <= mainUI.Timer2)
            {
                //Debug.Log("거");
                mainUI.UISetting(ref MainMng.I.script);
                mainUI.UIrender(MainMng.I.script, mainUI.ScriptON, mainUI.ScriptOFF);

                mainUI.Timer2 = 0;
            }
            //Debug.Log("보이스");
            return;
        }
    }

    void Fade(Image image)
    {    
        if(isFadeOut)
        image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a - fadeTime);
        else if (!isFadeOut)
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a + fadeTime);

    }

    void FadeOutUI()
    {
        if (TriggerImage.color.a >= 1)
            isFadeOut = true;
    }
    void FadeInUI()
    {
        if (TriggerImage.color.a <= 0)
            isFadeOut = false;
    }

    #region < Debug >
    //void Update()
    //{
    //if (ss.IsStart)
    //{

    //    AutoFade.LoadLevel("background_test 1", 2.0f, 2.0f, Color.black);
    //}

    //if (VRC.triggerButtonPressed)
    //    GetComponent<MeshRenderer>().enabled = true;
    //else { GetComponent<MeshRenderer>().enabled = false; }
    //}
    #endregion
}