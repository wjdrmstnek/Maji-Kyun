using UnityEngine;
using System.Collections;

public class FollowCam : MonoBehaviour
{
    #region < 오브젝트／컴포넌트 >
    #region < 오브젝트 >


    #endregion

    #region < 컴포넌트 >
    // 플레이어의 좌표계
    public Transform playerTr;

    #endregion
    #endregion

    #region < 변수 >
    // 부드럽게 따라오는 정도
    public float smoothing = 0.1f;
    // 플레이어와 카메라 간의 거리 조정
    public const float xDistance = 0.0f;
    public const float yDistance = 0.6f;
    public const float zDistance = 0.05f;

    #region < 방향 벡터 >
    // 타겟과 카메라 사이에 실제로 적용될 거리 벡터
    Vector3 Distance = new Vector3(xDistance, yDistance, zDistance);

    #endregion

    #region < 플래그 >
    // 플래그 모음
    public bool isSmoothing = true;
    public bool isJumping;
    // 플레이어 사망 여부
    // public ResetChecker rc;
    #endregion

    #endregion

    #region < 스크립트 >

    #endregion

    void LateUpdate()
    {
        #region < 아직 안 쓰는 부분 >
        //if (Input.GetButtonDown("Jump"))
        //    StartCoroutine("CheckJump");
        //if (!rc.isDead)
        //{
        //if (playerTr.position.x >= XLimitation.position.x)
        //{
        //if (!isJumping)
        //{
        #endregion

        // 타겟과 카메라가 딱 붙으면 멀리서 볼 수 없으므로 간격을 준다
        Vector3 movePos = playerTr.transform.position + Distance;
            // 부드럽게 이동하려는 경우
            if (isSmoothing)
                // Lerp를 사용하여 부드럽게 이동해준다
                transform.position = Vector3.Lerp(transform.position, movePos, smoothing);
            // 아닌 경우 곧바로 이동시켜준다
            else { transform.position = movePos; }
            //}
            //}
        //}
    }

    #region < 주석 정리 >
    //IEnumerator CheckJump()
    //{
    //    isJumping = true;
    //    yield return new WaitForSeconds(jumpTime);
    //    isJumping = false;
    //    StopCoroutine("CheckJump");
    //}
    #endregion
}