using UnityEngine;
using UnityEngine.UI;

public class GameRightCylinder : MonoBehaviour
{
    public VRController VRC;
    public SkinTrigger ST;
    public StateManager SM;
    public RayCast RC;
    public ReadText RT;
    public Image NameColorImage;
    public Image ScriptImage;
    public Text ScriptText;

    // 기능 bool
    public bool isCCOnOff = true;
    bool isCheckNameColor = true;
    bool isDontOpenNCI;
    public bool isClosed = true;
    // 체크 bool
    public bool isCheckOnOff;

    void Awake()
    {
        GetComponent<MeshRenderer>().enabled = false;
        #region < Find >
        if (Equals(ScriptImage, null))
            ScriptImage = GameObject.FindGameObjectWithTag("ScriptImage").GetComponent<Image>();
        if (Equals(ScriptText, null))
            ScriptText = GameObject.FindGameObjectWithTag("ScriptText").GetComponent<Text>();
        #endregion
        ScriptText.enabled = true;
        NameColorImage.enabled = false;
        isDontOpenNCI = RT.isDontOpenNameColor;
    }

    void Update()
    {
        isDontOpenNCI = RT.isDontOpenNameColor;
        if (!Equals(RT.scriptIndex, 21) && !Equals(RT.scriptIndex, 22) && !Equals(RT.scriptIndex, 40) && !Equals(RT.scriptIndex, 41) &&
            !Equals(RT.scriptIndex, 43) && !Equals(RT.scriptIndex, 44))
        {
            OnOffCCButton();
            OnOffScriptBox();
        }
    }

    void OnOffCCButton()
    {
        if (VRC.triggerButtonPressed && !isCheckOnOff)
        {
            isCheckOnOff = true;
            isCCOnOff = !isCCOnOff;
            RT.isCCOnOff = isCCOnOff;
            //Debug.Log("스크립트 온오프");
            //Debug.Log("isCCOnOff 플래그 : " + isCCOnOff);
            Debug.Log("isCCOnOff 실제 기능 : " + RT.isCCOnOff);
        }
        else if (!VRC.triggerButtonPressed && isCheckOnOff)
        {
            isCheckOnOff = false;
        }
    }

