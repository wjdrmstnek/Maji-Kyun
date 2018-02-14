using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Tutorial : MonoBehaviour
{
    #region < 변수 >
    // 대사창 이미지
    public Image NameColorImage;
    // 대사창 텍스트
    public Text ScriptText;
    // 대사간 딜레이
    float scriptDelay = 3.0f;
    // 대사 최대 수
    int maxScriptIndex = 5;
    // 대사창 내레이션 스프라이트
    public Sprite ColorSprite;
    // 지시 UI 이미지
    public Image ExplanationImage;
    // 지시 UI 텍스트
    public Text ExplanationText;
    // 진행 플래그
    bool checkFlag = true;

    public bool[] checkNotRepeatStop;
    public bool[] checkNotRepeatFade;

    #region < 텍스트 관련 변수 >
    // (유니티) 프로젝트 폴더에 텍스트를 놓으면 텍스트 에셋으로 변환된다
    TextAsset ta;
    // 개행에 사용할 정수형 변수 선언
    int newline;
    // 읽어온 텍스트 임시 저장      
    string stringBuf;
    // 텍스트 출력 완료 및 중복 출력을 체크할 bool타입 변수 선언
    public bool check;
    // 읽어올 텍스트 파일명
    public string fileName;
    // 텍스트의 내용
    public TextMesh contentsMesh;
    public Text contents;
    // 텍스트 출력 간격 설정
    public float seconds;
    // 텍스트 소거 딜레이
    float postDelay = 3.0f;
    // 대사 번호
    public int scriptIndex;
    #endregion

    #endregion

    #region < 오브젝트, 컴포넌트 > 
    public VRController VRC;
    #endregion

    void Awake()
    {
        #region < Find >
        if (Equals(ScriptText, null))
            ScriptText = GetComponent<Text>();
        if (Equals(NameColorImage, null))
            NameColorImage = GameObject.FindGameObjectWithTag("NameColorImage").GetComponent<Image>();
        if (Equals(ExplanationText, null))
            ExplanationText = GameObject.FindGameObjectWithTag("ExplanationText").GetComponent<Text>();
        #endregion
        SteamVR_Fade.View(Color.clear, 0.4f);

        if (Equals(ta, null))
        {
            ta = Resources.Load<TextAsset>("text/" + fileName);
            stringBuf = ta.text;// 텍스트 파일 로드
        }
        else if (Equals(ta, null))
            Debug.Log("error - text asset is null");

        newline = 0;
        StartCoroutine("PrintStart");
    }

    void Update()
    {
        if (Equals(scriptIndex, 3) && VRC.triggerButtonPressed && !checkNotRepeatFade[0])
        {
            checkNotRepeatFade[0] = true;
            StartScript();
            check = true;
            //Debug.Log("3임 함꺼짐");          
        }
        if (Equals(scriptIndex, 4) && VRC.triggerButtonPressed && !checkNotRepeatFade[1])
        {
            checkNotRepeatFade[1] = true;
            StartScript();
            check = true;
            //Debug.Log("4임 함 가");
        }
        //Debug.Log("우아우아");

    }

    #region < Print >
    public IEnumerator PrintStart()
    {
        while (true)
        {
            if (check)
            {
                StartCoroutine(waitCoroutine(postDelay));
                StopScripting(2);
                StopScripting(3);
                StopScripting(6);
            }
            yield return new WaitForSeconds(postDelay);
        }
    }

    IEnumerator waitCoroutine(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        AddIndex();
        #region < Play Events >
        for (int i = 0; i < maxScriptIndex; i++)
            PlayEvent(i);
        #endregion
        StartCoroutine(Print());
    }

    IEnumerator Print()
    {
        int count = 0;
        char[] parse = Parsing(stringBuf);

        for (count = 0; count < parse.Length; count++)
        {// 카운트 수만큼 텍스트 내용을 분류하여 한 글자씩 출력시킨다.
            contents.text += parse[count].ToString();

            // 일정 시간 만큼 기다린 후 출력한다
            yield return new WaitForSeconds(seconds);
        }
    }

    // 텍스트 파일 바꾸는거
    public void ChangeTextFile(string textFileName)
    {
        fileName = textFileName;
        ta = Resources.Load<TextAsset>("text/" + fileName);
        stringBuf = ta.text;// 텍스트 파일 로드

        if (Equals(ta, null))
            Debug.Log("error - text asset is null");

        newline = 0;
        //Debug.Log("full contents : " + stringBuf);
        //Debug.Log("text file was loaded");
        //Debug.Log(fileName);
    }
    #endregion

    #region < Parsing Block >
    char[] Parsing(string texts)
    {
        // string 타입의 문자열을 char형으로 캐스팅
        char[] ca = texts.ToCharArray();
        char[] savePoint = string.Empty.ToCharArray();
        texts = string.Empty;

        for (int i = newline; i < ca.Length; i++)
        {
            texts = string.Format(texts + ca[i].ToString());

            if (ca[i] == '\r')// 개행 문자를 발견할 경우
            {   // C#의 개행 문자는 2개('\r\n',  버전 별로 다를 수 있다)
                // 개행 지점을 발견한 정보를 정수형으로 저장
                newline = i + 2;
                savePoint = texts.ToCharArray();
                break;
            }
        }
        return savePoint;
    }
    #endregion

    #region < Events >
    void AddIndex()
    {
        scriptIndex += 1;
        //Debug.Log("scriptIndex : " + scriptIndex);
    }

    void PlayEvent(int index)
    {
        index = scriptIndex;
        #region < 이벤트 1 ~ 10 >
        if (Equals(index, 1))
        {
            if (checkFlag)
            {
                postDelay = 3.0f;
                scriptDelay = 3.0f;
                NameColorImage.sprite = ColorSprite;
                OnScriptBox();
                //Debug.Log("scriptIndex: " + scriptIndex);

                checkFlag = false;
            }
        }
        if (Equals(index, 2))
        {
            checkFlag = true;

            if (checkFlag)
            {
                //Debug.Log("scriptIndex: " + scriptIndex);
                NameColorImage.sprite = ColorSprite;
                OnScriptBox();
                ExplanationText.text = " 《 대사창: ON 》 ";

                StartCoroutine(ScriptSliding(ScriptText));

                checkFlag = false;
            }
        }
        if (Equals(index, 3))
        {
            checkFlag = true;

            if (checkFlag)
            {
                postDelay = 0.1f;
                scriptDelay = 0.1f;
                //Debug.Log("scriptIndex: " + scriptIndex);
                NameColorImage.sprite = ColorSprite;
                OnScriptBox();

                checkFlag = false;
            }
        }
        if (Equals(index, 4))
        {
            checkFlag = true;

            if (checkFlag)
            {
                check = true;
                postDelay = 0.1f;
                scriptDelay = 0.1f;
                //Debug.Log("scriptIndex: " + scriptIndex);
                ExplanationText.text = "  다시 누르시면 대사창이 ON됩니다. ";
                OffScriptBox();

                checkFlag = false;
            }
        }
        if (Equals(index, 5))
        {
            checkFlag = true;

            if (checkFlag)
            {
                check = true;
                postDelay = 3.0f;
                scriptDelay = 3.0f;
                ScriptText.text = "";
                Debug.Log("scriptIndex: " + scriptIndex);
                StartCoroutine(ScriptSliding(ScriptText));
                ExplanationText.text = "튜토리얼을 종료합니다.";
                OnScriptBox();

                checkFlag = false;
            }
        }
        if (Equals(index, 6))
        {
            checkFlag = true;

            if (checkFlag)
            {
                check = true;
                Debug.Log("scriptIndex: " + scriptIndex);
                StartCoroutine(ScriptSliding(ScriptText));
                OffScriptBox();
                ExplanationText.text = "곧, 게임이 시작됩니다.";

                checkFlag = false;
            }
        }
        if (Equals(index, 7))
        {
            checkFlag = true;

            if (checkFlag)
            {
                check = true;
                ExplanationImage.enabled = false;
                ExplanationText.text = "";
                GameLoad();
                checkFlag = false;
            }
        }
        #endregion
    }

    void StopScripting(int index)
    {
        index = scriptIndex;

        #region < 스탑스탑 >
        if (Equals(index, 2))
        {
            if (!checkNotRepeatStop[0])
            {
                checkNotRepeatStop[0] = true;
                check = false;
                StopCoroutine("PrintStart");
            }
        }
        if (Equals(index, 3))
        {
            if (!checkNotRepeatStop[1])
            {
                checkNotRepeatStop[1] = true;
                check = false;
                StopCoroutine("PrintStart");
            }
        }
        if (Equals(index, 6))
        {
            if (!checkNotRepeatStop[2])
            {
                checkNotRepeatStop[2] = true;
                check = false;
                StopCoroutine("PrintStart");
            }
        }

        #endregion
    }
    #endregion

    #region < Reset Text >
    public IEnumerator ScriptSliding(Text scriptText)
    {
        scriptText = ScriptText;
        // 스크립트 지속시간
        yield return new WaitForSeconds(scriptDelay);
        // 스크립트 내용 초기화
        scriptText.text = "";
        //Debug.Log("지워지워 " + scriptIndex);
        StopCoroutine("ScriptSliding");
    }
    #endregion

    void GameLoad()
    {
        SceneManager.LoadSceneAsync(2);
    }

    #region < 사운드 >
    void SfxPlay(int index)
    {
        //SfxSource.enabled = false;
        //SfxSource.clip = SfxSounds[index];
        //SfxSource.enabled = true;
        checkFlag = false;
    }
    #endregion

    #region < 대사창 >
    void OnScriptBox()
    {
        NameColorImage.enabled = true;
        ScriptText.enabled = true;
        //Debug.Log("OnScript");
    }
    void OffScriptBox()
    {
        NameColorImage.enabled = false;
        ScriptText.enabled = false;
        //Debug.Log("OffScript");
    }
    public void StartScript()
    {
        StartCoroutine("PrintStart");
    }
    void FastDelayFadeOut(Color color)
    {
        SteamVR_Fade.View(color, 0.1f);
    }
    void DelayFadeOut(Color color)
    {
        SteamVR_Fade.View(color, 1.0f);
    }
    void FastDelayFadeIn()
    {
        SteamVR_Fade.View(Color.clear, 0.1f);
    }
    void DelayFadeIn()
    {
        SteamVR_Fade.View(Color.clear, 1.0f);
    }
    #endregion

    //#region < 이펙트 >
    //void PlaySakuraEffect()
    //{
    //    SakuraEffect.SetActive(true);
    //}
    //void StopSakuraEffect()
    //{
    //    SakuraEffect.SetActive(false);
    //}
    //void PlayAuraEffect()
    //{
    //    AuraEffect.SetActive(true);
    //}
    //void StopAuraEffect()
    //{
    //    AuraEffect.SetActive(false);
    //}
    //void PlayHeartEffect()
    //{
    //    HeartEffect.SetActive(true);
    //}
    //void StopHeartEffect()
    //{
    //    HeartEffect.SetActive(false);
    //}
    //#endregion
}