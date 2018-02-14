using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VR;
using System.Collections;

public class ScrollText : MonoBehaviour
{
    public float moveSpeed = 22.0f;
    float delay = 0.1f;
    float timer = 0.0f;
    public float scrollTime = 16.0f;
    Vector3 nextPos;
    public Camera ScrollCam;
    public Camera UICam;
    public Camera PlayerCam;
    public TextMesh ScrollTextMesh;
    public AudioSource ScrollBGM;
    bool debugCheck;
    bool fadeCheck;

    public ReadText RT;

    void Awake()
    {
        if (Equals(ScrollCam, false))
            ScrollCam = GameObject.FindGameObjectWithTag("ScrollCam").GetComponent<Camera>();
        if (Equals(UICam, false))
            UICam = GameObject.FindGameObjectWithTag("UICam").GetComponent<Camera>();
        if (Equals(PlayerCam, false))
            PlayerCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        if (Equals(ScrollTextMesh, false))
            ScrollTextMesh = GetComponent<TextMesh>();
        if (Equals(ScrollBGM, false))
            ScrollBGM = GameObject.FindGameObjectWithTag("ScrollTextMesh").GetComponent<AudioSource>();
        if (Equals(RT, null))
            RT = GameObject.FindGameObjectWithTag("ScriptText").GetComponent<ReadText>();

        //ScrollCam.enabled = true;
        //StartCoroutine("Scrolling");

        // 스타워즈 제거
        DebugInit();
        ScrollCam.enabled = false;
    }

    //// PC 디버그용 업데이트
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Q) && !debugCheck)
    //    {
    //        debugCheck = true;
    //        DebugInit();
    //    }

    //}

    //IEnumerator Scrolling()
    //{
    //    while (true)
    //    {
    //        // 현재 위치
    //        Vector3 textPos = transform.localPosition;
    //        nextPos = transform.TransformDirection(Vector3.up);
    //        //nextPos = new Vector3(0, 1, 1);
    //        // 실제 위치를 다음 방향 * 속도 * 델타 타임으로 계산
    //        textPos += nextPos * moveSpeed * Time.deltaTime;
    //        // 현재 위치 변경 
    //        transform.localPosition = textPos;
    //        timer += 0.1f;
    //        //Debug.Log("스크롤링스크롤링");
    //        yield return new WaitForSeconds(delay);

    //        if (timer > scrollTime)
    //        {
    //            StopCoroutine("Scrolling");
    //            StartCoroutine("FadeOut");
    //            break;
    //        }
    //    }
    //}

    //// 비용이 큰 Update를 사용하지 않고 코루틴을 이용하여 처리한다
    //IEnumerator FadeOut()
    //{
    //    // 임시 변수 초기화
    //    float curTime = 0f;
    //    // 페이드 지속 시간
    //    float durationTime = 0.5f;

    //    while (curTime < durationTime)
    //    {
    //        // 선형보간함수를 사용하여 값을 조정해준다
    //        float alphaVal = Mathf.Lerp(1f, 0f, curTime / durationTime);
    //        // 이미지의 RGB값은 그대로 두고 알파값만 조절해준다
    //        ScrollTextMesh.color = new Color(ScrollTextMesh.color.r, ScrollTextMesh.color.g, ScrollTextMesh.color.b, alphaVal);
    //        // 시간을 증가시켜주며 페이드 아웃 시켜준다
    //        curTime += Time.deltaTime;
    //        //Debug.Log("curTime : " + curTime);
    //        //Debug.Log("durationTime : " + durationTime);
    //        //Debug.Log("alphaVal : " + alphaVal);
    //        yield return null;
    //    }
    //    StopCoroutine("FadeOut");
    //    fadeCheck = false;
    //    UICam.enabled = true;
    //    ScrollCam.enabled = false;
    //    PlayerCam.enabled = true;
    //    PlayerCam.enabled = false;
    //    PlayerCam.enabled = true;
    //    timer = 0.0f;
    //    RT.check = true;
    //    gameObject.SetActive(false);
    //    yield break;
    //}

    void DebugInit()
    {
        StopCoroutine("FadeOut");
        fadeCheck = false;
        UICam.enabled = true;
        PlayerCam.enabled = true;
        PlayerCam.enabled = false;
        PlayerCam.enabled = true;
        ScrollCam.enabled = false;
        timer = 0.0f;
        RT.check = true;
        gameObject.SetActive(false);
    }
}