    void OnOffScriptBox()
    {
        if (isCCOnOff && !RT.isCheckStartImage && !RT.isSelectioning)
            OnScriptBox();
        else if (!isCCOnOff && !RT.isDontOpenNameColor && !RT.isSelectioning)
        {         
            OffScriptBox();
        }
        RT.isCheckNameColor = isCheckNameColor;
    }
    public void OnScriptBox()
    {
        isClosed = false;
        isCheckNameColor = true;
        if (!isDontOpenNCI)
        {
            NameColorImage.enabled = true;
            //ScriptImage.enabled = true;

            //if (RT.isDontScriptImage)
            //{
            //    ScriptImage.enabled = false;
            //    Debug.Log("스크립트 이미지 꺼짐");
            //}
            //Debug.Log("isDontOpenNCI : " + isDontOpenNCI);
        }
        if (!Equals(RT.scriptIndex, 67))
            ScriptText.enabled = true;
        //Debug.Log("OnScriptBox");
    }
    void OffScriptBox()
    {
        isClosed = true;
        isCheckNameColor = false;
        NameColorImage.enabled = false;
        //ScriptImage.enabled = false;
        ScriptText.enabled = false;
        //Debug.Log("OffScriptBox");
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("어딘가에 충돌함");
    }
    void OnTriggerExit(Collider other)
    {
        //Debug.Log("어딘가에 충돌함");
    }
    void OnTriggerStay(Collider other)
    {
        //Debug.Log("어딘가에 충돌함");

        #region < 컨트롤러 선택지 선택 >
        if (Equals(other.gameObject.tag, "Image1") && VRC.triggerButtonPressed)
        {
            RC.images[0].sprite = RC.imageSprites[0];
            RC.images[1].sprite = RC.imageSprites[1];
            RC.Choice[0].enabled = true;
            RC.Choice[1].enabled = false;
            //ChoiceTexts[0].color = Alpha100;
            //ChoiceTexts[1].color = Alpha50;
            if (!RC.isSelectionTextFade[0])
            {
                RC.ChoiceTexts[0].color = RC.ColorBlack;
                RC.ChoiceTexts[1].color = RC.ColorBlack;
                RC.isSelectionTextFade[0] = true;
            }

            float alphaVal = Mathf.Lerp(1.0f, 0.0f, RC.smoothing[0]);
            RC.smoothing[0] += 0.0100f;
            //Debug.Log("스무딩 1 : " + RC.smoothing[0]);
            RC.images[1].color = new Color(RC.images[1].color.r, RC.images[1].color.g, RC.images[1].color.b, alphaVal);
            RC.Flowers[1].color = new Color(RC.Flowers[1].color.r, RC.Flowers[1].color.g, RC.Flowers[1].color.b, alphaVal);
            RC.ChoiceTexts[1].color = new Color(RC.ChoiceTexts[1].color.r, RC.ChoiceTexts[1].color.g, RC.ChoiceTexts[1].color.b, alphaVal);

            RC.Choice[0].fillAmount -= 1.0f * RC.ReduceSpeed * Time.deltaTime;
            #region < Flower >                  
            //Debug.Log(Choice[0].fillAmount);
            if (RC.Choice[0].fillAmount <= 1.0f && RC.Choice[0].fillAmount > 0.85f)
            {
                RC.Flowers[0].sprite = RC.imageSprites[3];
                //Debug.Log("갸갸");
            }
            if (RC.Choice[0].fillAmount <= 0.85f && RC.Choice[0].fillAmount > 0.6f)
            {
                RC.Flowers[0].sprite = RC.imageSprites[4];
                //Debug.Log("냐ㅑㄴ");
            }
            if (RC.Choice[0].fillAmount <= 0.6f && RC.Choice[0].fillAmount > 0.45f)
            {
                RC.Flowers[0].sprite = RC.imageSprites[5];
                //Debug.Log("댜댜");
            }
            if (RC.Choice[0].fillAmount <= 0.45f && RC.Choice[0].fillAmount > 0.3f)
            {
                RC.Flowers[0].sprite = RC.imageSprites[6];
                //Debug.Log("랴랴");
            }
            if (RC.Choice[0].fillAmount <= 0.3f && RC.Choice[0].fillAmount > 0.15f)
            {
                RC.Flowers[0].sprite = RC.imageSprites[7];
                //Debug.Log("먀ㅑㅁ");
            }
            if (RC.Choice[0].fillAmount <= 0.15f && RC.Choice[0].fillAmount > 0.0f)
            {
                RC.images[0].sprite = RC.imageSprites[2];
                RC.Flowers[0].sprite = RC.imageSprites[8];
                //Debug.Log("뱌뱌ㅑ");
            }
            #endregion

            if (Equals(RC.Choice[0].fillAmount, 0.0f))
            {
                switch (RT.scriptIndex)
                {
                    case 22:
                        RC.Choices[0].SetActive(false);
                        RC.Choices[1].SetActive(false);
                        RC.ScriptImage.enabled = false;
                        RC.WhiteImage.enabled = true;
                        RC.Selects[0].SetActive(false);
                        RC.Selects[1].SetActive(false);
                        RC.isYes = true;
                        // 선택지 선택 사운드 실행
                        RT.SfxPlay(91);
                        //StartCoroutine(RC.Fade(RC.WhiteImage, 0f, 1.1f, RC.CM));
                        //Invoke("DelayFadeOut", 1.0f);
                        RT.check = true;
                        RT.StartScript();
                        //Debug.Log("in" + RT.scriptIndex);
                        break;
                }
            }
            Debug.Log("첫번째 선택지");
        }
        else if (Equals(other.gameObject.tag, "Image2") && VRC.triggerButtonPressed)
        {
            RC.images[0].sprite = RC.imageSprites[1];
            RC.images[1].sprite = RC.imageSprites[0];
            RC.Choice[0].fillAmount = 1.0f;
            RC.Choice[0].enabled = false;
            RC.Choice[1].enabled = true;
            //ChoiceTexts[0].color = Alpha50;
            //ChoiceTexts[1].color = Alpha100;               

            float alphaVal = Mathf.Lerp(1.0f, 0.0f, RC.smoothing[1]);
            RC.smoothing[1] += 0.0100f;
            //Debug.Log("스무딩 2 : " + RC.smoothing[1]);
            RC.images[0].color = new Color(RC.images[0].color.r, RC.images[0].color.g, RC.images[0].color.b, alphaVal);
            RC.Flowers[0].color = new Color(RC.Flowers[0].color.r, RC.Flowers[0].color.g, RC.Flowers[0].color.b, alphaVal);
            RC.ChoiceTexts[0].color = new Color(RC.ChoiceTexts[0].color.r, RC.ChoiceTexts[0].color.g, RC.ChoiceTexts[0].color.b, alphaVal);

            RC.Choice[1].fillAmount -= 1.0f * RC.ReduceSpeed * Time.deltaTime;
            #region < Flower >                   
            //Debug.Log(RC.Choice[1].fillAmount);
            if (RC.Choice[1].fillAmount <= 1.0f && RC.Choice[1].fillAmount > 0.85f)
            {
                RC.Flowers[1].sprite = RC.imageSprites[3];
                //Debug.Log("갸갸");
            }
            if (RC.Choice[1].fillAmount <= 0.85f && RC.Choice[1].fillAmount > 0.6f)
            {
                RC.Flowers[1].sprite = RC.imageSprites[4];
                //Debug.Log("냐ㅑㄴ");
            }
            if (RC.Choice[1].fillAmount <= 0.6f && RC.Choice[1].fillAmount > 0.45f)
            {
                RC.Flowers[1].sprite = RC.imageSprites[5];
                //Debug.Log("댜댜");
            }
            if (RC.Choice[1].fillAmount <= 0.45f && RC.Choice[1].fillAmount > 0.3f)
            {
                RC.Flowers[1].sprite = RC.imageSprites[6];
                //Debug.Log("랴랴");
            }
            if (RC.Choice[1].fillAmount <= 0.3f && RC.Choice[1].fillAmount > 0.15f)
            {
                RC.Flowers[1].sprite = RC.imageSprites[7];
                //Debug.Log("먀ㅑㅁ");
            }
            if (RC.Choice[1].fillAmount <= 0.15f && RC.Choice[1].fillAmount > 0.0f)
            {
                RC.images[1].sprite = RC.imageSprites[2];
                RC.Flowers[1].sprite = RC.imageSprites[8];
                //Debug.Log("뱌뱌ㅑ");
            }
            #endregion

            if (Equals(RC.Choice[1].fillAmount, 0.0f))
            {
                switch (RT.scriptIndex)
                {
                    case 22:
                        RC.Choices[0].SetActive(false);
                        RC.Choices[1].SetActive(false);
                        RC.ScriptImage.enabled = false;
                        RC.WhiteImage.enabled = true;
                        RC.Selects[0].SetActive(false);
                        RC.Selects[1].SetActive(false);
                        RC.isNo = true;
                        //StartCoroutine(RC.Fade(RC.WhiteImage, 0f, 1.1f, RC.CM));
                        //Invoke("DelayFadeOut", 1.0f);
                        // 선택지 선택 사운드 실행
                        RT.SfxPlay(91);
                        RT.check = true;
                        RT.StartScript();
                        //Debug.Log("in" + RT.scriptIndex);
                        break;
                }
            }
            //Debug.Log("두번째 선택지");
        }
        else if (Equals(other.gameObject.tag, "Image3") && VRC.triggerButtonPressed)
        {
            RC.images[2].sprite = RC.imageSprites[9];
            RC.images[3].sprite = RC.imageSprites[10];
            RC.Choice[3].fillAmount = 1.0f;
            RC.Choice[2].enabled = true;
            RC.Choice[3].enabled = false;
            RC.Flowers[3].sprite = RC.imageSprites[8];

            //ChoiceTexts[2].color = Alpha100;
            //ChoiceTexts[3].color = Alpha50;
            if (!RC.isSelectionTextFade[2])
            {
                RC.ChoiceTexts[2].color = RC.ColorBlack;
                RC.ChoiceTexts[3].color = RC.ColorBlack;
                RC.isSelectionTextFade[2] = true;
            }
            RC.images[2].color = new Color(RC.images[2].color.r, RC.images[2].color.g, RC.images[2].color.b, 1.0f);
            RC.Flowers[2].color = new Color(RC.Flowers[2].color.r, RC.Flowers[2].color.g, RC.Flowers[2].color.b, 1.0f);
            RC.ChoiceTexts[2].color = new Color(RC.ChoiceTexts[2].color.r, RC.ChoiceTexts[2].color.g, RC.ChoiceTexts[2].color.b, 1.0f);

            float alphaVal = Mathf.Lerp(1.0f, 0.0f, RC.smoothing[2]);
            RC.smoothing[2] += 0.0100f;
            //Debug.Log("스무딩 3 : " + RC.smoothing[2]);
            RC.images[3].color = new Color(RC.images[3].color.r, RC.images[3].color.g, RC.images[3].color.b, alphaVal);
            RC.Flowers[3].color = new Color(RC.Flowers[3].color.r, RC.Flowers[3].color.g, RC.Flowers[3].color.b, alphaVal);
            RC.ChoiceTexts[3].color = new Color(RC.ChoiceTexts[3].color.r, RC.ChoiceTexts[3].color.g, RC.ChoiceTexts[3].color.b, alphaVal);

            RC.Choice[2].fillAmount -= 1.0f * RC.ReduceSpeed * Time.deltaTime;

            #region < Flower >                  
            //Debug.Log(Choice[2].fillAmount);
            if (RC.Choice[2].fillAmount <= 1.0f && RC.Choice[2].fillAmount > 0.85f)
            {
                RC.Flowers[2].sprite = RC.imageSprites[12];
                //Debug.Log("갸갸");
            }
            if (RC.Choice[2].fillAmount <= 0.85f && RC.Choice[2].fillAmount > 0.6f)
            {
                RC.Flowers[2].sprite = RC.imageSprites[13];
                //Debug.Log("냐ㅑㄴ");
            }
            if (RC.Choice[2].fillAmount <= 0.6f && RC.Choice[2].fillAmount > 0.45f)
            {
                RC.Flowers[2].sprite = RC.imageSprites[14];
                //Debug.Log("댜댜");
            }
            if (RC.Choice[2].fillAmount <= 0.45f && RC.Choice[2].fillAmount > 0.3f)
            {
                RC.Flowers[2].sprite = RC.imageSprites[15];
                //Debug.Log("랴랴");
            }
            if (RC.Choice[2].fillAmount <= 0.3f && RC.Choice[2].fillAmount > 0.15f)
            {
                RC.Flowers[2].sprite = RC.imageSprites[16];
                //Debug.Log("먀ㅑㅁ");
            }
            if (RC.Choice[2].fillAmount <= 0.15f && RC.Choice[2].fillAmount > 0.0f)
            {
                RC.images[2].sprite = RC.imageSprites[11];
                RC.Flowers[2].sprite = RC.imageSprites[8];
                //Debug.Log("뱌뱌ㅑ");
            }
            #endregion

            if (Equals(RC.Choice[2].fillAmount, 0.0f))
            {
                switch (RT.scriptIndex)
                {
                    case 43:
                    case 44:
                        //Debug.Log("gogogogogogogogogo");
                        RC.Choices[2].SetActive(false);
                        RC.Choices[3].SetActive(false);
                        RC.ScriptImage.enabled = false;
                        RC.WhiteImage.enabled = true;
                        RC.Selects[2].SetActive(false);
                        RC.Selects[3].SetActive(false);
                        //// 선택지 선택 사운드 실행
                        //RT.SfxPlay(91);
                        RT.check = true;
                        break;
                }
            }
            Debug.Log("세번째 선택지");
        }
        else if (Equals(other.gameObject.tag, "Image4") && VRC.triggerButtonPressed)
        {
            RC.images[2].sprite = RC.imageSprites[10];
            RC.images[3].sprite = RC.imageSprites[9];
            RC.Choice[2].fillAmount = 1.0f;
            RC.Choice[2].enabled = false;
            RC.Choice[3].enabled = true;
            RC.Flowers[2].sprite = RC.imageSprites[8];

            //ChoiceTexts[2].color = Alpha50;
            //ChoiceTexts[3].color = Alpha100;
            if (!RC.isSelectionTextFade[3])
            {
                RC.ChoiceTexts[2].color = RC.ColorBlack;
                RC.ChoiceTexts[3].color = RC.ColorBlack;
                RC.isSelectionTextFade[3] = true;
            }

            float alphaVal = Mathf.Lerp(1.0f, 0.0f, RC.smoothing[3]);
            RC.smoothing[3] += 0.0100f;
            //Debug.Log("스무딩 4 : " + RC.smoothing[3]);
            RC.images[3].color = new Color(RC.images[3].color.r, RC.images[3].color.g, RC.images[3].color.b, 1.0f);
            RC.Flowers[3].color = new Color(RC.Flowers[3].color.r, RC.Flowers[3].color.g, RC.Flowers[3].color.b, 1.0f);
            RC.ChoiceTexts[3].color = new Color(RC.ChoiceTexts[3].color.r, RC.ChoiceTexts[3].color.g, RC.ChoiceTexts[3].color.b, 1.0f);

            RC.images[2].color = new Color(RC.images[2].color.r, RC.images[2].color.g, RC.images[2].color.b, alphaVal);
            RC.Flowers[2].color = new Color(RC.Flowers[2].color.r, RC.Flowers[2].color.g, RC.Flowers[2].color.b, alphaVal);
            RC.ChoiceTexts[2].color = new Color(RC.ChoiceTexts[2].color.r, RC.ChoiceTexts[2].color.g, RC.ChoiceTexts[2].color.b, alphaVal);

            RC.Choice[3].fillAmount -= 1.0f * RC.ReduceSpeed * Time.deltaTime;

            #region < Flower >                  
            //Debug.Log(Choice[3].fillAmount);
            if (RC.Choice[3].fillAmount <= 1.0f && RC.Choice[3].fillAmount > 0.85f)
            {
                RC.Flowers[3].sprite = RC.imageSprites[12];
                //Debug.Log("갸갸");
            }
            if (RC.Choice[3].fillAmount <= 0.85f && RC.Choice[3].fillAmount > 0.6f)
            {
                RC.Flowers[3].sprite = RC.imageSprites[13];
                //Debug.Log("냐ㅑㄴ");
            }
            if (RC.Choice[3].fillAmount <= 0.6f && RC.Choice[3].fillAmount > 0.45f)
            {
                RC.Flowers[3].sprite = RC.imageSprites[14];
                //Debug.Log("댜댜");
            }
            if (RC.Choice[3].fillAmount <= 0.45f && RC.Choice[3].fillAmount > 0.3f)
            {
                RC.Flowers[3].sprite = RC.imageSprites[15];
                //Debug.Log("랴랴");
            }
            if (RC.Choice[3].fillAmount <= 0.3f && RC.Choice[3].fillAmount > 0.15f)
            {
                RC.Flowers[3].sprite = RC.imageSprites[16];
                //Debug.Log("먀ㅑㅁ");
            }
            if (RC.Choice[3].fillAmount <= 0.15f && RC.Choice[3].fillAmount > 0.0f)
            {
                RC.images[3].sprite = RC.imageSprites[11];
                RC.Flowers[3].sprite = RC.imageSprites[8];
                //Debug.Log("뱌뱌ㅑ");
            }
            #endregion

            if (Equals(RC.Choice[3].fillAmount, 0.0f))
            {
                switch (RT.scriptIndex)
                {
                    case 43:
                    case 44:
                        //Debug.Log("nonononononono");
                        RT.ChangeTextFile("Text_Story_JamesRoom_NO");
                        RC.Choices[2].SetActive(false);
                        RC.Choices[3].SetActive(false);
                        RC.ScriptImage.enabled = false;
                        RC.WhiteImage.enabled = true;
                        RC.Selects[2].SetActive(false);
                        RC.Selects[3].SetActive(false);
                        //// 선택지 선택 사운드 실행
                        //RT.SfxPlay(91);
                        RT.scriptIndex = 60;
                        RT.check = true;
                        break;
                }
            }
            Debug.Log("네번째 선택지");
        }
        else
        {
            RC.Yes.fillAmount = 1.0f;
            RC.No.fillAmount = 1.0f;
            RC.images[0].sprite = RC.imageSprites[1];
            RC.images[1].sprite = RC.imageSprites[1];
            RC.images[2].sprite = RC.imageSprites[10];
            RC.images[3].sprite = RC.imageSprites[10];

            for (int i = 0; i <= 3; i++)
            {
                RC.images[i].color = new Color(RC.images[i].color.r, RC.images[i].color.g, RC.images[i].color.b, 1.0f);
                RC.Flowers[i].color = new Color(RC.Flowers[i].color.r, RC.Flowers[i].color.g, RC.Flowers[i].color.b, 1.0f);
                RC.ChoiceTexts[i].color = new Color(RC.ChoiceTexts[i].color.r, RC.ChoiceTexts[i].color.g, RC.ChoiceTexts[i].color.b, 1.0f);
            }
            for (int i = 0; i <= 3; i++)
                RC.smoothing[i] = 0.0f;

            for (int i = 0; i <= 1; i++)
                RC.Choice[i].enabled = false;
            for (int i = 0; i <= 1; i++)
                RC.ChoiceTexts[i].color = RC.ColorBlack;
            for (int i = 0; i <= 3; i++)
                RC.isSelectionTextFade[i] = false;
            for (int i = 0; i <= 3; i++)
                RC.Flowers[i].sprite = RC.imageSprites[8];
            for (int i = 0; i <= 3; i++)
                RC.isFadeImages[i] = false;
            for (int i = 0; i < RC.ChoiceNum; i++)
            {
                RC.Choice[i].fillAmount = 1.0f;
            }
            //Debug.Log("초기화 처리");
        }
        #endregion
    }

}
