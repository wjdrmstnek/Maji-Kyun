using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class RayCast : MonoBehaviour
{
    public Camera player_cam;
    public Image Yes;
    public Image No;
    public Image[] Choice;
    public Image[] Flowers;
    // 선택지
    public GameObject[] Choices;
    // 스크립트 이미지
    public Image ScriptImage;
    // 화이트 아웃 이미지
    public Image WhiteImage;
    // 타이틀 이미지
    public Image TitleImage;
    public int UI = 5;
    public int ChoiceNum = 3;
    public float ReduceSpeed = 5f;
    public float[] smoothing;
    public bool isYes;
    public bool isNo;
    public bool[] isSelectionTextFade;
    public bool[] isFadeImages;

    public ReadText RT;
    public Material CM;
    public GameObject[] FadeCube;
    public Image[] images;
    public Sprite[] imageSprites;
    public GameObject[] Selects;
    public Text[] ChoiceTexts;
    public Color32 Alpha0;
    public Color32 Alpha50;
    public Color32 Alpha100;
    public Color32 ColorRed;
    public Color32 ColorBlack;

    #region < 디버그 >
    public Text TimeScaleText;
    public Text AccelarationText;
    public Image TimeScaleImage;
    bool isOnTimeScaleImage = false;
    float moveSpeed = 3.0f;
    #endregion

    void Awake()
    {
        #region < Find >
        if (Equals(player_cam, null))
            player_cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        if (Equals(Yes, null))
            Yes = GameObject.FindGameObjectWithTag("Yes").GetComponent<Image>();
        if (Equals(No, null))
            No = GameObject.FindGameObjectWithTag("No").GetComponent<Image>();
        if (Equals(RT, null) && Equals(SceneManager.GetActiveScene().name, "background_test 1"))
            RT = GameObject.FindGameObjectWithTag("ScriptText").GetComponent<ReadText>();
        if (Equals(ScriptImage, null))
            ScriptImage = GameObject.FindGameObjectWithTag("ScriptImage").GetComponent<Image>();
        if (Equals(WhiteImage, null))
            WhiteImage = GameObject.FindGameObjectWithTag("WhiteImage").GetComponent<Image>();
        if (Equals(TitleImage, null))
            TitleImage = GameObject.FindGameObjectWithTag("TitleImage").GetComponent<Image>();
        if (Equals(images[0], null))
            images[0] = GameObject.FindGameObjectWithTag("Image1").GetComponent<Image>();
        if (Equals(images[1], null))
            images[1] = GameObject.FindGameObjectWithTag("Image2").GetComponent<Image>();
        if (Equals(images[2], null))
            images[2] = GameObject.FindGameObjectWithTag("Image3").GetComponent<Image>();
        if (Equals(images[3], null))
            images[3] = GameObject.FindGameObjectWithTag("Image4").GetComponent<Image>();
        if (Equals(Choice[0], null))
            Choice[0] = GameObject.FindGameObjectWithTag("Choice1").GetComponent<Image>();
        if (Equals(Choice[1], null))
            Choice[1] = GameObject.FindGameObjectWithTag("Choice2").GetComponent<Image>();
        if (Equals(Choice[2], null))
            Choice[2] = GameObject.FindGameObjectWithTag("Choice3").GetComponent<Image>();
        if (Equals(Choice[3], null))
            Choice[3] = GameObject.FindGameObjectWithTag("Choice4").GetComponent<Image>();
        if (Equals(Selects[0], null))
            Selects[0] = GameObject.FindGameObjectWithTag("Select1");
        if (Equals(Selects[1], null))
            Selects[1] = GameObject.FindGameObjectWithTag("Select2");
        if (Equals(Selects[2], null))
            Selects[2] = GameObject.FindGameObjectWithTag("Select3");
        if (Equals(Selects[3], null))
            Selects[3] = GameObject.FindGameObjectWithTag("Select4");
        if (Equals(ChoiceTexts[0], null))
            ChoiceTexts[0] = GameObject.FindGameObjectWithTag("ChoiceText1").GetComponent<Text>();
        if (Equals(ChoiceTexts[1], null))
            ChoiceTexts[1] = GameObject.FindGameObjectWithTag("ChoiceText2").GetComponent<Text>();
        #endregion

        Alpha0 = new Color32(255, 255, 255, 0);
        Alpha50 = new Color32(255, 255, 255, 128);
        Alpha100 = new Color32(255, 255, 255, 255);
        ColorRed = new Color32(255, 20, 20, 255);
        ColorBlack = new Color32(0, 0, 0, 255);
    }

    void Update()
    {
        //RayCastToScreen();
        AdjTimeScale();
    }

    // 비용이 큰 Update를 사용하지 않고 코루틴을 이용하여 처리한다
    public IEnumerator Fade(Image image, float StartAlpha, float EndAlpha, Material CubeMaterial)
    {
        // 임시 변수 초기화
        float curTime = 0f;
        // 페이드 지속 시간
        float durationTime = 0.5f;

        while (curTime < durationTime)
        {
            // 선형보간함수를 사용하여 값을 조정해준다
            float alphaVal = Mathf.Lerp(StartAlpha, EndAlpha, curTime / durationTime);
            // 이미지의 RGB값은 그대로 두고 알파값만 조절해준다
            image.color = new Color(image.color.r, image.color.g, image.color.b, alphaVal);
            // 이미지의 알파값을 큐브에 넣어준다
            CM.SetColor("_Color", image.color);
            // 시간을 증가시켜주며 페이드 아웃 시켜준다
            curTime += 1.5f * Time.deltaTime;
            //Debug.Log(".a " + WhiteImage.color.a);
            //Debug.Log("curTime : " + curTime);
            //Debug.Log("durationTime : " + durationTime);
            //Debug.Log("alphaVal : " + alphaVal);
            yield return null;
        }
        StopCoroutine("FadeOut");
        yield break;
    }

    void DelayFadeOut()
    {
        StartCoroutine(Fade(WhiteImage, 1f, 0f, CM));
        RT.check = true;
    }

    #region < 화면 중앙 레이를 컨트롤러 충돌체크로 전환했음 >
    //void RayCastToScreen()
    //{
    //    // 레이를 쏘기 시작할 좌표 - 화면 정중앙
    //    //Vector3 rayOrigin = player_cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
    //    Vector3 rayOrigin = player_cam.transform.position;
    //    RaycastHit hit;

    //    // 1. 레이의 위치. 2. 방향. 3. 맞은 위치. 4. 최대 범위. (5. 맞출 레이어 마스크)
    //    if (Physics.Raycast(rayOrigin, player_cam.transform.forward, out hit, Mathf.Infinity))
    //    {
    //        Debug.DrawRay(rayOrigin, player_cam.transform.forward, Color.red, 1.0f);

    //        if (Equals(hit.collider.gameObject.layer, UI))
    //        {
    //            #region < Yes / No >
    //            //if (hit.collider.gameObject.CompareTag("Yes"))
    //            //{
    //            //    Debug.Log("Yes로 쏨");
    //            //    Yes.fillAmount -= 1.0f * 0.33f * Time.deltaTime;

    //            //    if (Equals(Yes.fillAmount, 0.0f))
    //            //    {
    //            //        switch (RT.scriptIndex)
    //            //        {
    //            //            case 8:
    //            //                RT.ChangeTextFile("Text_Yes1_Dummy");
    //            //                break;
    //            //        }
    //            //    }
    //            //}
    //            //if (hit.collider.gameObject.CompareTag("No"))
    //            //{
    //            //    Debug.Log("No로 쏨");
    //            //    No.fillAmount -= 1.0f * 0.33f * Time.deltaTime;

    //            //    if (Equals(No.fillAmount, 0.0f))
    //            //    {
    //            //        switch (RT.scriptIndex)
    //            //        {
    //            //            case 8:
    //            //                RT.ChangeTextFile("Text_No1_Dummy");
    //            //                break;
    //            //        }
    //            //    }
    //            //}
    //            #endregion

    //            #region < Selectable Choices >
    //            if (hit.collider.gameObject.CompareTag("Choice1"))
    //            {
    //                images[0].sprite = imageSprites[0];
    //                images[1].sprite = imageSprites[1];
    //                Choice[0].enabled = true;
    //                Choice[1].enabled = false;
    //                //ChoiceTexts[0].color = Alpha100;
    //                //ChoiceTexts[1].color = Alpha50;
    //                if (!isSelectionTextFade[0])
    //                {
    //                    ChoiceTexts[0].color = ColorBlack;
    //                    ChoiceTexts[1].color = ColorBlack;
    //                    isSelectionTextFade[0] = true;
    //                }

    //                float alphaVal = Mathf.Lerp(1.0f, 0.0f, smoothing[0]);
    //                smoothing[0] += 0.0100f;
    //                //Debug.Log("스무딩 1 : " + smoothing[0]);
    //                images[1].color = new Color(images[1].color.r, images[1].color.g, images[1].color.b, alphaVal);
    //                Flowers[1].color = new Color(Flowers[1].color.r, Flowers[1].color.g, Flowers[1].color.b, alphaVal);
    //                ChoiceTexts[1].color = new Color(ChoiceTexts[1].color.r, ChoiceTexts[1].color.g, ChoiceTexts[1].color.b, alphaVal);

    //                Choice[0].fillAmount -= 1.0f * ReduceSpeed * Time.deltaTime;
    //                #region < Flower >                  
    //                //Debug.Log(Choice[0].fillAmount);
    //                if (Choice[0].fillAmount <= 1.0f && Choice[0].fillAmount > 0.85f)
    //                {
    //                    Flowers[0].sprite = imageSprites[3];
    //                    //Debug.Log("갸갸");
    //                }
    //                if (Choice[0].fillAmount <= 0.85f && Choice[0].fillAmount > 0.6f)
    //                {
    //                    Flowers[0].sprite = imageSprites[4];
    //                    //Debug.Log("냐ㅑㄴ");
    //                }
    //                if (Choice[0].fillAmount <= 0.6f && Choice[0].fillAmount > 0.45f)
    //                {
    //                    Flowers[0].sprite = imageSprites[5];
    //                    //Debug.Log("댜댜");
    //                }
    //                if (Choice[0].fillAmount <= 0.45f && Choice[0].fillAmount > 0.3f)
    //                {
    //                    Flowers[0].sprite = imageSprites[6];
    //                    //Debug.Log("랴랴");
    //                }
    //                if (Choice[0].fillAmount <= 0.3f && Choice[0].fillAmount > 0.15f)
    //                {
    //                    Flowers[0].sprite = imageSprites[7];
    //                    //Debug.Log("먀ㅑㅁ");
    //                }
    //                if (Choice[0].fillAmount <= 0.15f && Choice[0].fillAmount > 0.0f)
    //                {
    //                    images[0].sprite = imageSprites[2];
    //                    Flowers[0].sprite = imageSprites[8];
    //                    //Debug.Log("뱌뱌ㅑ");
    //                }
    //                #endregion

    //                if (Equals(Choice[0].fillAmount, 0.0f))
    //                {
    //                    switch (RT.scriptIndex)
    //                    {
    //                        case 22:
    //                            Choices[0].SetActive(false);
    //                            Choices[1].SetActive(false);
    //                            ScriptImage.enabled = false;
    //                            WhiteImage.enabled = true;
    //                            Selects[0].SetActive(false);
    //                            Selects[1].SetActive(false);
    //                            isYes = true;
    //                            StartCoroutine(Fade(WhiteImage, 0f, 1.1f, CM));
    //                            Invoke("DelayFadeOut", 1.0f);
    //                            RT.check = true;
    //                            RT.StartScript();
    //                            //Debug.Log("in" + RT.scriptIndex);
    //                            break;
    //                    }
    //                }
    //            }
    //            if (hit.collider.gameObject.CompareTag("Choice2"))
    //            {
    //                images[0].sprite = imageSprites[1];
    //                images[1].sprite = imageSprites[0];
    //                Choice[0].fillAmount = 1.0f;
    //                Choice[0].enabled = false;
    //                Choice[1].enabled = true;
    //                //ChoiceTexts[0].color = Alpha50;
    //                //ChoiceTexts[1].color = Alpha100;               

    //                float alphaVal = Mathf.Lerp(1.0f, 0.0f, smoothing[1]);
    //                smoothing[1] += 0.0100f;
    //                //Debug.Log("스무딩 2 : " + smoothing[1]);
    //                images[0].color = new Color(images[0].color.r, images[0].color.g, images[0].color.b, alphaVal);
    //                Flowers[0].color = new Color(Flowers[0].color.r, Flowers[0].color.g, Flowers[0].color.b, alphaVal);
    //                ChoiceTexts[0].color = new Color(ChoiceTexts[0].color.r, ChoiceTexts[0].color.g, ChoiceTexts[0].color.b, alphaVal);

    //                Choice[1].fillAmount -= 1.0f * ReduceSpeed * Time.deltaTime;
    //                #region < Flower >                   
    //                Debug.Log(Choice[1].fillAmount);
    //                if (Choice[1].fillAmount <= 1.0f && Choice[1].fillAmount > 0.85f)
    //                {
    //                    Flowers[1].sprite = imageSprites[3];
    //                    Debug.Log("갸갸");
    //                }
    //                if (Choice[1].fillAmount <= 0.85f && Choice[1].fillAmount > 0.6f)
    //                {
    //                    Flowers[1].sprite = imageSprites[4];
    //                    Debug.Log("냐ㅑㄴ");
    //                }
    //                if (Choice[1].fillAmount <= 0.6f && Choice[1].fillAmount > 0.45f)
    //                {
    //                    Flowers[1].sprite = imageSprites[5];
    //                    Debug.Log("댜댜");
    //                }
    //                if (Choice[1].fillAmount <= 0.45f && Choice[1].fillAmount > 0.3f)
    //                {
    //                    Flowers[1].sprite = imageSprites[6];
    //                    Debug.Log("랴랴");
    //                }
    //                if (Choice[1].fillAmount <= 0.3f && Choice[1].fillAmount > 0.15f)
    //                {
    //                    Flowers[1].sprite = imageSprites[7];
    //                    Debug.Log("먀ㅑㅁ");
    //                }
    //                if (Choice[1].fillAmount <= 0.15f && Choice[1].fillAmount > 0.0f)
    //                {
    //                    images[1].sprite = imageSprites[2];
    //                    Flowers[1].sprite = imageSprites[8];
    //                    Debug.Log("뱌뱌ㅑ");
    //                }
    //                #endregion

    //                if (Equals(Choice[1].fillAmount, 0.0f))
    //                {
    //                    switch (RT.scriptIndex)
    //                    {
    //                        case 22:
    //                            Choices[0].SetActive(false);
    //                            Choices[1].SetActive(false);
    //                            ScriptImage.enabled = false;
    //                            WhiteImage.enabled = true;
    //                            Selects[0].SetActive(false);
    //                            Selects[1].SetActive(false);
    //                            isNo = true;
    //                            StartCoroutine(Fade(WhiteImage, 0f, 1.1f, CM));
    //                            Invoke("DelayFadeOut", 1.0f);
    //                            RT.check = true;
    //                            RT.StartScript();
    //                            //Debug.Log("in" + RT.scriptIndex);
    //                            break;
    //                    }
    //                }
    //            }
    //            if (hit.collider.gameObject.CompareTag("Choice3"))
    //            {
    //                images[2].sprite = imageSprites[9];
    //                images[3].sprite = imageSprites[10];
    //                Choice[3].fillAmount = 1.0f;
    //                Choice[2].enabled = true;
    //                Choice[3].enabled = false;
    //                Flowers[3].sprite = imageSprites[8];

    //                //ChoiceTexts[2].color = Alpha100;
    //                //ChoiceTexts[3].color = Alpha50;
    //                if (!isSelectionTextFade[2])
    //                {
    //                    ChoiceTexts[2].color = ColorBlack;
    //                    ChoiceTexts[3].color = ColorBlack;
    //                    isSelectionTextFade[2] = true;
    //                }
    //                images[2].color = new Color(images[2].color.r, images[2].color.g, images[2].color.b, 1.0f);
    //                Flowers[2].color = new Color(Flowers[2].color.r, Flowers[2].color.g, Flowers[2].color.b, 1.0f);
    //                ChoiceTexts[2].color = new Color(ChoiceTexts[2].color.r, ChoiceTexts[2].color.g, ChoiceTexts[2].color.b, 1.0f);

    //                float alphaVal = Mathf.Lerp(1.0f, 0.0f, smoothing[2]);
    //                smoothing[2] += 0.0100f;
    //                //Debug.Log("스무딩 3 : " + smoothing[2]);
    //                images[3].color = new Color(images[3].color.r, images[3].color.g, images[3].color.b, alphaVal);
    //                Flowers[3].color = new Color(Flowers[3].color.r, Flowers[3].color.g, Flowers[3].color.b, alphaVal);
    //                ChoiceTexts[3].color = new Color(ChoiceTexts[3].color.r, ChoiceTexts[3].color.g, ChoiceTexts[3].color.b, alphaVal);

    //                Choice[2].fillAmount -= 1.0f * ReduceSpeed * Time.deltaTime;

    //                #region < Flower >                  
    //                //Debug.Log(Choice[2].fillAmount);
    //                if (Choice[2].fillAmount <= 1.0f && Choice[2].fillAmount > 0.85f)
    //                {
    //                    Flowers[2].sprite = imageSprites[12];
    //                    Debug.Log("갸갸");
    //                }
    //                if (Choice[2].fillAmount <= 0.85f && Choice[2].fillAmount > 0.6f)
    //                {
    //                    Flowers[2].sprite = imageSprites[13];
    //                    Debug.Log("냐ㅑㄴ");
    //                }
    //                if (Choice[2].fillAmount <= 0.6f && Choice[2].fillAmount > 0.45f)
    //                {
    //                    Flowers[2].sprite = imageSprites[14];
    //                    Debug.Log("댜댜");
    //                }
    //                if (Choice[2].fillAmount <= 0.45f && Choice[2].fillAmount > 0.3f)
    //                {
    //                    Flowers[2].sprite = imageSprites[15];
    //                    Debug.Log("랴랴");
    //                }
    //                if (Choice[2].fillAmount <= 0.3f && Choice[2].fillAmount > 0.15f)
    //                {
    //                    Flowers[2].sprite = imageSprites[16];
    //                    Debug.Log("먀ㅑㅁ");
    //                }
    //                if (Choice[2].fillAmount <= 0.15f && Choice[2].fillAmount > 0.0f)
    //                {
    //                    images[2].sprite = imageSprites[11];
    //                    Flowers[2].sprite = imageSprites[8];
    //                    Debug.Log("뱌뱌ㅑ");
    //                }
    //                #endregion

    //                if (Equals(Choice[2].fillAmount, 0.0f))
    //                {
    //                    switch (RT.scriptIndex)
    //                    {
    //                        case 43:
    //                        case 44:
    //                            //Debug.Log("gogogogogogogogogo");
    //                            Choices[2].SetActive(false);
    //                            Choices[3].SetActive(false);
    //                            ScriptImage.enabled = false;
    //                            WhiteImage.enabled = true;
    //                            Selects[2].SetActive(false);
    //                            Selects[3].SetActive(false);
    //                            RT.check = true;
    //                            break;
    //                    }
    //                }
    //            }
    //            if (hit.collider.gameObject.CompareTag("Choice4"))
    //            {
    //                images[2].sprite = imageSprites[10];
    //                images[3].sprite = imageSprites[9];
    //                Choice[2].fillAmount = 1.0f;
    //                Choice[2].enabled = false;
    //                Choice[3].enabled = true;
    //                Flowers[2].sprite = imageSprites[8];

    //                //ChoiceTexts[2].color = Alpha50;
    //                //ChoiceTexts[3].color = Alpha100;
    //                if (!isSelectionTextFade[3])
    //                {
    //                    ChoiceTexts[2].color = ColorBlack;
    //                    ChoiceTexts[3].color = ColorBlack;
    //                    isSelectionTextFade[3] = true;
    //                }                   

    //                float alphaVal = Mathf.Lerp(1.0f, 0.0f, smoothing[3]);
    //                smoothing[3] += 0.0100f;
    //                //Debug.Log("스무딩 4 : " + smoothing[3]);
    //                images[3].color = new Color(images[3].color.r, images[3].color.g, images[3].color.b, 1.0f);
    //                Flowers[3].color = new Color(Flowers[3].color.r, Flowers[3].color.g, Flowers[3].color.b, 1.0f);
    //                ChoiceTexts[3].color = new Color(ChoiceTexts[3].color.r, ChoiceTexts[3].color.g, ChoiceTexts[3].color.b, 1.0f);

    //                images[2].color = new Color(images[2].color.r, images[2].color.g, images[2].color.b, alphaVal);
    //                Flowers[2].color = new Color(Flowers[2].color.r, Flowers[2].color.g, Flowers[2].color.b, alphaVal);
    //                ChoiceTexts[2].color = new Color(ChoiceTexts[2].color.r, ChoiceTexts[2].color.g, ChoiceTexts[2].color.b, alphaVal);

    //                Choice[3].fillAmount -= 1.0f * ReduceSpeed * Time.deltaTime;

    //                #region < Flower >                  
    //                //Debug.Log(Choice[3].fillAmount);
    //                if (Choice[3].fillAmount <= 1.0f && Choice[3].fillAmount > 0.85f)
    //                {
    //                    Flowers[3].sprite = imageSprites[12];
    //                    Debug.Log("갸갸");
    //                }
    //                if (Choice[3].fillAmount <= 0.85f && Choice[3].fillAmount > 0.6f)
    //                {
    //                    Flowers[3].sprite = imageSprites[13];
    //                    Debug.Log("냐ㅑㄴ");
    //                }
    //                if (Choice[3].fillAmount <= 0.6f && Choice[3].fillAmount > 0.45f)
    //                {
    //                    Flowers[3].sprite = imageSprites[14];
    //                    Debug.Log("댜댜");
    //                }
    //                if (Choice[3].fillAmount <= 0.45f && Choice[3].fillAmount > 0.3f)
    //                {
    //                    Flowers[3].sprite = imageSprites[15];
    //                    Debug.Log("랴랴");
    //                }
    //                if (Choice[3].fillAmount <= 0.3f && Choice[3].fillAmount > 0.15f)
    //                {
    //                    Flowers[3].sprite = imageSprites[16];
    //                    Debug.Log("먀ㅑㅁ");
    //                }
    //                if (Choice[3].fillAmount <= 0.15f && Choice[3].fillAmount > 0.0f)
    //                {
    //                    images[3].sprite = imageSprites[11];
    //                    Flowers[3].sprite = imageSprites[8];
    //                    Debug.Log("뱌뱌ㅑ");
    //                }
    //                #endregion

    //                if (Equals(Choice[3].fillAmount, 0.0f))
    //                {
    //                    switch (RT.scriptIndex)
    //                    {
    //                        case 43:
    //                        case 44:
    //                            //Debug.Log("nonononononono");
    //                            RT.ChangeTextFile("Text_Story_JamesRoom_NO");
    //                            Choices[2].SetActive(false);
    //                            Choices[3].SetActive(false);
    //                            ScriptImage.enabled = false;
    //                            WhiteImage.enabled = true;
    //                            Selects[2].SetActive(false);
    //                            Selects[3].SetActive(false);
    //                            RT.scriptIndex = 60;
    //                            RT.check = true;
    //                            break;
    //                    }
    //                }
    //            }

    //            #region < 나머지 선택지들 >
    //            //if (hit.collider.gameObject.CompareTag("Choice3"))
    //            //{
    //            //    Debug.Log("세번째 선택지로 쏨");
    //            //    No.fillAmount -= 1.0f * 0.33f * Time.deltaTime;
    //            //    Choice[0].fillAmount = 1.0f;
    //            //    Choice[1].fillAmount = 1.0f;
    //            //    if (Equals(No.fillAmount, 0.0f))
    //            //    {
    //            //        Debug.Log("Choice3");

    //            //        switch (RT[0].scriptIndex)
    //            //        {
    //            //            case 27:
    //            //                Debug.Log("272727272727");
    //            //                RT[0].ChangeTextFile("Text_Choice_Dummy");
    //            //                break;
    //            //        }
    //            //    }
    //            //}
    //            #endregion

    //            #endregion
    //        }
    //        else
    //        {
    //            Yes.fillAmount = 1.0f;
    //            No.fillAmount = 1.0f;
    //            images[0].sprite = imageSprites[1];
    //            images[1].sprite = imageSprites[1];
    //            images[2].sprite = imageSprites[10];
    //            images[3].sprite = imageSprites[10];

    //            for (int i = 0; i <= 3; i++)
    //            {
    //                images[i].color = new Color(images[i].color.r, images[i].color.g, images[i].color.b, 1.0f);
    //                Flowers[i].color = new Color(Flowers[i].color.r, Flowers[i].color.g, Flowers[i].color.b, 1.0f);
    //                ChoiceTexts[i].color = new Color(ChoiceTexts[i].color.r, ChoiceTexts[i].color.g, ChoiceTexts[i].color.b, 1.0f);
    //            }
    //            for (int i = 0; i <= 3; i++)
    //                smoothing[i] = 0.0f;

    //            for (int i = 0; i <= 1; i++)
    //                Choice[i].enabled = false;
    //            for (int i = 0; i <= 1; i++)
    //                ChoiceTexts[i].color = ColorBlack;
    //            for (int i = 0; i <= 3; i++)
    //                isSelectionTextFade[i] = false;
    //            for (int i = 0; i <= 3; i++)
    //                Flowers[i].sprite = imageSprites[8];
    //            for (int i = 0; i <= 3; i++)
    //                isFadeImages[i] = false;
    //            for (int i = 0; i < ChoiceNum; i++)
    //            {
    //                Choice[i].fillAmount = 1.0f;
    //            }
    //        }
    //    }
    //    //}
    //}
    #endregion

    #region < 디버그 >
    void AdjTimeScale()
    {
        TimeScaleText.text = Time.timeScale.ToString();
        OnOffTimeScale();
        JamesMove();

        //if (Input.GetMouseButton(0))
        //{
        //    if (Time.timeScale > 10)
        //        Time.timeScale = 10;

        //    Time.timeScale += 2f * Time.deltaTime;
        //}
        //if (Input.GetMouseButton(1))
        //{
        //    if (Time.timeScale < 0)
        //        Time.timeScale = 0;

        //    Time.timeScale -= 0.5f * Time.deltaTime;
        //}
        if (Input.GetKeyDown(KeyCode.E))
        {
            Time.timeScale = 1;
        }
    }

    void OnOffTimeScale()
    {
        if (Input.GetKeyDown(KeyCode.W) && isOnTimeScaleImage)
        {
            AccelarationText.enabled = false; TimeScaleText.enabled = false; TimeScaleImage.enabled = false; isOnTimeScaleImage = false;
        }
        else if (Input.GetKeyDown(KeyCode.W) && !isOnTimeScaleImage)
        {
            AccelarationText.enabled = true; TimeScaleText.enabled = true; TimeScaleImage.enabled = true; isOnTimeScaleImage = true;
        }
    }

    void JamesMove()
    {
        if (Input.GetKey(KeyCode.J))
            RT.JamesObj.transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.L))
            RT.JamesObj.transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.I))
            RT.JamesObj.transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.K))
            RT.JamesObj.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.U))
            RT.JamesObj.transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.O))
            RT.JamesObj.transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
    }
    #endregion
}