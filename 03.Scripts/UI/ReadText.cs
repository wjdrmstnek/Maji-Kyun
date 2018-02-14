using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ReadText : MonoBehaviour
{
    #region < 변수 >
    // 미미 영상 진행 시간
    float MiMiTime = 2.0f;
    float EndAlpha = 0.05f;
    // 애니 진행 플래그
    bool checkAnim;
    // 이펙트 진행 플래그
    bool checkEffect;
    // 사운드 재생 플래그
    bool checkSound;
    bool checkSound2;
    float smoothing1 = 0.0f;
    float smoothing2 = 0.0f;
    float smoothing3 = 0.0f;
    float smoothing4 = 0.0f;
    bool move1;
    bool move2;
    bool move3;
    public bool move4 = true;
    bool checkNOtRepeatStop1;
    bool checkNOtRepeatStop2;
    bool checkNOtRepeatStop3;
    bool checkNOtRepeatStop4;
    bool checkNOtRepeatStop5;
    bool checkNOtRepeatStop6;
    bool checkNOtRepeatStop7;
    bool checkNOtRepeatStop8;
    bool checkNOtRepeatStop9;
    bool checkNOtRepeatStop10;

    bool checkNotRepeatFade1;
    bool checkNotRepeatFade2;
    bool checkNotRepeatFade3;
    bool checkNotRepeatFade4;
    bool checkNotRepeatFade5;
    bool checkNotRepeatFade6;
    bool checkNotRepeatFade7;
    bool checkNotRepeatFade8;
    bool checkNotRepeatFade9;
    bool checkNotRepeatFade10;
    bool checkNotRepeatFade11;

    bool checkNotRepeatTrue1;
    bool checkNotRepeatTrue2;
    bool checkNotRepeatTrue3;

    bool isCheckStartRub;
    public bool isCheckStartImage = true;
    bool isGameOver;

    // true일 때 On
    public bool isCCOnOff = true;
    bool isVoiceOnOff = true;

    public bool isCheckNameColor = true;
    public bool isDontOpenNameColor = true;
    public bool isDontScriptImage = true;
    public bool isSelectioning = false;

    public bool isSelect = false;

    bool ISV = false;
    bool DSV = false;

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
    // 후딜레이
    float postDelay = 5.0f;
    // 대사 번호
    public int scriptIndex;
    #endregion

    #endregion

    #region < 오브젝트, 컴포넌트 > 
    // 가져올 스크립트
    public StateManager SM;
    public WriteText WT;
    public RayCast RC;
    // 스킨이벤트담당
    public SkinTrigger ST;
    // 벚꽃잎 이펙트
    public GameObject SakuraEffect;
    // 검은 아우라 이펙트
    public GameObject AuraEffect;
    // 하트 이펙트(일회성)
    public GameObject HeartEffect;
    // 선택지
    public GameObject [] Choices;
    // 선택지 지문
    public Text [] ChoiceText;
    // 스크립트 이미지
    public Image ScriptImage;
    // 스크립트 텍스트
    public Text ScriptText;
    // 화이트 아웃 이미지
    public Image WhiteImage;
    // 플레이어 오브젝트
    public GameObject PlayerObj;
    // 제임스 오브젝트
    public GameObject JamesObj;
    // 제임스 UI 관리
    public James james;
    // 제임스 모델
    public GameObject jamesModel;
    // 이름 이미지
    public Image NameColorImage;
    // 이름 텍스트
    public Text NameText;
    // 이름 컬러 스프라이트 Blue / Red
    public Sprite [] ColorSprite;
    // 플레이어가 이동할 위치
    public Transform [] PlayerPos;
    // 벚꽃 이펙트가 이동할 위치
    public Transform [] SakuraPos;
    // 선택지 위치
    public Transform [] UIMovePos;
    // 앉아있는 제임스
    public GameObject SitJames;
    // 스크롤 카메라
    public Camera ScrollCam;
    public GameObject ScrollCamObj;
    // 스프
    public GameObject Soup;
    // 숟가락
    public GameObject Spoon;
    // 테이블 숟가락
    public GameObject TableSpoon;
    // 쓰다듬는 손
    public GameObject RubbingHand;
    // 제임스 왼손
    public GameObject JamesLHand;
    // 둥근 터치 구
    public GameObject TouchSphere;
    // 둥근 터치 구 위치
    public Transform [] HandPos;
    // 손가락 터치 UI월드 위치
    public RectTransform [] HandCanvasPos;
    // 엔딩 이미지
    public GameObject ED1Image;
    public GameObject ED2Image;
    public GameObject GameOverImage;
    // 미미 세트
    public GameObject MiMiSet;
    // 미미 글자
    public GameObject MiMi;
    // 미미 음식
    public GameObject MiMiFood;
    // 선택지 캔버스
    public GameObject UICanvas;
    // 브금
    public AudioSource [] BGMs;
    // 브금 파일
    public AudioClip [] BGMFiles;
    // 터치이벤트담당
    public TouchStageManger stageManger;
    bool stage = false;
    // 선택해주세요
    public Sprite [] ExplanationSprites;
    public Image ExplanationImage;
    public Text ExplanationText;

    public Image [] OnOffButtonImages;
    public MeshRenderer RSMR;
    public MeshRenderer LSMR;
    public CapsuleCollider LCCC;
    public GameRightCylinder GRC;
    public GameLeftCylinder GLC;
    public VRController VRCLeft;
    public VRController VRCRight;
    public GeunSuCameraShake CS;
    public GameObject ED2MovePos;
    #endregion

    void Awake()
    {
        #region < Find >
        if (Equals(contents, null))
            contents = GetComponent<Text>();
        if (Equals(contentsMesh, null))
            contentsMesh = GetComponent<TextMesh>();
        if (Equals(SM, null))
            SM = GameObject.FindGameObjectWithTag("GameController").GetComponent<StateManager>();
        if (Equals(WT, null))
            WT = GameObject.FindGameObjectWithTag("Script").GetComponent<WriteText>();
        if (Equals(RC, null))
            RC = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<RayCast>();
        if (Equals(ST, null))
            ST = GameObject.FindGameObjectWithTag("SkinTrigger").GetComponent<SkinTrigger>();
        if (Equals(SakuraEffect, null))
            SakuraEffect = GameObject.FindGameObjectWithTag("SakuraEffect");
        if (Equals(ScriptImage, null))
            ScriptImage = GameObject.FindGameObjectWithTag("ScriptImage").GetComponent<Image>();
        if (Equals(WhiteImage, null))
            WhiteImage = GameObject.FindGameObjectWithTag("WhiteImage").GetComponent<Image>();
        if (Equals(james, null))
            james = GameObject.FindGameObjectWithTag("James").GetComponent<James>();
        if (Equals(jamesModel, null))
            jamesModel = GameObject.FindGameObjectWithTag("JamesModel");
        if (Equals(NameText, null))
            NameText = GameObject.FindGameObjectWithTag("NameText").GetComponent<Text>();
        if (Equals(NameColorImage, null))
            NameColorImage = GameObject.FindGameObjectWithTag("NameColorImage").GetComponent<Image>();
        if (Equals(ScriptText, null))
            ScriptText = GameObject.FindGameObjectWithTag("ScriptText").GetComponent<Text>();
        if (Equals(PlayerObj, null))
            PlayerObj = GameObject.FindGameObjectWithTag("Player");
        if (Equals(JamesObj, null))
            JamesObj = GameObject.FindGameObjectWithTag("JamesModel");
        if (Equals(SitJames, null))
            SitJames = GameObject.FindGameObjectWithTag("SitJames");
        if (Equals(Soup, null))
            Soup = GameObject.FindGameObjectWithTag("Soup");
        if (Equals(TableSpoon, null))
            TableSpoon = GameObject.FindGameObjectWithTag("TableSpoon");
        if (Equals(BGMs [0], null))
            BGMs [0] = GameObject.FindGameObjectWithTag("DarkBGM").GetComponent<AudioSource>();
        if (Equals(ScrollCam, null))
            ScrollCam = GameObject.FindGameObjectWithTag("ScrollCam").GetComponent<Camera>();
        if (Equals(RubbingHand, null))
            RubbingHand = GameObject.FindGameObjectWithTag("LeftHand");
        if (Equals(TouchSphere, null))
            TouchSphere = GameObject.FindGameObjectWithTag("TouchEvent");
        if (Equals(MiMi, null))
            MiMi = GameObject.FindGameObjectWithTag("MiMi");
        if (Equals(MiMiSet, null))
            MiMiSet = GameObject.FindGameObjectWithTag("MiMiSet");
        if (Equals(MiMiFood, null))
            MiMiFood = GameObject.FindGameObjectWithTag("MiMiFood");
        if (Equals(ExplanationImage, null))
            ExplanationImage = GameObject.FindGameObjectWithTag("ExplanationImage").GetComponent<Image>();
        if (Equals(ExplanationText, null))
            ExplanationText = GameObject.FindGameObjectWithTag("ExplanationText").GetComponent<Text>();
        if (Equals(RSMR, null))
            RSMR = GameObject.FindGameObjectWithTag("RightCylinder").GetComponent<MeshRenderer>();
        if (Equals(GRC, null))
            GRC = GameObject.FindGameObjectWithTag("RightCylinder").GetComponent<GameRightCylinder>();
        if (Equals(GLC, null))
            GLC = GameObject.FindGameObjectWithTag("LeftCylinder").GetComponent<GameLeftCylinder>();
        #endregion
        SteamVR_Fade.View(Color.clear, 0.4f);

        if (Equals(isVoiceOnOff, null) && Equals(SceneManager.GetActiveScene().name, "Main"))
        {
            isVoiceOnOff = MainMng.I.voice;
            isCCOnOff = MainMng.I.script;
        }
        else { Debug.Log("background_test 1"); }
        if (Equals(ta, null))
        {
            ta = Resources.Load<TextAsset>("text/" + fileName);
            stringBuf = ta.text;// 텍스트 파일 로드
        }
        else if (Equals(ta, null))
            Debug.Log("error - text asset is null");

        newline = 0;
        RSMR.enabled = false;
        LSMR.enabled = false;
        LCCC.enabled = false;
        //Debug.Log("full contents : " + stringBuf);
        //Debug.Log("text file was loaded");
        //Debug.Log(fileName);
        //jamesModel.GetComponent<BoxCollider>().enabled = false;
        SetColorAlpha0();
        PlayerObj.transform.localPosition = PlayerPos[0].localPosition;
        PlayerObj.transform.localRotation = PlayerPos[0].localRotation;
        StartCoroutine("PrintStart");
    }

    void Update()
    {
        ChangeNameRealTime();

        if (isCheckStartImage || isDontOpenNameColor)
        {
            NameColorImage.enabled = false;
            //ScriptImage.enabled = false;
            ScriptText.enabled = false;
        }
        //Debug.Log("isCCOn? : " + isCCOnOff);

        if (move1)
            MoveChar1();
        if (move2)
            MoveChar2();
        if (move3)
            MoveChar3();
        //if (move4)
        //    MoveMiMi();
        if (ISV)
            IncreaseSoundVolume(BGMs [0]);
        if (DSV)
            DecreaseSoundVolume(BGMs [0]);

        if (stageManger.trigger.isSucced && !checkNotRepeatTrue1)
        {
            checkNotRepeatTrue1 = true;
            check = true;
        }
        if (stageManger.trigger.isSucced && !checkNotRepeatTrue2)
        {
            checkNotRepeatTrue2 = true;
            check = true;
        }
        if (stageManger.trigger.isFail && !checkNotRepeatTrue3)
        {
            checkNotRepeatTrue3 = true;
            isGameOver = true;
            check = true;
            scriptIndex = 443;

            postDelay = 0.01f;
            WT.scriptDelay = 0.01f;
        }

        if (MiMiFood.activeInHierarchy)
            RotateObj(MiMiFood, Vector3.forward);

        #region Debug Button
        //if (Input.GetKeyDown(KeyCode.U))
        //    CCOnOffButtonOn();
        //if (Input.GetKeyDown(KeyCode.I))
        //    CCOnOffButtonOff();
        //if (Input.GetKeyDown(KeyCode.O))
        //    VoiceOnOffTestButtonOn();
        //if (Input.GetKeyDown(KeyCode.P))
        //    VoiceOnOffTestButtonOff();
        #endregion
    }

    #region < Print >
    public IEnumerator PrintStart()
    {
        while (true)
        {
            if (check)
            {
                StartCoroutine(waitCoroutine(postDelay));
                StopScripting(21);
                StopScripting(22);
                StopScripting(40);
                StopScripting(43);
                StopScripting(46);
                StopScripting(58);
                StopScripting(66);
                StopScripting(77);
            }
            yield return new WaitForSeconds(postDelay);
        }
    }

    IEnumerator waitCoroutine(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if (isCCOnOff)
        {
            //if (isCheckStartImage)
            //    ScriptImage.enabled = false;
            //else if (!isCheckStartImage)
            //    ScriptImage.enabled = true;
        }
        else if (!isCCOnOff) { ScriptImage.enabled = false; }
        if (!isCCOnOff) ScriptImage.enabled = false;
        if (Equals(scriptIndex, 21))
        {
            ScriptImage.enabled = false;
        }

        AddIndex();
        #region < Play Events >
        for (int i = 0; i < 80; i++)
            PlayEvent(i);
        #endregion
        StartCoroutine(Print());
    }

    IEnumerator Print()
    {
        int count = 0;
        char [] parse = Parsing(stringBuf);

        for (count = 0; count < parse.Length; count++)
        {// 카운트 수만큼 텍스트 내용을 분류하여 한 글자씩 출력시킨다.
            contents.text += parse [count].ToString();

            // 일정 시간 만큼 기다린 후 출력한다
            yield return new WaitForSeconds(seconds);
        }
    }

    // 텍스트 파일 바꾸는거..
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
    char [] Parsing(string texts)
    {
        // string 타입의 문자열을 char형으로 캐스팅
        char [] ca = texts.ToCharArray();
        char [] savePoint = string.Empty.ToCharArray();
        texts = string.Empty;

        for (int i = newline; i < ca.Length; i++)
        {
            texts = string.Format(texts + ca [i].ToString());

            if (ca [i] == '\r')// 개행 문자를 발견할 경우
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
            checkSound = true;

            if (checkSound)
            {
                isCheckStartImage = false;
                isDontOpenNameColor = false;
                isDontScriptImage = true;
                postDelay = 1.8f;
                WT.scriptDelay = 5.5f;
                ScriptText.enabled = true;
                OffName();
                NameColorImage.enabled = true;
                ScriptImage.enabled = false;
                GRC.isClosed = false;
                GLC.isClosed = false;
                // 당신은 주인공입니다. 어느날 주말, 평소 읽지 않던 책을 읽고 있을 무렵 뜻밖의 손님이 찾아옵니다.
                //Debug.Log("isCCOnOff : " + isCCOnOff);
                //Debug.Log("isVoiceOnOff : " + isVoiceOnOff);
                if (isVoiceOnOff) VoicePlay(1);
                if (!isCCOnOff) OffScriptBox();
            }
        }
        if (Equals(index, 2))
        {
            checkSound = true;

            if (checkSound)
            {
                ScriptImage.enabled = false;
                postDelay = 5.5f;
                DeleteScriptText();
                checkSound = false;
            }
        }
        if (Equals(index, 3))
        {
            checkSound = true;

            if (checkSound)
            {
                // 똑똑
                ScriptImage.enabled = false;
                isDontOpenNameColor = true;
                OffName();
                OffScriptBox();
                if (isVoiceOnOff) VoicePlay(2);
            }
        }
        if (Equals(index, 4))
        {
            checkSound = true;

            if (checkSound)
            {
                postDelay = 3.6f;
                WT.scriptDelay = 3.6f;
                isDontOpenNameColor = false;
                DeleteScriptText();
                // 주인공
                if (isCCOnOff) OnScriptBox(); else { OffScriptBox(); }
                if (isCCOnOff) OnPlayerName();
                // 종필이가 놀러온 건가? 이 녀석, 말도 없이 갑자기 이렇게
                if (isVoiceOnOff) VoicePlay(3);
            }
        }
        if (Equals(index, 5))
        {
            checkSound = true;
            move1 = true;

            if (checkSound)
            {
                SM.JamesBBaGGom();
                SM.DoorOpen();
                postDelay = 2.8f;
                WT.scriptDelay = 2.8f;
                DeleteScriptText();
                if (isCCOnOff) OnJamesName();
                // 문 살짝 여는 소리
                SfxPlay(83);
                // 쥬인공님? 주인공님 댁인가요??              
                if (isVoiceOnOff) VoicePlay(4);
            }
        }
        if (Equals(index, 6))
        {
            //Debug.Log("제임스 빼꼼");
            checkAnim = true;
            checkSound = true;
            move2 = true;

            if (checkSound)
            {
                SM.DoorSmash();
                postDelay = 0.5f;
                WT.scriptDelay = 0.5f;
                isDontOpenNameColor = true;
                DeleteScriptText();
                OffScriptBox();
                OffName();
                // 문 쾅
                if (isVoiceOnOff) VoicePlay(5);
            }

            if (checkAnim)
            {
                SM.JamesCenterToEnd();
                SM.DoorOpen();
                SM.JamesMove();
                //Debug.Log("문 쾅");
                checkAnim = false;
            }
        }
        if (Equals(index, 7))
        {
            //Debug.Log("제임스 다가옴");
            checkAnim = true;
            checkSound = true;
            move3 = true;

            if (checkSound)
            {
                postDelay = 2.2f;
                WT.scriptDelay = 2.2f;
                DeleteScriptText();
                isDontOpenNameColor = false;
                if (isCCOnOff) OnPlayerName();
                if (isCCOnOff) OnScriptBox(); else { OffScriptBox(); }
                ScriptImage.enabled = false;
                // 걸어오는 소리
                SfxPlay(84);
                // "어차피 열 거면 왜 물어보는 거지..?"
                if (isVoiceOnOff) VoicePlay(6);
            }
            if (checkAnim)
            {
                RC.WhiteImage.enabled = true;
                checkAnim = false;
            }
        }
        if (Equals(index, 8))
        {
            //Debug.Log("제임스 킁킁");
            checkAnim = true;
            checkSound = true;

            if (checkSound)
            {
                postDelay = 1.0f;
                WT.scriptDelay = 1.0f;
                isDontOpenNameColor = true;
                DeleteScriptText();
                OffName();
                OffScriptBox();
                //// 너는 공포감에 뻣뻣하게 굳어 아무것도 할 수가 없다. - 삭제
                //if(isVoiceOnOff) VoicePlay(7);
            }
            if (checkAnim)
            {
                SM.JamesBedRecline();
                SM.JamesBedIdle();
                checkAnim = false;
            }
        }
        if (Equals(index, 9))
        {
            //Debug.Log("납치범인가?");
            checkAnim = true;
            checkSound = true;

            if (checkSound)
            {
                postDelay = 4.4f;
                WT.scriptDelay = 4.4f;
                DeleteScriptText();
                isDontOpenNameColor = false;
                // 킁킁 소리
                SM.SfxSource.enabled = false;
                SM.SfxSource.clip = SM.VoiceDialogs [0];
                Invoke("DelaySfxPlay", 3.3f);
                if (isCCOnOff) OnScriptBox(); else { OffScriptBox(); }
                if (isCCOnOff) OnPlayerName();
                ScriptImage.enabled = false;
                // "납치범인가..? 괴물..? 혹시 난 아직도 꿈을 꾸고 있는 건가..?"
                if (isVoiceOnOff) VoicePlay(8);
            }
            if (checkAnim)
            {
                SM.JamesKuengKueng();
                SM.JamesSmellEnd();
                checkAnim = false;
            }
        }
        if (Equals(index, 10))
        {
            checkSound = true;

            if (checkSound)
            {
                postDelay = 3.5f;
                WT.scriptDelay = 3.5f;
                DeleteScriptText();
                if (isCCOnOff) OnJamesName();
                // "이 냄새는..... 역시 마왕님이 틀림 없어....!!"
                if (isVoiceOnOff) VoicePlay(9);
            }
        }
        #endregion
        #region < 이벤트 11 ~ 20 >
        if (Equals(index, 11))
        {
            checkSound = true;

            if (checkSound)
            {
                postDelay = 6.0f;
                WT.scriptDelay = 6.0f;
                DeleteScriptText();
                if (isCCOnOff)
                {
                    OnPlayerName();
                    OnScriptBox();
                }
                else
                {
                    //OffName();
                    OffScriptBox();
                }
                // "누.. 누군지는 모르겠지만 사람 잘못보셨는데요. 무단침입으로 경찰에 신고하기 전에 나가주시,,죠.....?"
                if (isVoiceOnOff) VoicePlay(10);
                ScriptImage.enabled = false;
                StopCoroutine(WT.ScriptSliding(WT.ScriptText));
            }
        }
        if (Equals(index, 12))
        {
            checkSound = true;
            checkAnim = true;

            if (checkSound)
            {
                postDelay = 8.0f;
                WT.scriptDelay = 8.0f;
                DeleteScriptText();
                if (isCCOnOff) OnJamesName();
                // "아차차, '지금의' 마왕님은 저를 모르죠~! 바보바보! 제임스는 바보야! (딱콩!)"
                if (isVoiceOnOff) VoicePlay(11);
            }
            if (checkAnim)
            {
                // 바보야! 딱콩!
                SM.JamesHeadAttack();
                checkAnim = false;
            }
        }
        if (Equals(index, 13))
        {
            //Debug.Log("제임스 토킹 시작, 반복");
            checkAnim = true;
            checkSound = true;

            if (checkSound)
            {
                postDelay = 5.0f;
                WT.scriptDelay = 5.0f;
                DeleteScriptText();
                ScriptImage.enabled = false;
                // "제 이름은 사쿠라 제임스, 마왕님의 가장 충실한 수하...!"
                if (isVoiceOnOff) VoicePlay(22);
            }
            if (checkAnim)
            {
                SM.JamesBedBreastTalk1();
                SM.JamesBedBreastTalkIdle();
                checkAnim = false;
            }
        }
        if (Equals(index, 14))
        {
            checkSound = true;

            if (checkSound)
            {
                DeleteScriptText();
                ScriptImage.enabled = false;
                // "저의 마왕님은 500년 전, 워프를 잘못 타서 인간계에 떨어지셨죠."
                if (isVoiceOnOff) VoicePlay(23);
            }
        }
        if (Equals(index, 15))
        {
            checkSound = true;

            if (checkSound)
            {
                DeleteScriptText();
                ScriptImage.enabled = false;
                //  "그리고 저를 비롯한 마왕님의 수하들은 마왕님을 애타게 찾고 있었죠..."
                if (isVoiceOnOff) VoicePlay(24);
                SM.JamesBedBreastTalkEnd();
                SM.JamesBedIdle2();
            }
        }
        if (Equals(index, 16))
        {
            checkSound = true;

            if (checkSound)
            {
                DeleteScriptText();
                ScriptImage.enabled = false;
                // "긴 시간 끝에 모두 지쳐서 포기했을때 즈음! 제가 찾은거예요, 마왕님을!"
                if (isVoiceOnOff) VoicePlay(25);
            }
        }
        if (Equals(index, 17))
        {
            checkSound = true;

            if (checkSound)
            {
                postDelay = 3.0f;
                WT.scriptDelay = 3.0f;
                DeleteScriptText();
                ScriptImage.enabled = false;
                // 이젠 다신 마왕님을 놓치지 않겠어요!
                if (isVoiceOnOff) VoicePlay(26);
                SM.JamesBedBreastTalkEnd2();
            }
            //Debug.Log("제임스 토킹 종료");
        }
        if (Equals(index, 18))
        {
            //Debug.Log("페이드");
            checkEffect = true;
            checkSound = true;

            if (checkSound)
            {
                OffName();
                OffScriptBox();
                isDontOpenNameColor = true;
                // 환생환생
                if (isVoiceOnOff) VoicePlay(13);
                postDelay = 6.0f;
                WT.scriptDelay = 6.0f;
                DeleteScriptText();
            }
            if (checkEffect)
            {
                RC.WhiteImage.enabled = true;
                if (!checkNotRepeatFade1)
                {
                    checkNotRepeatFade1 = true;
                    // 페이드 큐브 켜기
                    //CubeOn();
                    // 페이드 시작
                    //StartCoroutine(RC.Fade(RC.WhiteImage, 0f, 1.2f, RC.CM));

                    SteamVR_Fade.View(Color.white, 1.0f);
                }
                RC.TitleImage.enabled = true;
                checkEffect = false;
            }
        }
        if (Equals(index, 19))
        {
            checkEffect = true;
            checkSound = true;

            if (checkSound)
            {
                if (RC.WhiteImage.color.a < 0.1f)
                {
                    SetColorAlpha0();
                }
                if (isCCOnOff) OnScriptBox(); else { OffScriptBox(); }
                isDontOpenNameColor = false;
                postDelay = 5.5f;
                WT.scriptDelay = 5.5f;
                DeleteScriptText();
                RC.TitleImage.enabled = false;
                // 이제부터는 당신을 마왕으로 섬기는 제임스와 함께 살게됩니다.
                if (isVoiceOnOff) VoicePlay(14);
                SM.JamesBedSmile();
            }
            if (checkEffect)
            {
                //if (isCCOnOff) ScriptImage.enabled = true;
                //else if (!isCCOnOff) { ScriptImage.enabled = false; }
                if (!checkNotRepeatFade2)
                {
                    checkNotRepeatFade2 = true;
                    //StartCoroutine(RC.Fade(RC.WhiteImage, 1f, 0f, RC.CM));
                    //Invoke("CubeOff", 0.5f);
                    SteamVR_Fade.View(Color.clear, 0.5f);
                }
                StopSakuraEffect();
                checkEffect = false;
            }
        }
        if (Equals(index, 20))
        {
            checkSound = true;

            if (checkSound)
            {
                postDelay = 4.0f;
                WT.scriptDelay = 4.0f;
                DeleteScriptText();
                if (isCCOnOff) OnJamesName();
                SetColorAlpha0();
                // "마왕님 충성충성충성~!!"
                if (isVoiceOnOff) VoicePlay(15);
                SM.JamesBedBreastTalkIdle2();
                SM.JamesBedBreastTalkEnd3();
            }
        }
        #endregion
        #region < 이벤트 21 ~ 29 >   
        if (Equals(index, 21))
        {
            checkSound = true;

            if (checkSound)
            {
                //OffName();
                //isDontScriptImage = false;
                if (isCCOnOff) { NameColorImage.enabled = true; /*Debug.Log("isCCOn? : " + isCCOnOff);*/ }
                else if (!isCCOnOff) { NameColorImage.enabled = false; /*Debug.Log("isCCOn? : " + isCCOnOff);*/ }
                ScriptImage.enabled = false;
                NameColorImage.sprite = ColorSprite [4];
                Invoke("OffScriptBox", 4.5f);
                postDelay = 4.4f;
                WT.scriptDelay = 4.4f;
                DeleteScriptText();
                // 당신의 행동은 미래에 영향을 끼치니 신중하게 결정하세요.
                if (isVoiceOnOff) VoicePlay(16);
            }
        }
        if (Equals(index, 22))
        {
            checkSound = true;

            if (checkSound)
            {
                postDelay = 1.0f;
                WT.scriptDelay = 1.0f;
                isDontOpenNameColor = true;
                OnExplanationImage();
                DeleteScriptText();
                ScriptText.text = "";
                // 레이저 발사
                RSMR.enabled = true;
            }
        }
        if (Equals(index, 23))
        {
            check = true;

            checkSound = true;

            if (checkSound)
            {
                CubeOff();
                OffExplanationImage();
                isSelectioning = true;
                isDontOpenNameColor = false;
                isDontScriptImage = true;
                isSelect = false;

                // 레이저 종료
                RSMR.enabled = false;

                if (RC.isYes)
                {
                    RC.isYes = false;
                    // 제임스는 남자야?
                    if (isCCOnOff) OnPlayerName();
                    if (isCCOnOff) OnScriptBox(); else { OffScriptBox(); }
                    if (isVoiceOnOff) VoicePlay(80);
                    postDelay = 2.0f;
                    WT.scriptDelay = 2.0f;
                    DeleteScriptText();
                    ScriptImage.enabled = false;
                }
                else if (RC.isNo)
                {
                    RC.isNo = false;
                    // 제임스는 여자야?
                    ChangeTextFile("Text_Script_Female");
                    if (isCCOnOff) OnPlayerName();
                    if (isCCOnOff) OnScriptBox(); else { OffScriptBox(); }
                    if (isVoiceOnOff) VoicePlay(81);
                    postDelay = 2.0f;
                    WT.scriptDelay = 2.0f;
                    DeleteScriptText();
                    ScriptImage.enabled = false;
                }
                //Debug.Log("지금 여길 지난거야?");
                Invoke("DelayWhiningEnd", 1.2f);
            }
        }
        if (Equals(index, 24))
        {
            checkSound = true;

            if (checkSound)
            {
                postDelay = 0.01f;
                WT.scriptDelay = 0.01f;
                // 여기부터 다시 스크립트 온오프 가능
                isSelectioning = false;
                ChangeTextFile("Text_Story1");
            }
        }
        if (Equals(index, 25))
        {
            //Debug.Log("체크 갱신");
            check = true;

            checkSound = true;
            checkAnim = true;
            if (checkAnim)
            {
                // 공격 애니
                SM.JamesCheekAttack();
                SM.JamesAfterAttack();
                checkAnim = false;
            }
            if (checkSound)
            {
                if (isCCOnOff) OnScriptBox(); else { OffScriptBox();/* Debug.Log("OffScriptBox"); */}
                //GRC.isCCOnOff = true; GLC.isCCOnOff = true;
                postDelay = 5.0f;
                WT.scriptDelay = 5.0f;
                DeleteScriptText();
                if (isCCOnOff) OnJamesName();
                // "...!? 어떻게 그렇게 실례되는 말을!? 마왕님 미워요!"
                if (isVoiceOnOff) VoicePlay(17);
                //ScriptImage.enabled = false;
                //NameColorImage.enabled = true;
                //ScriptText.enabled = true;
                // 퍼억 소리
                SM.SfxSource.enabled = false;
                SM.SfxSource.clip = SM.VoiceDialogs [85];
                Invoke("DelaySfxPlay", 2.25f);

                if (!checkNotRepeatFade8)
                {
                    checkNotRepeatFade8 = true;
                    RC.WhiteImage.color = RC.ColorRed;
                    Invoke("FastDelayFadeOutRed", 2.25f);
                    Invoke("DelayFadeIn", 2.35f);
                }
            }
        }
        if (Equals(index, 26))
        {
            checkSound = true;

            if (checkSound)
            {
                SetColorAlpha0();
                if (isCCOnOff) OnJamesName();
                postDelay = 5.5f;
                WT.scriptDelay = 5.5f;
                DeleteScriptText();
                // "저의 성별을 그런 이분법적 잣대로 결정해버리다니 몹시 불쾌하군요?"
                if (isVoiceOnOff) VoicePlay(18);
            }
        }
        if (Equals(index, 27))
        {
            checkSound = true;

            if (checkSound)
            {
                postDelay = 3.8f;
                WT.scriptDelay = 8.0f;
                DeleteScriptText();
                if (isCCOnOff) OnJamesNameLarge();
                // "저의 남성성 여성성 어느 쪽이든 부정할 수 없는 저이기 때문에,
                if (isVoiceOnOff) VoicePlay(19);
            }
        }
        if (Equals(index, 28))
        {
            checkSound = true;

            if (checkSound)
            {
                postDelay = 4.8f;
                if (isCCOnOff) OnJamesNameLarge();
                SM.JamesBedBreastTalkEnd4();
                // 방금 마왕님의 발언으로 인해 제임스의 존엄성은 해쳐졌다고 보는 부분입니다."
                if (isVoiceOnOff) VoicePlay(20);
            }
        }
        if (Equals(index, 29))
        {
            checkSound = true;

            if (checkSound)
            {
                WT.scriptDelay = 7.0f;
                if (isCCOnOff) OnPlayerName();
                // "아, 내 생각이 짧았어! 나를 용서해줘!"
                if (isVoiceOnOff) VoicePlay(21);
                Invoke("DelayWhiningStart", 3.0f);
            }
        }
        if (Equals(index, 30))
        {
            //Debug.Log("벚꽃 이펙트");
            checkEffect = true;

            if (checkEffect)
            {
                OffName();
                OffScriptBox();
                PlaySakuraEffect();
                DSV = true;
                isDontOpenNameColor = true;
                checkEffect = false;
                //Debug.Log("스크립트 이미지 끄기");
                ScriptImage.enabled = false;
                ScriptText.text = "";

                if (!checkNotRepeatFade3)
                {
                    //Debug.Log("페이드");
                    CubeOn();
                    checkNotRepeatFade3 = true;
                    //StartCoroutine(RC.Fade(RC.WhiteImage, 0f, 1f, RC.CM));
                    Invoke("DelayFadeOutWhite", 4.0f);
                }
            }
        }
        if (Equals(index, 31))
        {
            checkEffect = true;

            if (checkEffect)
            {
                postDelay = 2.8f;
                OffName();
                OffScriptBox();
            }
        }
        #endregion
        #region < 이벤트 30 ~ 40 >
        if (Equals(index, 32))
        {
            //Debug.Log("시나리오 재개");
            check = true;

            checkEffect = true;
            if (checkEffect)
            {
                SetColorAlpha0();
                OffName();
                OffScriptBox();
                StopSakuraEffect();
                checkEffect = false;
                // 플레이어 이동
                PlayerObj.transform.localPosition = PlayerPos [1].localPosition;
                PlayerObj.transform.localRotation = new Quaternion(PlayerObj.transform.localRotation.x, 0, PlayerObj.transform.localRotation.z, 0);
                // 앉아있는 제임스, 스프 소환
                SitJames.SetActive(true);
                Soup.SetActive(true);
                // 프롤로그 제임스 제거
                JamesObj.SetActive(false);
                // 문 닫기
                SM.DoorOpen_R();
                SM.DoorForcedClose();

                postDelay = 0.5f;
                WT.scriptDelay = 0.5f;
                DeleteScriptText();

                if (!checkNotRepeatFade4)
                {
                    //Debug.Log("페이드");
                    checkNotRepeatFade4 = true;
                    SteamVR_Fade.View(Color.clear, 1.0f);
                    //StartCoroutine(RC.Fade(RC.WhiteImage, 1f, 0f, RC.CM));
                }
                SetColorAlpha0();
            }
        }
        if (Equals(index, 33))
        {
            checkAnim = true;
            checkSound = true;

            if (checkAnim)
            {
                SetColorAlpha0();
                CubeOff();
                ScriptText.text = "";
                ChangeTextFile("Text_Story_JamesRoom");
                SM.JamesFingerStep();
                ISV = true;
                checkAnim = false;
            }
            if (checkSound)
            {
                isDontOpenNameColor = false;
                if (isCCOnOff) OnScriptBox(); else { OffScriptBox(); }
                if (isCCOnOff) OnJamesName();
                // 코루틴 딜레이 설정
                postDelay = 6.0f;
                WT.scriptDelay = 6.0f;
                DeleteScriptText();
                // "오! 늘! 은! 마왕님을 위해 제임스가 아침 식사를 준비한 날~"
                if (isVoiceOnOff) VoicePlay(27);
            }
        }
        if (Equals(index, 34))
        {
            checkSound = true;

            if (checkSound)
            {
                // 코루틴 딜레이 설정
                postDelay = 6.0f;
                WT.scriptDelay = 6.0f;
                DeleteScriptText();
                if (isCCOnOff) OnJamesName();
                // "잘했죠? 이래 봬도 제임스 마계에 3명 밖에 없는 마계 양식 기능장이라고요!"
                if (isVoiceOnOff) VoicePlay(28);
            }
        }
        if (Equals(index, 35))
        {
            checkSound = true;

            if (checkSound)
            {
                // 코루틴 딜레이 설정
                postDelay = 4.0f;
                WT.scriptDelay = 4.0f;
                DeleteScriptText();
                if (isCCOnOff) OnPlayerName();
                SM.JamesShy();
                // "아.. 제임스는 참 대단하구나...!"
                if (isVoiceOnOff) VoicePlay(29);
            }
        }
        if (Equals(index, 36))
        {
            checkSound = true;

            if (checkSound)
            {
                // 대사 삭제됨
                scriptIndex++;
                //Debug.Log("index : " + scriptIndex);
            }
        }
        if (Equals(index, 37))
        {
            checkSound = true;

            if (checkSound)
            {
                // 코루틴 딜레이 설정
                postDelay = 8.0f;
                WT.scriptDelay = 8.0f;
                DeleteScriptText();
                if (isCCOnOff) OnJamesName();
                SM.JamesShyTell();
                // "드래곤의 귀지와 렝가의 다리털 사이에 낀 비듬, 마계 지네의 다리 등 최고급 재료만을 엄선했죠!" 
                if (isVoiceOnOff) VoicePlay(31);
            }
        }
        if (Equals(index, 38))
        {
            checkSound = true;

            if (checkSound)
            {
                // 코루틴 딜레이 설정
                postDelay = 6.8f;
                WT.scriptDelay = 6.8f;
                DeleteScriptText();
                if (isCCOnOff) OnScriptBox(); else { OffScriptBox(); }
                OffName();
                SM.JamesShy2();
                // 비주얼이- 음. 어~. 만들어 왔다고 칭찬해달라는 거 같은데.
                if (isVoiceOnOff) VoicePlay(32);
            }
        }
        if (Equals(index, 39))
        {
            checkSound = true;

            if (checkSound)
            {
                // 코루틴 딜레이 설정
                postDelay = 6.3f;
                WT.scriptDelay = 6.3f;
                DeleteScriptText();
                if (isCCOnOff) OnScriptBox(); else { OffScriptBox(); }
                OffName();
                // 제임스의 머리를 반짝이게 놔두면 세상이 멸망합니다. 쓰다듬어서 진정시키세요.
                if (isVoiceOnOff) VoicePlay(33);
            }
        }
        #endregion
        #region < 이벤트 41 ~ 50 >
        if (Equals(index, 40))
        {
            checkSound = true;
            OffName();
            OffScriptBox();
            isSelectioning = true;
            ScriptText.text = "";

            if (checkSound)
            {
                if (!stage)
                {
                    stageManger.StartTouchEventInterface(0);
                    stage = true;
                    ChangeSpherePos(0);
                    ChangeTouchUICanvasPos(0);
                }
            }
        }
        if (Equals(index, 41))
        {
            checkSound = true;

            if (checkSound)
            {
                postDelay = 2.0f;
                WT.scriptDelay = 2.0f;
                OffName();
                OffScriptBox();
                SM.JamesHeadEnd();
                SM.JamesSpoon();
                SM.SfxSource2.enabled = false;
            }
        }
        if (Equals(index, 42))
        {
            checkSound = true;

            if (checkSound)
            {
                // 코루틴 딜레이 설정
                postDelay = 4.0f;
                WT.scriptDelay = 4.0f;
                DeleteScriptText();
                isDontOpenNameColor = false;
                isDontScriptImage = true;
                if (isCCOnOff) OnJamesName();
                if (isCCOnOff) OnScriptBox(); else { OffScriptBox(); }
                PlayHeartEffect();
                stageManger.trigger.isSucced = false;
                // "데헷, 그럼 어서 드셔보세요. 제 혼신의 스프를!"
                if (isVoiceOnOff) VoicePlay(34);
                ScriptImage.enabled = false;
                SM.SfxSource2.volume = 0;
                SM.SfxSource2.enabled = false;
            }
        }
        if (Equals(index, 43))
        {
            checkSound = true;
            OffScriptBox();
            OffName();
            isSelectioning = true;

            if (checkSound)
            {
                UICanvas.transform.position = UIMovePos [1].position;
                isDontOpenNameColor = true;
                Invoke("OnSelects2", 0.8f);
                OnExplanationImage();
                StopHeartEffect();
                SM.JamesWaiting();
                //Debug.Log("훙훙");
                checkSound = false;
            }
        }
        if (Equals(index, 44))
        {
            checkSound = true;

            if (checkSound)
            {
                checkSound = false;
                isDontOpenNameColor = true;
                isSelectioning = true;
                OffScriptBox();
                OffName();
                postDelay = 0.01f;
                WT.scriptDelay = 0.01f;
            }
        }
        if (Equals(index, 45))
        {
            checkSound = true;

            if (checkSound)
            {
                LSMR.enabled = false;
                LCCC.enabled = false;
                isDontScriptImage = true;
                isSelect = false;
                // 선택지 선택 사운드 실행
                SfxPlay(91);
                // 코루틴 딜레이 설정
                postDelay = 4.5f;
                WT.scriptDelay = 4.5f;
                OffExplanationImage();
                DeleteScriptText();
                isDontOpenNameColor = false;
                if (isCCOnOff) OnPlayerName();
                if (isCCOnOff) OnScriptBox(); else { OffScriptBox(); }
                // "아하하. 제임스는 친절하구나. 잘먹을게."
                if (isVoiceOnOff) VoicePlay(35);
                ScriptImage.enabled = false;
            }
        }
        if (Equals(index, 46))
        {
            checkSound = true;

            if (checkSound)
            {
                OffName();
                OffScriptBox();
                postDelay = 0.4f;
                WT.scriptDelay = 0.4f;
                OffSpoon();
                isDontOpenNameColor = true;
                isSelectioning = false;

                if (!checkNotRepeatFade9)
                {
                    checkNotRepeatFade9 = true;
                    SetColorBlack();

                    // 페이드 큐브 켜기
                    //CubeOn();
                    // 페이드 시작
                    //StartCoroutine(RC.Fade(RC.WhiteImage, 0f, 2.2f, RC.CM));
                    SteamVR_Fade.View(Color.black, 0.2f);
                }
                SM.JamesExpect();
            }
        }
        if (Equals(index, 47))
        {
            checkSound = true;

            if (checkSound)
            {
                OffName();
                OffScriptBox();
                // 우주로 올려보냄
                OffScrollCam();
                //OnScrollCam();
                isDontOpenNameColor = true;
                //// 미ㅡ미ㅡ실행
                //Invoke("OnMiMi", 0.5f);
                //// 미미 글자 이동 시작
                //Invoke("MiMiStart", 1.0f);
                Invoke("Delaycheck", MiMiTime);
            }
        }
        if (Equals(index, 48))
        {
            checkSound = true;

            if (checkSound)
            {
                OffName();
                OffScriptBox();
                isDontOpenNameColor = false;
                postDelay = 0.1f;
                WT.scriptDelay = 0.1f;
                // 인덱스 변경 ▶ 크헉
                scriptIndex = 50;
            }
        }

        #region < Deleted >
        //if (Equals(index, 50))
        //{
        //checkSound = true;

        //if (checkSound)
        //{
        //    postDelay = 3.2f;
        //    WT.scriptDelay = 3.2f;
        //    DeleteScriptText();
        //    if(isCCOnOff) OnPlayerName();
        //    if(isCCOnOff) OnScriptBox();
        //    // "응.. 맛있어..."
        //    if(isVoiceOnOff) VoicePlay(65);
        //}
        //}
        //if (Equals(index, 51))
        //{
        //checkSound = true;

        //if (checkSound)
        //{
        //    postDelay = 3.0f;
        //    WT.scriptDelay = 3.0f;
        //    DeleteScriptText();
        //    if(isCCOnOff) OnPlayerName();
        //    if(isCCOnOff) OnScriptBox();
        //    // "..아니, 맛없어."
        //    if(isVoiceOnOff) VoicePlay(66);
        //}
        //}
        //if (Equals(index, 52))
        //{
        //checkSound = true;

        //if (checkSound)
        //{
        //    postDelay = 6.2f;
        //    WT.scriptDelay = 6.2f;
        //    DeleteScriptText();
        //    if(isCCOnOff) OnJamesName();
        //    if(isCCOnOff) OnScriptBox();
        //    // "어머, 호홓 그럴 리가 없어요~ 다시 음미해보세요~^^"
        //    if(isVoiceOnOff) VoicePlay(67);
        //}
        //}
        //if (Equals(index, 53))
        //{
        //checkSound = true;

        //if (checkSound)
        //{
        //    postDelay = 0.1f;
        //    WT.scriptDelay = 0.1f;
        //    DeleteScriptText();
        //    OffName();
        //    OffScriptBox();
        //}
        //}
        //if (Equals(index, 54))
        //{
        //checkSound = true;

        //if (checkSound)
        //{
        //    postDelay = 3.2f;
        //    WT.scriptDelay = 3.2f;
        //    DeleteScriptText();
        //    if(isCCOnOff) OnPlayerName();
        //    if(isCCOnOff) OnScriptBox();
        //    // "....맛...없어...!!"
        //    if(isVoiceOnOff) VoicePlay(69);
        //}
        //}
        //if (Equals(index, 55))
        //{
        //checkSound = true;

        //if (checkSound)
        //{
        //    postDelay = 6.2f;
        //    WT.scriptDelay = 6.2f;
        //    DeleteScriptText();
        //    if(isCCOnOff) OnJamesName();
        //    if(isCCOnOff) OnScriptBox();
        //    // "홍홍. 마왕님은 누구보다 마계 음식을 좋아하면서 왜 본심을 숨기는 걸까?"
        //    if(isVoiceOnOff) VoicePlay(70);
        //}
        //}
        //if (Equals(index, 56))
        //{
        //checkSound = true;

        //if (checkSound)
        //{
        //    postDelay = 0.2f;
        //    WT.scriptDelay = 0.2f;
        //    DeleteScriptText();
        //    OffName();
        //    OffScriptBox();
        //    scriptIndex = scriptIndex + 4;
        //}
        //}
        //if (Equals(index, 57))
        //{
        //checkSound = true;

        //if (checkSound)
        //{
        //    postDelay = 4.2f;
        //    WT.scriptDelay = 4.2f;
        //    DeleteScriptText();
        //    if(isCCOnOff) OnJamesName();
        //    if(isCCOnOff) OnScriptBox();
        //    // "크흑...윽.. 맛있어...."
        //    if(isVoiceOnOff) VoicePlay(68);
        //}
        //}
        //if (Equals(index, 58))
        //{
        //checkSound = true;

        //if (checkSound)
        //{
        //    postDelay = 0.1f;
        //    WT.scriptDelay = 0.1f;
        //    DeleteScriptText();
        //    OffName();
        //    OffScriptBox();
        //    scriptIndex = scriptIndex + 2;
        //}
        //}
        #endregion

        if (Equals(index, 50))
        {
            checkSound = true;

            if (checkSound)
            {
                // 미미 오프
                OffMiMi();
                // 플레이어 우주 이동
                PlayerObj.transform.localPosition = new Vector3(0, 200, 2000);

                if (!checkNotRepeatFade11)
                {
                    checkNotRepeatFade11 = true;
                    SetColorRed();

                    // 페이드 큐브 켜기
                    //CubeOn();
                    // 페이드 시작
                    //StartCoroutine(RC.Fade(RC.WhiteImage, 0f, 1f, RC.CM));                   
                    SteamVR_Fade.View(Color.black, 1.0f);

                    Invoke("DelayFadeIn", 0.5f);
                    //Invoke("CubeOff", 0.51f);
                }
                postDelay = 2.2f;
                WT.scriptDelay = 2.2f;
                isDontOpenNameColor = false;
                DeleteScriptText();
                if (isCCOnOff) OnPlayerName();
                if (isCCOnOff) OnScriptBox(); else { OffScriptBox(); }
                // 우당탕 소리
                SfxPlay(87);
                //ScriptImage.enabled = false;
                // "크헉...!"
                if (isVoiceOnOff) VoicePlay(71);
            }
        }
        if (Equals(index, 51))
        {
            checkSound = true;

            if (checkSound)
            {
                // 미ㅡ미ㅡ 종료. 페이드 인
                if (!checkNotRepeatFade10)
                {
                    checkNotRepeatFade10 = true;
                    SetColorBlack();
                    // 페이드 큐브 켜기
                    //CubeOn();
                    // 페이드 시작
                    //StartCoroutine(RC.Fade(RC.WhiteImage, 1f, 0f, RC.CM));

                    // 페이드 큐브 끄기
                    //Invoke("CubeOff", 0.5f);
                }
                postDelay = 1.0f;
                WT.scriptDelay = 1.0f;
                isDontOpenNameColor = true;
                DeleteScriptText();
                //OffName();
                OffScriptBox();
                OffScrollCam();
                SM.JamesWicked();
            }
        }
        if (Equals(index, 52))
        {
            checkSound = true;

            if (checkSound)
            {
                OffMiMi();
                //GRC.isCCOnOff = true; GLC.isCCOnOff = true;
                isDontOpenNameColor = false;
                // 플레이어 이동
                PlayerObj.transform.localPosition = PlayerPos[3].localPosition;
                PlayerObj.transform.localRotation = PlayerPos[3].localRotation;
                ScriptText.text = "";
                if (isCCOnOff) { OnScriptBox(); } else { OffScriptBox(); }
                SteamVR_Fade.View(Color.clear, 1.0f);
                // 코루틴 딜레이 설정
                postDelay = 6.5f;
                WT.scriptDelay = 6.5f;
                DeleteScriptText();
                if (isCCOnOff) OnJamesName();
                // "야레야레.. 마계의 진미를 받아들이기에 「지.금.의.」마왕님은 약하군...!"
                if (isVoiceOnOff) VoicePlay(36);
            }
        }
        if (Equals(index, 53))
        {
            checkSound = true;

            if (checkSound)
            {
                // 코루틴 딜레이 설정
                postDelay = 5.0f;
                WT.scriptDelay = 5.0f;
                DeleteScriptText();
                if (isCCOnOff) OnJamesName();
                // "내 정성으로 『훈.련』시켜야겠어... 킄킄...!"
                if (isVoiceOnOff) VoicePlay(37);
            }
        }
        if (Equals(index, 54))
        {
            checkSound = true;

            if (checkSound)
            {
                //OffName();
                OffScriptBox();
                postDelay = 0.4f;
                WT.scriptDelay = 0.4f;
                DeleteScriptText();
                isDontOpenNameColor = true;

                if (!checkNotRepeatFade5)
                {
                    checkNotRepeatFade5 = true;

                    SetColorBlack();

                    // 페이드 큐브 켜기
                    //CubeOn();
                    // 페이드 시작
                    //StartCoroutine(RC.Fade(RC.WhiteImage, 0f, 2.2f, RC.CM));
                }
            }
        }
        if (Equals(index, 55))
        {
            checkSound = true;

            if (checkSound)
            {
                isDontOpenNameColor = false;
                //isDontScriptImage = false;
                // 엔딩 일러스트
                OffScrollCam();
                //OnScrollCam();
                PlayerObj.transform.localPosition = ED2MovePos.transform.localPosition;
                PlayerObj.transform.localRotation = ED2MovePos.transform.localRotation;
                OnED2Illust();
                // 코루틴 딜레이 설정
                postDelay = 9.0f;
                WT.scriptDelay = 9.0f;
                DeleteScriptText();
                OffName();
                if (isCCOnOff) OnScriptBox(); else { OffScriptBox(); }
                SfxPlay(88);
                // 그 뒤, 제임스의 교육이라는 이름의 폭력에 시달리던 주인공은 화가 난 나머지 경찰서에 신고를 했고, 평범한 일상을 되찾게 되었다.
                if (isVoiceOnOff) VoicePlay(67);
            }
        }
        if (Equals(index, 56))
        {
            checkSound = true;

            if (checkSound)
            {
                // 코루틴 딜레이 설정
                postDelay = 9.5f;
                WT.scriptDelay = 9.5f;
                DeleteScriptText();
                OffName();
                if (isCCOnOff) OnScriptBox(); else { OffScriptBox(); }
                // 어둡고 외로운 감옥 안에서 제임스는 자신의 일방적인 감정과 행동이 상대에게 폭력으로 와닿을 수 있음을 납득하고 깊게 반성했다.
                if (isVoiceOnOff) VoicePlay(68);
            }
        }
        if (Equals(index, 57))
        {
            checkSound = true;

            if (checkSound)
            {
                isDontOpenNameColor = true;
                postDelay = 2.5f;
                WT.scriptDelay = 2.5f;
                //OffName();
                OffScriptBox();
            }
        }
        if (Equals(index, 58))
        {
            checkSound = true;

            if (checkSound)
            {
                //OffName();
                OffScriptBox();
                BackTotheTitleScene();
                //Invoke("GameQuit", 5.0f);
            }
        }
        // =========================================
        if (Equals(index, 60))
        {
            checkSound = true;

            if (checkSound)
            {
                //OffName();
                OffScriptBox();
                OffSpoon();
                isDontOpenNameColor = true;
                isSelectioning = true;
                checkSound = false;
            }

        }
        if (Equals(index, 61))
        {
            checkSound = true;

            if (checkSound)
            {
                GRC.isCCOnOff = true; GLC.isCCOnOff = true;
                LSMR.enabled = false;
                LCCC.enabled = false;
                isSelect = false;
                // 선택지 선택 사운드 실행
                SfxPlay(91);
                WT.ScriptText.text = "";
                isDontOpenNameColor = false;
                isDontScriptImage = true;
                // 코루틴 딜레이 설정
                postDelay = 4.0f;
                WT.scriptDelay = 4.0f;
                OffExplanationImage();
                DeleteScriptText();
                if (isCCOnOff) OnScriptBox(); else { OffScriptBox(); }
                if (isCCOnOff) OnPlayerName();
                // "아, 나 지금 속이 안 좋아서.."
                if (isVoiceOnOff) VoicePlay(38);
            }
        }
        if (Equals(index, 62))
        {
            checkSound = true;

            if (checkSound)
            {
                if (!checkNotRepeatFade6)
                {
                    checkNotRepeatFade6 = true;
                    RC.WhiteImage.color = RC.Alpha0;
                    // 페이드 큐브 켜기
                    //CubeOn();
                    // 페이드 시작
                    //StartCoroutine(RC.Fade(RC.WhiteImage, 0f, 2.2f, RC.CM));
                    SteamVR_Fade.View(Color.black, 0.03f);
                    //GRC.isCCOnOff = false; GLC.isCCOnOff = false;

                    Invoke("DelayFadeIn", 1.85f);
                    //Invoke("CubeOff", 1.9f);
                    //GRC.isCCOnOff = false; GLC.isCCOnOff = false;
                }

                isSelectioning = false;
                OffScriptBox();
                //OffName();
                SM.JamesFrusrated();
                SM.JamesFrusratedTell();
                OffSpoon();
                // 코루틴 딜레이 설정
                postDelay = 4.6f + 2.8f;
                WT.scriptDelay = 4.6f;
                Invoke("DeleteScriptText", 2.8f);

                // "마왕님, 지금 제 음식을 보고 속이 안좋다고 생각하셨죠?"
                //if(isVoiceOnOff) VoicePlay(39);
                Invoke("DelayVoicePlay39", 2.8f);
            }
        }
        #endregion
        #region < 이벤트 51 ~ 60 >
        if (Equals(index, 63))
        {
            checkSound = true;

            if (checkSound)
            {
                if (isCCOnOff) OnScriptBox(); else { OffScriptBox(); }
                // 코루틴 딜레이 설정
                postDelay = 5.0f;
                WT.scriptDelay = 5.0f;
                DeleteScriptText();
                if (isCCOnOff) OnJamesName();
                SM.JamesFrusratedTell2();
                // "정말 너무해요..! 제임스 상처받아버렸어요!"
                if (isVoiceOnOff) VoicePlay(40);
            }
        }
        if (Equals(index, 64))
        {
            checkSound = true;

            if (checkSound)
            {
                if (isCCOnOff) OnScriptBox(); else { OffScriptBox(); }
                // 코루틴 딜레이 설정
                postDelay = 5.5f;
                WT.scriptDelay = 5.5f;
                DeleteScriptText();
                if (isCCOnOff) OnPlayerName();
                SM.JamesFrusratedIdle();
                // "으..응..? 절대 그렇게 생각하지 않아...! 참 맛있어 보이는걸...?"
                if (isVoiceOnOff) VoicePlay(41);
            }
        }
        if (Equals(index, 65))
        {
            checkSound = true;

            if (checkSound)
            {
                if (isCCOnOff) OnScriptBox(); else { OffScriptBox(); }
                // 코루틴 딜레이 설정
                postDelay = 5.2f;
                WT.scriptDelay = 5.2f;
                DeleteScriptText();
                OffName();
                // 쓰담 스테이지 리셋
                stage = false;
                stageManger.trigger.isEvent = false;
                if (!isCheckStartRub)
                {
                    SM.PlayerFrustratedStart();
                    isCheckStartRub = true;
                }
                SM.JamesSkinshipFrustrated1();
                // 그러나 제임스는 이미 당신의 무성의한 대답에 상처받았다구~
                if (isVoiceOnOff) VoicePlay(77);
            }
        }
        if (Equals(index, 66))
        {
            checkSound = true;
            //OffName();
            OffScriptBox();
            // 스담스담
            if (checkSound)
            {
                postDelay = 0.1f;
                WT.scriptDelay = 0.1f;
                isDontOpenNameColor = true;
                TouchSphere.GetComponent<MeshRenderer>().enabled = true;

                if (!stage)
                {
                    stageManger.StartTouchEventInterface(0);//.isEvent = true;
                    stage = true;
                    ChangeSpherePos(1);
                    ChangeTouchUICanvasPos(1);
                    checkNotRepeatTrue2 = false;
                }
            }
        }
        if (Equals(index, 67))
        {
            checkSound = true;

            if (checkSound)
            {
                //OffName();
                OffScriptBox();
            }
        }
        if (Equals(index, 68))
        {
            checkSound = true;

            if (checkSound)
            {
                if (isCCOnOff) OnScriptBox(); else { OffScriptBox(); }
                isDontOpenNameColor = false;
                // 코루틴 딜레이 설정
                postDelay = 6.0f;
                WT.scriptDelay = 6.0f;
                DeleteScriptText();
                if (isCCOnOff) OnJamesName();
                SM.JamesSkinshipFrustrated3();
                SM.JamesArmIdle();
                SM.JamesArmTell();
                OffSpoon();
                // "흐흥... 쓰다듬 받은 것 정도로 딱히 기분이 좋아진 건 아니니까요!"
                if (isVoiceOnOff) VoicePlay(43);
                PlayHeartEffect();
            }
        }
        if (Equals(index, 69))
        {
            checkSound = true;

            if (checkSound)
            {
                if (isCCOnOff) OnScriptBox(); else { OffScriptBox(); }
                // 코루틴 딜레이 설정
                postDelay = 7.8f;
                WT.scriptDelay = 7.8f;
                DeleteScriptText();
                if (isCCOnOff) OnPlayerName();
                SM.JamesArmIdle2();
                OffSpoon();
                // "음, 그렇구나. 어쨌든 나는 절대 네 음식이 위협적이라거나 끔찍해 보인다든가 하는 게 아니고,
                // 정말 속이 안 좋아서 그런 거니까."
                if (isVoiceOnOff) VoicePlay(44);
                StopHeartEffect();
            }
        }
        if (Equals(index, 70))
        {
            checkSound = true;

            if (checkSound)
            {
                if (isCCOnOff) OnScriptBox(); else { OffScriptBox(); }
                // 코루틴 딜레이 설정
                postDelay = 5.2f;
                WT.scriptDelay = 5.2f;
                DeleteScriptText();
                if (isCCOnOff) OnPlayerName();
                // "알겠지? 그나저나 마계 기능장이라니, 대단한걸? 시집가면 이쁨받겠네!"
                if (isVoiceOnOff) VoicePlay(45);
            }
        }
        #region < Deleted >
        //if (Equals(index, 72))
        //{
        //checkSound = true;

        //if (checkSound)
        //{
        //    if(isCCOnOff) OnScriptBox();
        //    // 코루틴 딜레이 설정
        //    postDelay = 8.0f;
        //    WT.scriptDelay = 8.0f;
        //    DeleteScriptText();
        //    if(isCCOnOff) OnPlayerName();
        //    // "으응? 맞다, 미안~ 제임스는 무성이지. 무성이면 김무성을 지지하나? 킄킄"
        //    if(isVoiceOnOff) VoicePlay(47);

        //}
        //checkSound = true;

        //if (checkSound)
        //{
        //// 대사 삭제됨
        //scriptIndex++;
        //Debug.Log("index : " + scriptIndex);
        //    }
        //}
        //if (Equals(index, 73))
        //{
        //    //checkSound = true;

        //    //if (checkSound)
        //    //{
        //    //    if(isCCOnOff) OnScriptBox();
        //    //    // 코루틴 딜레이 설정
        //    //    postDelay = 5.0f;
        //    //    WT.scriptDelay = 5.0f;
        //    //    DeleteScriptText();
        //    //    OffName();
        //    //    // 눈치 없는 너의 농담에 제임스는 화가 났다!
        //    //    if(isVoiceOnOff) VoicePlay(48);
        //    //}
        //    checkSound = true;

        //    if (checkSound)
        //    {
        //        // 대사 삭제됨
        //        scriptIndex++;
        //        //Debug.Log("index : " + scriptIndex);
        //    }
        //}
        #endregion
        #region < 이벤트 61 ~ 70 >
        #endregion

        if (Equals(index, 71))
        {
            checkSound = true;

            if (checkSound)
            {
                if (isCCOnOff) OnScriptBox(); else { OffScriptBox(); }
                // 코루틴 딜레이 설정
                postDelay = 3.5f;
                WT.scriptDelay = 3.5f;
                DeleteScriptText();
                if (isCCOnOff) OnJamesName();
                //// 브금 깔림
                //BGMs[0].clip = BGMFiles[2];
                //BGMs[0].enabled = true;
                // 효과음 깔림
                SfxPlay(96);
                SM.JamesAngry();
                DSV = true;
                // "(나레이션톤) 시집가면 이쁨 받는다고?"
                if (isVoiceOnOff) VoicePlay(46);
                PlayAuraEffect();
            }
        }
        if (Equals(index, 72))
        {
            checkSound = true;

            if (checkSound)
            {
                if (isCCOnOff) OnScriptBox(); else { OffScriptBox(); }
                // 코루틴 딜레이 설정
                postDelay = 5.7f;
                WT.scriptDelay = 5.7f;
                DeleteScriptText();
                if (isCCOnOff) OnJamesName();
                SM.JamesAngryTell1();
                // "요리를 잘하면 잘하는 거지, 요리와 시집에 대체 어느 연관성이 있는 거죠?"
                if (isVoiceOnOff) VoicePlay(49);
            }
        }
        if (Equals(index, 73))
        {
            checkSound = true;

            if (checkSound)
            {
                if (isCCOnOff) OnScriptBox(); else { OffScriptBox(); }
                // 코루틴 딜레이 설정
                postDelay = 8.0f;
                WT.scriptDelay = 8.0f;
                DeleteScriptText();
                if (isCCOnOff) OnJamesName();
                // "요리를 잘하니 장가가면 이쁨받겠다고 어디 말씀해보시죠? 물론 전『무.성』입니다만?"
                if (isVoiceOnOff) VoicePlay(50);
            }
        }
        if (Equals(index, 74))
        {
            checkSound = true;

            if (checkSound)
            {
                if (isCCOnOff) OnScriptBox(); else { OffScriptBox(); }
                // 코루틴 딜레이 설정
                postDelay = 14.0f;
                WT.scriptDelay = 14.0f;
                DeleteScriptText();
                if (isCCOnOff) OnJamesNameLarge(); else if (!isCCOnOff) { OffScriptBox(); }
                SM.JamesAngryTell2();
                // 효과음 깔림
                SfxPlay(96);
                // "방금 마왕님의 발언은 21세기 첨단 유비쿼터스 사회의 고등 교육 과정을 거치는 현대인의 입에선 나올 수 없는
                // 구시대적 성차별 발언으로, 제임스의 존엄성은 해쳐졌다고 보는 부분입니다만?"
                if (isVoiceOnOff) VoicePlay(51);
            }
        }
        if (Equals(index, 75))
        {
            checkSound = true;

            if (checkSound)
            {
                if (isCCOnOff) OnScriptBox(); else { OffScriptBox(); }
                // 코루틴 딜레이 설정
                postDelay = 4.0f;
                WT.scriptDelay = 4.0f;
                DeleteScriptText();
                OffName();
                SM.JamesAngryIdle();
                // 효과음 깔림
                SfxPlay(96);
                // 이만 스피드웨건은 쿨하게 사라져주지.
                if (isVoiceOnOff) VoicePlay(72);
            }
        }
        if (Equals(index, 76))
        {
            checkSound = true;

            if (checkSound)
            {
                if (isCCOnOff) OnScriptBox(); else { OffScriptBox(); }
                // 코루틴 딜레이 설정
                postDelay = 4.0f;
                WT.scriptDelay = 4.0f;
                DeleteScriptText();
                if (isCCOnOff) OnPlayerName();
                // 우우웅 소리 중단
                SM.SfxSource.enabled = false;
                // "아, 내 생각이 짧았어! 나를 용서해줘!"
                if (isVoiceOnOff) VoicePlay(53);
            }
        }
        if (Equals(index, 77))
        {
            checkSound = true;

            if (checkSound)
            {
                // 코루틴 딜레이 설정
                postDelay = 11.0f;
                WT.scriptDelay = 11.0f;
                DeleteScriptText();
                if (isCCOnOff) OnScriptBox(); else { OffScriptBox(); }
                if (isCCOnOff) OnJamesName();
                // 포카리 브금 깔림
                //BGMs[0].enabled = false;
                //BGMs[0].clip = BGMFiles[1];
                //BGMs[0].enabled = true;                

                SM.JamesForgive1();
                SM.JamesForgive2();
                // "아니에요, 마왕님. 사람이 잘못할 수도 있죠! 제임스는 마왕님의 진심을 알아요~"
                // "스프는 냉동실에 넣어놓을 테니 배고프면 꺼내드세요!"
                if (isVoiceOnOff) VoicePlay(54);
                StopHeartEffect();
                StopAuraEffect();
                SakuraEffect.transform.localPosition = SakuraPos [1].localPosition;
                PlaySakuraEffect();
            }
        }
        if (Equals(index, 78))
        {
            checkSound = true;

            if (checkSound)
            {
                //OffName();
                OffScriptBox();
                postDelay = 3.0f;
                WT.scriptDelay = 3.0f;
                isDontOpenNameColor = true;

                if (!checkNotRepeatFade7)
                {
                    checkNotRepeatFade7 = true;

                    RC.WhiteImage.color = RC.ColorBlack;
                    // 페이드 큐브 켜기
                    //CubeOn();
                    // 페이드 시작
                    //StartCoroutine(RC.Fade(RC.WhiteImage, 0f, 2.2f, RC.CM));
                    SteamVR_Fade.View(Color.black, 1.0f);
                    Invoke("StopSakuraEffect", 1.0f);
                }
            }
        }
        if (Equals(index, 79))
        {
            checkSound = true;

            if (checkSound)
            {
                if (isCCOnOff) { OnScriptBox(); OnNarrationLarge(); } /*else { OffScriptBox(); }*/
                SteamVR_Fade.View(Color.clear, 1.0f);
                isDontOpenNameColor = false;
                //PlayerObj.transform.SetParent(ScrollCamObj.transform, true);
                //PlayerObj.transform.localPosition = ScrollCamObj.transform.localPosition;
                //PlayerObj.transform.localRotation = ScrollCamObj.transform.localRotation;
                PlayerObj.transform.localPosition = ED2MovePos.transform.localPosition;
                PlayerObj.transform.localRotation = ED2MovePos.transform.localRotation;

                // 코루틴 딜레이 설정
                postDelay = 18.0f;
                WT.scriptDelay = 18.0f;
                DeleteScriptText();
                RC.WhiteImage.color = RC.Alpha0;
                SakuraEffect.transform.localPosition = SakuraPos [0].localPosition;
                OffScrollCam();
                //OnScrollCam();
                OnED1Illust();
                OnNarrationLarge();
                //  그 뒤, 제임스의 꾸준한 참교육으로 성별에 대한 편견을 벗어던지고 진정한 마왕으로 거듭난 주인공은 이때까지 자신을 보필해준 제임스와 종족의 벽을 뛰어넘고 아름다운 사랑을 하며, 현명하고 슬기롭게 마계를 다스렸다.
                if (isVoiceOnOff) VoicePlay(82);
                Invoke("OffScriptBox", 18.9f);
            }
        }
        if (Equals(index, 80))
        {
            checkSound = true;

            if (checkSound)
            {
                isDontOpenNameColor = true;
                //OffScrollCam();
                //OnScrollCam();
                //OnED1Illust();
                //OffName();
                OffScriptBox();
                BackTotheTitleScene();
                //isDontOpenNameColor = false;
                //Invoke("GameQuit", 5.0f);
            }
        }
        if (Equals(index, 444))
        {
            checkSound = true;

            if (checkSound)
            {
                SM.SfxSource2.enabled = false;
                //OffScrollCam();
                OnScrollCam();
                CS.isShaking = true;
                OnGameOverIllust();
                //OffName();
                OffScriptBox();
                BackTotheTitleScene(4.0f);
                isDontOpenNameColor = true;
                SteamVR_Fade.View(Color.clear, 0.1f);
                StopAllCoroutines();
                //Invoke("GameQuit", 5.0f);
            }
        }

        #region < 음란한 숫캐 >
        //if (Equals(index, 72))
        //{
        //    checkSound = true;

        //    if (checkSound)
        //    {
        //        if(isCCOnOff) OnScriptBox();
        //        // 코루틴 딜레이 설정
        //        postDelay = 4.0f;
        //        WT.scriptDelay = 4.0f;
        //        DeleteScriptText();
        //        if(isCCOnOff) OnJamesName();
        //        // "하하, 울부짖어라. 이 음란한 숫캐야!"
        //        if(isVoiceOnOff) VoicePlay(74);
        //    }
        //}
        //if (Equals(index, 73))
        //{
        //    checkSound = true;

        //    if (checkSound)
        //    {
        //        if(isCCOnOff) OnScriptBox();
        //        // 코루틴 딜레이 설정
        //        postDelay = 4.0f;
        //        WT.scriptDelay = 4.0f;
        //        DeleteScriptText();
        //        if(isCCOnOff) OnPlayerName();
        //        // "흐읏.. 내 존엄성을 해치는 발언으로 가버렷!"
        //        if(isVoiceOnOff) VoicePlay(75);
        //    }
        //}
        //if (Equals(index, 74))
        //{
        //    checkSound = true;

        //    if (checkSound)
        //    {
        //        if(isCCOnOff) OnScriptBox();
        //        // 코루틴 딜레이 설정
        //        postDelay = 11.5f;
        //        WT.scriptDelay = 11.5f;
        //        DeleteScriptText();
        //        OffName();
        //        // "그 뒤... 제임스의 마계 음식 조교와 훈육으로, 주인공은 자신의 피 속에 내재되어 있었던 마왕으로서의 정체성에 서서히 눈을 뜨게 되었다."
        //        if(isVoiceOnOff) VoicePlay(78);
        //    }
        //}
        //if (Equals(index, 75))
        //{
        //    checkSound = true;

        //    if (checkSound)
        //    {
        //        if(isCCOnOff) OnScriptBox();
        //        // 코루틴 딜레이 설정
        //        postDelay = 15.5f;
        //        WT.scriptDelay = 15.5f;
        //        DeleteScriptText();
        //        OffName();
        //        // 그렇게 주인공은 제임스와 함께 인간계에 소박한 마계를 꾸몄다. 비록 마계의 마왕은 아니었지만, 그들만의 작은 성에서 둘은 서로 사랑했고, 행복했다... 아, 근데 커플이라고?
        //        if(isVoiceOnOff) VoicePlay(79);
        //    }
        //}
        //if (Equals(index, 76))
        //{
        //    checkSound = true;

        //    if (checkSound)
        //    {
        //        if(isCCOnOff) OnScriptBox();
        //        // 코루틴 딜레이 설정
        //        postDelay = 5.0f;
        //        WT.scriptDelay = 5.0f;
        //        DeleteScriptText();
        //        if(isCCOnOff) OnJamesName();
        //        // "으흐흐킄킄... 이제 마왕님은 제임스만의 것이에요!"
        //        if(isVoiceOnOff) VoicePlay(76);
        //    }
        //}
        #endregion
        #endregion
    }

    void StopScripting(int index)
    {
        index = scriptIndex;

        #region < 스탑스탑 >
        if (Equals(index, 21))
        {
            if (!checkNOtRepeatStop1)
            {
                isSelectioning = true;
                checkNOtRepeatStop1 = true;
                check = false;
                StopCoroutine("PrintStart");
                Invoke("OnSelects", 4.5f);
                Choices [0].SetActive(true);
                Choices [1].SetActive(true);
                ChoiceText [0].text = "제임스는 남자야?";
                ChoiceText [1].text = "제임스는 여자야?";
                //Debug.Log("스토리 진행 멈춤.");
                //Debug.Log("선택지를 고르세요.");
            }
        }

        if (Equals(index, 40))
        {
            if (!checkNOtRepeatStop2)
            {
                StopScript(checkNOtRepeatStop2);

                //Debug.Log("스토리 진행 멈춤.");
                //Debug.Log("쓰담쓰담 타임");
            }
        }

        if (Equals(index, 43))
        {
            if (!checkNOtRepeatStop3)
            {
                StopScript(checkNOtRepeatStop3);

                //Debug.Log("스토리 진행 멈춤.");
                //Debug.Log("선택지를 고르세요");
            }
        }

        if (Equals(index, 46))
        {
            if (!checkNOtRepeatStop4)
            {
                StopScript(checkNOtRepeatStop4);
                //Debug.Log("스토리 진행 멈춤.");
                //Debug.Log("미미");
            }
        }
        if (Equals(index, 58))
        {
            if (!checkNOtRepeatStop5)
            {
                StopScript(checkNOtRepeatStop5);
                StopAllCoroutines();
                //Debug.Log("챕터 1 엔딩 2");
            }
        }
        if (Equals(index, 66))
        {
            if (!checkNOtRepeatStop6)
            {
                StopScript(checkNOtRepeatStop6);
                //Debug.Log("스토리 진행 멈춤.");
                //Debug.Log("NO - 쓰다듬 2번째");
            }
        }
        if (Equals(index, 80))
        {
            if (!checkNOtRepeatStop7)
            {
                StopScript(checkNOtRepeatStop7);
                StopAllCoroutines();
                //Debug.Log("챕터 1 엔딩 1");
            }
        }
        #endregion
    }
    #endregion


    #region < 사운드 >
    void VoicePlay(int index)
    {
        SM.VoiceSource.enabled = false;
        SM.VoiceSource.clip = SM.VoiceDialogs [index];
        SM.VoiceSource.enabled = true;
        checkSound = false;
    }
    public void SfxPlay(int index)
    {
        SM.SfxSource.enabled = false;
        SM.SfxSource.clip = SM.VoiceDialogs [index];
        SM.SfxSource.enabled = true;
        checkSound2 = false;
    }
    #endregion

    #region < 이펙트 >
    void PlaySakuraEffect()
    {
        SakuraEffect.SetActive(true);
    }
    void StopSakuraEffect()
    {
        SakuraEffect.SetActive(false);
    }
    void PlayAuraEffect()
    {
        AuraEffect.SetActive(true);
    }
    void StopAuraEffect()
    {
        AuraEffect.SetActive(false);
    }
    void PlayHeartEffect()
    {
        HeartEffect.SetActive(true);
    }
    void StopHeartEffect()
    {
        HeartEffect.SetActive(false);
    }
    #endregion

    #region < 기타 >
    void Delaycheck()
    {
        check = true;
    }
    void MoveChar1()
    {
        jamesModel.transform.position = Vector3.Lerp(jamesModel.transform.position, GameObject.FindGameObjectWithTag("MovePos1").transform.position, smoothing1);
        smoothing1 += 0.5f * Time.deltaTime;
        if (smoothing1 >= 1.0f)
            move1 = false;
    }
    void MoveChar2()
    {
        jamesModel.transform.position = Vector3.Lerp(jamesModel.transform.position, GameObject.FindGameObjectWithTag("MovePos2").transform.position, smoothing2);
        smoothing2 += 3f * Time.deltaTime;
        if (smoothing2 >= 1.0f)
            move2 = false;
    }
    void MoveChar3()
    {
        jamesModel.transform.position = Vector3.Lerp(jamesModel.transform.position, GameObject.FindGameObjectWithTag("MovePos3").transform.position, smoothing3);
        smoothing3 += 0.018f * Time.deltaTime;
        if (smoothing3 >= 0.058f)
            move3 = false;
    }
    void MoveMiMi()
    {
        MiMi.transform.position = Vector3.Lerp(MiMi.transform.position, GameObject.FindGameObjectWithTag("MiMiPos2").transform.position, smoothing4);
        smoothing4 += 0.1f * Time.deltaTime;
        if (smoothing4 >= 0.1f)
            move4 = false;
    }
    void OnED1Illust()
    {
        ED1Image.SetActive(true);
    }
    void OffED1Illust()
    {
        ED1Image.SetActive(false);
    }
    void OnED2Illust()
    {
        ED2Image.SetActive(true);
    }
    void OffED2Illust()
    {
        ED2Image.SetActive(false);
    }
    void OnGameOverIllust()
    {
        GameOverImage.SetActive(true);
    }
    void OffGameOverIllust()
    {
        GameOverImage.SetActive(false);
    }
    void OnScrollCam()
    {
        //ScrollCam.enabled = true;
        PlayerObj.transform.SetParent(ScrollCamObj.transform, true);
        //PlayerObj.transform.localPosition = ScrollCamObj.transform.localPosition;
        //PlayerObj.transform.localRotation = ScrollCamObj.transform.localRotation;
    }
    void OffScrollCam()
    {
        ScrollCam.enabled = false;
    }
    void DelayMove2Flag()
    {
        move2 = true;
    }
    void DelayWhiningStart()
    {
        SM.JamesWhiningStart();
    }
    void DelayWhiningEnd()
    {
        SM.JamesWhiningEnd();
    }
    void testDelay()
    {
        WT.ScriptText.text = "";
    }
    void SetColorAlpha0()
    {
        RC.WhiteImage.color = RC.Alpha0;
        RC.CM.SetColor("_Color", RC.Alpha0);
    }
    void SetColorAlpha100()
    {
        RC.WhiteImage.color = RC.Alpha100;
        RC.CM.SetColor("_Color", RC.Alpha100);
    }
    void SetColorBlack()
    {
        RC.WhiteImage.color = RC.ColorBlack;
        RC.CM.SetColor("_Color", RC.ColorBlack);
    }
    void SetColorRed()
    {
        RC.WhiteImage.color = RC.ColorRed;
        RC.CM.SetColor("_Color", RC.ColorRed);
    }
    void ChangeSpherePos(int index)
    {
        TouchSphere.transform.position = HandPos [index].transform.position;
        TouchSphere.transform.localScale = HandPos [index].transform.localScale;
    }
    void ChangeTouchUICanvasPos(int index)
    {
        stageManger.trigger.TouchUI.GetComponent<RectTransform>().position = HandCanvasPos [index].position;
        //stageManger.trigger.TouchUI.GetComponent<RectTransform>().localPosition = HandCanvasPos[index].localPosition;
    }
    void RotateObj(GameObject obj, Vector3 vector)
    {
        obj.transform.Rotate(vector);
    }
    void CubeOn()
    {
        for (int i = 0; i < 3; i++)
            RC.FadeCube [i].SetActive(true);
    }
    void CubeOff()
    {
        for (int i = 0; i < 3; i++)
            RC.FadeCube [i].SetActive(false);
    }
    void DelayVoicePlay39()
    {
        if (isCCOnOff) OnJamesName();
        if (isCCOnOff) OnScriptBox(); else { OffScriptBox(); }
        if (isVoiceOnOff) VoicePlay(39);
    }
    void IncreaseSoundVolume(AudioSource asource)
    {
        asource.volume += 0.011f;
        //if (asource.volume != 1)
        //asource.volume = 1f;
        //Debug.Log("볼륨 : " + asource.volume);
        if (asource.volume >= 0.9f)
        {
            asource.volume = 1;
            ISV = false;
        }
    }
    void DecreaseSoundVolume(AudioSource asource)
    {
        asource.volume -= 0.011f;
        //if (asource.volume != 0)
        //asource.volume = 0f;
        //Debug.Log("볼륨 : " + asource.volume);
        if (asource.volume <= 0)
        {
            asource.volume = 0;
            DSV = false;
        }
    }
    #endregion

    #region < 이름 >
    void OffName()
    {
        NameColorImage.sprite = ColorSprite [4];
        NameColorImage.enabled = false;
        NameText.text = "";
    }
    void OnNarrationLarge()
    {
        //ScriptImage.sprite = ColorSprite[3];
        //NameColorImage.enabled = false;
        NameColorImage.sprite = ColorSprite [3];
        NameColorImage.enabled = true;
        NameText.text = "";
    }
    void OnJamesName()
    {
        NameColorImage.sprite = ColorSprite [1];
        if (isCCOnOff)
            NameColorImage.enabled = true;
        else if (!isCCOnOff)
            NameColorImage.enabled = false;
        NameText.text = "";
    }
    void OnJamesNameLarge()
    {
        NameColorImage.sprite = ColorSprite [2];
        if (isCCOnOff)
            NameColorImage.enabled = true;
        else if (!isCCOnOff)
            NameColorImage.enabled = false;
        NameText.text = "";
    }
    void OnPlayerName()
    {
        if (isCCOnOff)
            NameColorImage.enabled = true;
        //ScriptImage.enabled = false;
        NameColorImage.sprite = ColorSprite [0];
        NameText.text = "";
    }
    void OnScriptBox()
    {
        //ScriptImage.enabled = true;
        NameColorImage.enabled = true;
        ScriptText.enabled = true;
        //Debug.Log("OnScriptBox");
        //Debug.Log("Index: " + scriptIndex);
    }
    void OffScriptBox()
    {
        //ScriptImage.enabled = false;
        NameColorImage.enabled = false;
        ScriptText.enabled = false;
        //Debug.Log("OffScriptBox");
    }
    void OnSpoon()
    {
        Spoon.SetActive(true);
    }
    void OffSpoon()
    {
        Spoon.SetActive(false);
    }
    public void OffTableSpoon()
    {
        Destroy(TableSpoon);
        OnSpoon();
    }
    public void DeleteScriptText()
    {
        StartCoroutine(WT.ScriptSliding(WT.ScriptText));
    }
    public void StartScript()
    {
        StartCoroutine("PrintStart");
    }
    void FastDelayFadeOut(Color color)
    {
        //StartCoroutine(RC.Fade(RC.WhiteImage, 0f, 1f, RC.CM));
        SteamVR_Fade.View(color, 0.1f);
    }
    void DelayFadeOut(Color color)
    {
        //StartCoroutine(RC.Fade(RC.WhiteImage, 0f, 1f, RC.CM));
        SteamVR_Fade.View(color, 1.0f);
    }
    void DelayFadeOutWhite()
    {
        //StartCoroutine(RC.Fade(RC.WhiteImage, 0f, 1f, RC.CM));
        SteamVR_Fade.View(Color.white, 1.0f);
    }
    void FastDelayFadeOutRed()
    {
        //StartCoroutine(RC.Fade(RC.WhiteImage, 0f, 1f, RC.CM));
        SteamVR_Fade.View(Color.red, 0.2f);
    }
    void DelayFadeIn()
    {
        //StartCoroutine(RC.Fade(RC.WhiteImage, 1f, 0f, RC.CM));
        SteamVR_Fade.View(Color.clear, 1.0f);
        //GRC.isCCOnOff = true; GLC.isCCOnOff = true;
    }
    void FastDelayFadeIn()
    {
        //StartCoroutine(RC.Fade(RC.WhiteImage, 1f, 0f, RC.CM));
        SteamVR_Fade.View(Color.clear, 0.1f);
    }
    void OnSelects()
    {
        RC.Selects [0].SetActive(true);
        RC.Selects [1].SetActive(true);
        isSelect = true;
        VRCLeft.gripButtonPressed = false;
        VRCRight.gripButtonPressed = false;
    }
    void OnSelects2()
    {
        RC.Selects [2].SetActive(true);
        RC.Selects [3].SetActive(true);
        LSMR.enabled = true;
        LCCC.enabled = true;
        isSelect = true;
        VRCLeft.gripButtonPressed = false;
        VRCRight.gripButtonPressed = false;
    }
    void StopScript(bool bflag)
    {
        bflag = true;
        check = false;
        OffScriptBox();
        OffName();
    }
    void DelaySfxPlay()
    {
        SM.SfxSource.enabled = true;
    }
    void MiMiStart()
    {
        move4 = true;
    }
    void OnMiMi()
    {
        MiMiSet.SetActive(true);
    }
    void OffMiMi()
    {
        MiMiSet.SetActive(false);
    }
    void OnExplanationImage()
    {
        ExplanationImage.enabled = true;
        ExplanationText.enabled = true;
    }
    void OffExplanationImage()
    {
        ExplanationImage.enabled = false;
        ExplanationText.enabled = false;
    }
    void ChangeNameRealTime()
    {
        // 플레이어
        if (Equals(scriptIndex, 4) || Equals(scriptIndex, 7) || Equals(scriptIndex, 9) || Equals(scriptIndex, 11) || Equals(scriptIndex, 23)
            || Equals(scriptIndex, 29) || Equals(scriptIndex, 35) || Equals(scriptIndex, 45) || Equals(scriptIndex, 50) || Equals(scriptIndex, 61)
            || Equals(scriptIndex, 64) || Equals(scriptIndex, 69) || Equals(scriptIndex, 70) || Equals(scriptIndex, 76))
            NameColorImage.sprite = ColorSprite [0];
        // 제임스
        if (Equals(scriptIndex, 5) || Equals(scriptIndex, 10) || Equals(scriptIndex, 12) || Equals(scriptIndex, 20) || Equals(scriptIndex, 25)
             || Equals(scriptIndex, 26) || Equals(scriptIndex, 33) || Equals(scriptIndex, 37) || Equals(scriptIndex, 42) || Equals(scriptIndex, 52)
             || Equals(scriptIndex, 53) || Equals(scriptIndex, 62) || Equals(scriptIndex, 63) || Equals(scriptIndex, 68) || Equals(scriptIndex, 71) || Equals(scriptIndex, 72)
             || Equals(scriptIndex, 73) || Equals(scriptIndex, 77))
            NameColorImage.sprite = ColorSprite [1];
        // 제임스 big
        if (Equals(scriptIndex, 27) || Equals(scriptIndex, 28) || Equals(scriptIndex, 74))
            NameColorImage.sprite = ColorSprite [2];
        // 내레이션 big
        if (Equals(scriptIndex, 79))
            NameColorImage.sprite = ColorSprite [3];
        // 내레이션
        if (Equals(scriptIndex, 1) || Equals(scriptIndex, 3) || Equals(scriptIndex, 6) || Equals(scriptIndex, 8) || Equals(scriptIndex, 18)
            || Equals(scriptIndex, 30) || Equals(scriptIndex, 31) || Equals(scriptIndex, 32) || Equals(scriptIndex, 38) || Equals(scriptIndex, 39)
            || Equals(scriptIndex, 40) || Equals(scriptIndex, 41) || Equals(scriptIndex, 43) || Equals(scriptIndex, 44) || Equals(scriptIndex, 46)
            || Equals(scriptIndex, 47) || Equals(scriptIndex, 48) || Equals(scriptIndex, 55) || Equals(scriptIndex, 56) || Equals(scriptIndex, 65) || Equals(scriptIndex, 75))
            NameColorImage.sprite = ColorSprite [4];
    }
    #region < Debug >
    //public void CCOnOffButtonOn()
    //{
    //    isCCOnOff = true;
    //    OnOffButtonImages[0].color = new Color32(255, 173, 173, 255);
    //    OnOffButtonImages[1].color = new Color32(255, 255, 255, 255);
    //}
    //public void CCOnOffButtonOff()
    //{
    //    isCCOnOff = false;
    //    OnOffButtonImages[0].color = new Color32(255, 255, 255, 255);
    //    OnOffButtonImages[1].color = new Color32(255, 173, 173, 255);
    //}
    //public void VoiceOnOffTestButtonOn()
    //{
    //    isVoiceOnOff = true;
    //    OnOffButtonImages[2].color = new Color32(255, 173, 173, 255);
    //    OnOffButtonImages[3].color = new Color32(255, 255, 255, 255);
    //}
    //public void VoiceOnOffTestButtonOff()
    //{
    //    isVoiceOnOff = false;
    //    OnOffButtonImages[2].color = new Color32(255, 255, 255, 255);
    //    OnOffButtonImages[3].color = new Color32(255, 173, 173, 255);
    //}
    void GameQuit()
    {
        Application.Quit();
    }
    void BackTotheTitleScene()
    {
        //AutoFade.LoadLevel(0, 4.0f, 4.0f, Color.black);
        SteamVR_Fade.View(Color.black, 2.0f);
        Invoke("BackToTitle", 2.0f);
    }
    void BackTotheTitleScene(float delayTotitle)
    {
        //AutoFade.LoadLevel(0, 4.0f, 4.0f, Color.black);
        SteamVR_Fade.View(Color.black, 2.0f);
        Invoke("BackToTitle", delayTotitle);
    }
    void BackToTitle()
    {
        SceneManager.LoadScene("Main");
        //SteamVR_Fade.View(Color.clear, 2.0f);
    }

    #endregion
    #endregion
}