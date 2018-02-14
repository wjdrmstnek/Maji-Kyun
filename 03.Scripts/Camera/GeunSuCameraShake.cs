/* 근수 프레임워크 - 카메라 흔들기 */
using UnityEngine;
using System.Collections;
//using UnityStandardAssets.ImageEffects;

public class GeunSuCameraShake : MonoBehaviour
{
    // 카메라의 좌표
    public Transform CameraTr;
    // 카메라가 흔들릴 지속시간
    public float shakeDuration = 3.0f;
    // 카메라가 흔들릴 정도
    public float shakeAmount = 0.3f;
    // 감소시켜줄 요소
    [HideInInspector]
    public float Friction = 1.0f;
    //[HideInInspector]
    float decreaseShakeAmount = 0.5f;
    // 이동을 부드럽게 해주는 변수
    //float moveSmoothing = 0.0f;
    // 카메라의 로컬 좌표
    Vector3 CamLocalPos;
    // 카메라 흔들림 플래그
    public bool isShaking;

    #region < 안 써요 안 써 >
    // 카메라가 도착할 지점
    //Vector3 DestPos;
    //Vector3 DestYPos;
    // 카메라 이동 플래그
    //public bool isMovingZAxis;
    //bool isMovingYAxis;
    #endregion

    void Awake()
    {
        // 초기화 부분
        isShaking = false;
        CamLocalPos = CameraTr.localPosition;

        // 없을 경우 카메라의 좌표를 가져온다
        if (CameraTr == null)
            CameraTr = GetComponent<Transform>();

        #region < 안 써요 안 써 >
        //DestPos = new Vector3(CameraTr.localPosition.x, CameraTr.localPosition.y, CameraTr.localPosition.z + 30.0f);
        //if (transform.position.z == -12)
        //    DestPos = new Vector3(CameraTr.localPosition.x, CameraTr.localPosition.y, CameraTr.localPosition.z + 50.0f);
        #endregion
    }

    void Update()
    {
        // 카메라 좌표가 지정되어 있는 경우에만 작동
        if (CameraTr != null)
        {
            StartCoroutine("CameraShake");
        }
    }

    IEnumerator CameraShake()
    {
        // 카메라 좌표가 지정되어 있는 경우에만 작동
        if (CameraTr != null)
        {
            // 플래그가 true일 경우
            if (isShaking)
            {
                // 카메라가 흔들릴 시간이 0초 이상일 경우
                if (shakeDuration > 0)
                {
                    // 카메라의 위치를 충돌 구체 내부에서 랜덤하게 흔들릴 정도 만큼 이동시켜준다
                    CameraTr.localPosition = CamLocalPos + Random.insideUnitSphere * shakeAmount;
                    // 카메라가 흔들릴 시간을 일정 수치만큼 빼준다
                    shakeDuration -= Time.deltaTime * Friction;
                    // 카메라의 강도를 일정 수치만큼 빼준다
                    shakeAmount -= Time.deltaTime * decreaseShakeAmount;
                    if (shakeAmount < 0)
                        shakeAmount = 0;
                }
                else
                {
                    // 0으로 초기화
                    shakeDuration = 0.0f;
                    // 카메라를 초기 위치로 되돌려준다
                    CameraTr.localPosition = CamLocalPos;
                    isShaking = false;
                }
            }

            #region < 안 써요 안 써 >
            //if (!isShaking && CameraTr.gameObject.GetComponent<Camera>().fieldOfView >= 95.0f)
            //    isMovingZAxis = true;

            //if (!isShaking && isMovingZAxis)
            //    OnMovingZAxis();

            //if (!isShaking && isMovingYAxis)
            //    StartCoroutine("OnMovingYAxis");
            #endregion

        }
        // 프레임 별로 1회 실행
        yield return new WaitForEndOfFrame();
    }

    // 카메라 흔들림 플래그를 true로 만드는 함수
    // 버튼 등에 넣어서 사용
    public void OnCameraShakingFlag()
    {
        isShaking = true;
    }

    #region < 안 써요 안 써 >
    //void OnMovingZAxis()
    //{
    //    if (moveSmoothing >= 0.05f)
    //    {
    //        isMovingZAxis = false;
    //        isMovingYAxis = true;
    //        DestYPos = new Vector3(CameraTr.localPosition.x, CameraTr.localPosition.y + 80.0f, CameraTr.localPosition.z);
    //        moveSmoothing = 0.0f;
    //        return;
    //    }

    //    if (!isShaking && isMovingZAxis)
    //    {
    //        CameraTr.localPosition = Vector3.Lerp(CameraTr.localPosition, DestPos, moveSmoothing);
    //        moveSmoothing = moveSmoothing + 0.04f * Time.deltaTime;
    //        if (followCam.GetComponent<BloomAndFlares>().bloomIntensity <= 8.0f)
    //            followCam.GetComponent<BloomAndFlares>().bloomIntensity += 0.03f;
    //    }
    //}

    //IEnumerator OnMovingYAxis()
    //{
    //    yield return new WaitForSeconds(2.0f);

    //    if (!isShaking && isMovingYAxis)
    //    {
    //        CameraTr.localPosition = Vector3.Lerp(CameraTr.localPosition, DestYPos, moveSmoothing);
    //        moveSmoothing = moveSmoothing + 0.05f * Time.deltaTime;
    //    }
    //}
    #endregion
}