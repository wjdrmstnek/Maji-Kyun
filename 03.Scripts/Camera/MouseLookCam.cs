using UnityEngine;

public class MouseLookCam : MonoBehaviour
{
    #region < 변수 >
    // 마우스 감도
    public float MouseSensitivity = 4.0f;
    // 마우스를 회전시켜줄 만큼의 값
    public float xRotation;
    public float yRotation;
    // 현재의 회전값
    public float curXrotation;
    public float curYrotation;
    // 축 별 회전 제한값
    public float xcurRotationUP = -40f;
    public float xcurRotationDOWN = 60f;
    public float ycurRotationLEFT = -3600f;
    public float ycurRotationRIGHT = 3600f;
    // X, Y축 ref값
    public float xRotationV;
    public float yRotationV;
    // 부드럽게 따라오는 값
    public float Smoothing = 0.1f;

    #region < 플래그 >
    // 초기 카메라 각도 플래그
    //bool bYSetting = true;
    //bool bYSetting180 = true;
    #endregion

    #endregion

    void Update()
    {
        GetMouseAxis();
        LimitCamRotation();
        SmoothRotation();
        UpdateCamRotation();
    }

    #region < 사용된 함수 부분 >
    void GetMouseAxis()
    {
        // 마우스 움직임을 인식해서 카메라를 움직여준다
        xRotation -= Input.GetAxis("Mouse Y") * MouseSensitivity;
        yRotation += Input.GetAxis("Mouse X") * MouseSensitivity;
    }

    void LimitCamRotation()
    {
        // 상하좌우 카메라 로테이션 한도를 둔다
        xRotation = Mathf.Clamp(xRotation, xcurRotationUP, xcurRotationDOWN);
        yRotation = Mathf.Clamp(yRotation, ycurRotationLEFT, ycurRotationRIGHT);
    }

    void SmoothRotation()
    {
        // 부드럽게 회전되도록 스무스 댐프를 사용한다
        curXrotation = Mathf.SmoothDamp(curXrotation, xRotation, ref xRotationV, Smoothing);
        curYrotation = Mathf.SmoothDamp(curYrotation, yRotation, ref yRotationV, Smoothing);
    }

    void UpdateCamRotation()
    {
        #region < 안 쓰는 부분 >
        //// 처음에 1번 앞면을 보게 해준다
        //if (bYSetting == true)
        //{
        //    if (bYSetting180 == true)
        //    {
        //        curYrotation = 180;
        //        bYSetting180 = false;
        //    }
        //    //Debug.Log("curYrotation : " + curYrotation);
        //    curYrotation = Mathf.Lerp(curYrotation, 0, 1.0f * Time.deltaTime);

        //    if (curYrotation < 1.0f)
        //        bYSetting = false;
        //}
        #endregion

        // 계산된 회전값을 플레이어 회전에 반영하는 부분
        transform.rotation = Quaternion.Euler(curXrotation, curYrotation, 0);
    }
    #endregion
}