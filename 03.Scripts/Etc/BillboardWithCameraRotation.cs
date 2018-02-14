using UnityEngine;

public class BillboardWithCameraRotation : MonoBehaviour
{
    #region < 오브젝트／컴포넌트 >
    #region < 오브젝트 >
    // 따라갈 위치
    public Transform targetCam;

    #endregion

    #region < 컴포넌트 >

    #endregion
    #endregion

    #region < 변수 >
    // 기본 회전값
    Quaternion originRotation;

    #region < 방향 벡터 >

    #endregion

    #region < 플래그 >

    #endregion
    #endregion

    #region < 스크립트 >

    #endregion

    void Awake()
    {
        if (targetCam == null)
            targetCam = GameObject.FindGameObjectWithTag("MainCamera").transform;
        // 회전값 저장
        originRotation = transform.rotation;
    }

    void Update()
    {
        // 회전값에 카메라 회전값, 기존 회전값 넣기
        transform.rotation = targetCam.rotation * originRotation;
        transform.rotation = targetCam.localRotation * new Quaternion(1,0,0,0);
    }
}