/* 201513126 근수 프레임워크 이동 스크립트 */
using UnityEngine;
using System.Collections;

#region < 레거시 애니메이션을 사용할 경우 >
//// 클래스에 System.Serializable이라는 어트리뷰트(Attribute)를 명시해야 한다
//// Inspector 뷰에 노출됨
//[System.Serializable]
//public class Anim
//{
//    public AnimationClip Idle;
//    public AnimationClip Walk;
//    public AnimationClip Run;
//    public AnimationClip Jump;
//}
#endregion

// Rigidbody 컴포넌트가 없는 충돌 오브젝트를 움직이면 CPU에 부하가 생기므로
// Rigidbody 컴포넌트를 오브젝트에 추가하고 IsKinematics를 체크한다
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CharacterController))]
public class GeunSuMove : MonoBehaviour
{
    #region < 레거시 애니메이션을 사용할 경우>
    //// 인스펙터뷰에 표시할 애니메이션 클래스 변수
    //public Anim anim;
    //// 아래에 있는 3D 모델의 Animation 컴포넌트에 접근하기 위한 변수
    //public Animation _animation;
    #endregion

    // 이동 속도
    float moveSpeed = 0.5f;
    // 최종 이동 속도
    public float pMoveSpeed = 0.5f;
    //기본 이동 속도
    public float sMoveSpeed = 0.5f;
    //달리기 속도 배수
    public float runMultiplier = 3.0f;
    // 달리기 가속도
    public float accelerationSpeed = 0.01f;
    // 달리기 감속 변수
    public float runSmoothing = 0.01f;
    // 회전 속도
    public float rotateSpeed = 200.0f;
    // 부드러운 회전을 위한 변수
    public float rotateSmoothing = 5.0f;
    // 점프 속도
    public float jumpSpeed = 4.0f;
    public float pJumpSpeed = 0.0f;
    // 중력
    public float Gravity = 9.8f;
    // 현재 캐릭터의 좌표계
    Transform tr;
    // 현재 캐릭터의 회전계
    Quaternion qRotation;
    // 현재 캐릭터의 캐릭터 컨트롤러
    CharacterController controller;
    // Horizontal, Vertical Input 초기화
    float h = 0.0f;
    float v = 0.0f;
    // 이동 방향 초기화
    Vector3 moveDirection = Vector3.zero;
    // 아이들 상태로 되돌아가기까지 기다리는 변수
    public float ReturnIdleTime = 0.1f;

    // 가져올 카메라
    public Camera MainCamera;
    // 오디오 소스
    public AudioSource audioSource;
    // 오디오 클립
    public AudioClip[] audioClips;

    #region < 레이 캐스팅을 사용할 경우 >
    //// 레이를 쏠 위치
    //public Transform StartRayPos;
    //// 최대 레이 범위
    //public float maxRayDistance = 50.0f;
    #endregion

    // 애니메이션 플래그들
    // 아이들 플래그
    bool isIdle;
    // 달리기 플래그
    bool isRunning;
    // 점프 플래그
    bool isJumping;
    // 달리기 가속 플래그
    bool isAccelerateSlowly;
    // 달리기 감속 플래그
    bool isReduceSlowly;
    // 사용 플래그
    bool isOther;

    #region < 레이 캐스팅을 사용할 경우 >
    //// 레이 사용 플래그
    //bool isPickingUp;
    #endregion

    // 가져올 스크립트들


    void Awake()
    {
        #region < 레거시 애니메이션을 사용할 경우>
        //// 자신의 하위에 있는 Animation 컴포넌트를 찾아와 변수에 할당
        //_animation = GetComponentInChildren<Animation>();
        //// Animation 컴포넌트의 애니메이션 클립을 지정하고 실행
        //_animation.clip = anim.Idle;
        //_animation.Play();
        #endregion

        // 현재 캐릭터의 좌표계를 가져온다
        tr = GetComponent<Transform>();
        // 회전계를 초기화시켜준다
        qRotation = transform.rotation;
        // 현재 캐릭터의 캐릭터 컨트롤러를 가져온다
        controller = GetComponent<CharacterController>();

        // 플래그 초기화
        isIdle = true;
        isRunning = false;
        isJumping = false;
    }

    void Update()
    {
        GetInput();
        CalcDirection();
        ShowAnimation();
        Running();

        #region < 레이 캐스팅을 사용할 경우 >
        //Picking();
        #endregion

        if (isAccelerateSlowly)
            StartCoroutine("AccelerateMoveSpeed", 0.1f);

        if (isReduceSlowly)
            StartCoroutine("ReduceMoveSpeed", 0.1f);

        // 캐릭터 컨트롤러 콜라이더가 땅에 붙어 있을 때
        if (controller.isGrounded)
        {
            isIdle = true;
            isJumping = false;
            // 땅에 있을 때에는 점프 속도를 0으로 초기화시켜준다
            pJumpSpeed = 0.0f;

            Jumping();
            ShowAnimationOnGround();

            #region < 그때그때 사용할 나머지 부분 >
            // 구면 선형보간함수를 사용하여 부드럽게 저절로 벡터가 0인 평면을 바라보게 만든다
            //transform.rotation = Quaternion.Slerp( transform.rotation, Quaternion.LookRotation( ( Vector3.zero + transform.position ).normalized ), Time.deltaTime );
            // Y축 회전하여 시선이 마우스 포인터를 따라가게 해준다
            //transform.rotation = Quaternion.Euler( MainCamera.GetComponent<MouseLookCam>( ).curXrotation, MainCamera.GetComponent<MouseLookCam>( ).curYrotation, 0 );

            //if( Input.GetKey(KeyCode.Q)/* && Input.GetAxis("Horizontal") <= 0*/ ) {
            //    // Y축을 기준으로 입력값 * 회전 속도 만큼 회전시킨다.
            //    qRotation *= Quaternion.AngleAxis(Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime, Vector3.up);
            //    transform.rotation = qRotation;
            //    _animation.CrossFade(anim.Walk.name, 0.3f);
            //}
            //if( Input.GetKey(KeyCode.E) /*&& Input.GetAxis("Horizontal") >= 0*/ ) {
            //    // Y축을 기준으로 입력값 * 회전 속도 만큼 회전시킨다.
            //    qRotation *= Quaternion.AngleAxis(Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime, Vector3.up);
            //    transform.rotation = qRotation;
            //    _animation.CrossFade(anim.Walk.name, 0.3f); 
            //}
            #endregion
        }
        else
        {
            // 중력에 따라 플레이어가 가라앉게 해준다
            pJumpSpeed -= Gravity * Time.deltaTime;
        }
        // 최종 방향 계산
        Vector3 movement = moveSpeed * moveDirection + new Vector3(0, pJumpSpeed, 0);
        // 의도하지 않게 빠르게 동작하지 않도록 초단위 시간 조절
        movement *= Time.deltaTime;
        // 최종적으로 계산된 결과 방향(이동, 회전, 점프)으로 캐릭터 컨트롤러를 움직여준다
        controller.Move(movement);
    }

    void GetInput()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
    }

    #region < 이동 관련 함수 >

    #region < 이동 방향 + 회전 방향을 계산하는 부분 >
    void CalcDirection()
    {
        // 카메라 벡터를 이용하여 위치를 보정할 경우
        // 카메라의 전면 벡터, 우측 벡터를 정규화시킨 값을 활용하여
        Vector3 forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = forward.normalized;
        Vector3 right = Camera.main.transform.right;
        right.y = 0;
        right = right.normalized;
        // 카메라의 벡터를 방향 벡터에 곱해서 입력 방향 벡터를 보정한다
        Vector3 inputDirection = Input.GetAxis("Horizontal") * right + Input.GetAxis("Vertical") * forward;
        // 입력 방향 벡터에 정규화된 방향 벡터를 각각 곱해준다
        //Vector3 inputDirection = Input.GetAxis("Horizontal") * Vector3.right + Input.GetAxis("Vertical") * Vector3.forward;
        // 입력 방향 벡터를 이동 방향 벡터에 넣어준 다음 정규화시켜준다
        if (inputDirection != Vector3.zero)
        {
            moveDirection = Vector3.Lerp(moveDirection, inputDirection, rotateSmoothing * Time.deltaTime);
            moveDirection = moveDirection.normalized;
            // 캐릭터를 회전시켜준다
            transform.rotation = Quaternion.LookRotation(moveDirection);
        }
        // 플레이어 이동 속도에 벡터의 크기를 곱해서 최종 이동 속도를 계산해준다
        moveSpeed = pMoveSpeed * inputDirection.magnitude;
    }
    #endregion

    #region < 달리기. 최종 이동 속도 증가 >
    void Running()
    {
        // 좌측 쉬프트 키를 누르면 이동 속도 증가
        if (Input.GetKeyDown(KeyCode.LeftShift) && (h != 0 || v != 0))
        {
            isRunning = true;

            if (isRunning)
            {
                // 최종 이동 속도를 곧바로 증가시켜 유저가 직관적으로 달리기 상태가 시작되었음을 느끼게 할 수 있다.
                pMoveSpeed = sMoveSpeed * runMultiplier;
                #region < 서서히 가속되도록 만들고 싶은 경우 >
                //isAccelerateSlowly = true;
                #endregion
                //Debug.Log("달리기 시작햇다능");
            }
        }

        // 좌측 쉬프트 키를 떼면 이동 속도 복귀
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRunning = false;

            if (!isRunning)
            {
                // 최종 이동 속도를 곧바로 감소시켜 유저가 직관적으로 달리기 상태가 해제되었음을 느끼게 할 수 있다.
                pMoveSpeed = sMoveSpeed;
                #region < 서서히 감속되도록 만들고 싶은 경우 >
                //isReduceSlowly = true;
                #endregion
                //Debug.Log("달리기 끝낫다능");
            }
        }
    }

    #region < 서서히 가속되도록 만들고 싶은 경우 >
    IEnumerator AccelerateMoveSpeed(float accelerateSecondbySecond)
    {
        Debug.Log("가속 코루틴 시작");
        pMoveSpeed += accelerationSpeed * 4;
        StopAccelerateRunSpeedCoroutine();
        yield return new WaitForSeconds(accelerateSecondbySecond);
    }

    void StopAccelerateRunSpeedCoroutine()
    {
        if (pMoveSpeed >= sMoveSpeed * runMultiplier - accelerationSpeed * 0.1f)
        {
            Debug.Log("가속 코루틴 중지");
            isAccelerateSlowly = false;
            StopCoroutine("AccelerateMoveSpeed");
        }
    }
    #endregion

    #region < 서서히 감속되도록 만들고 싶은 경우 >

    IEnumerator ReduceMoveSpeed(float reduceSecondbySecond)
    {
        Debug.Log("감속 코루틴 시작");
        //Debug.Log("pMoveSpeed : " + pMoveSpeed);
        //Debug.Log("accelerationSpeed : " + accelerationSpeed);
        StopReduceRunSpeedCoroutine();
        yield return new WaitForSeconds(reduceSecondbySecond);
        pMoveSpeed -= accelerationSpeed;
    }

    void StopReduceRunSpeedCoroutine()
    {
        if (pMoveSpeed <= sMoveSpeed - accelerationSpeed - accelerationSpeed * 0.1f)
        {
            Debug.Log("감속 코루틴 중지");
            isReduceSlowly = false;
            StopCoroutine("ReduceMoveSpeed");
        }
    }
    #endregion

    #endregion

    #region < 점프! >
    void Jumping()
    {
        // 점프 버튼(스페이스 바 등)을 누르면
        if (Input.GetAxis("Jump") != 0)
        {
            isJumping = true;

            if (isJumping)
            {
                // 플레이어 점프 속도에 점프 속도값을 넣어준다
                pJumpSpeed = jumpSpeed;
                //Debug.Log("점프햇다능");
            }
        }
    }
    #endregion

    #endregion

    #region < 레이 캐스팅 관련 함수 > 

    #region < 클릭 / 클릭으로 오브젝트 줍고 놓는 기능 > 
    //void Picking()
    //{
    //    // 클릭 혹은 좌측 컨트롤 키를 누른 경우
    //    if (Input.GetButtonDown("Fire1") && !isPickingUp /*&& isCanPicking*/)
    //    {
    //        Vector3 rayOrigin = MainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
    //        //Vector3 rayOrigin = transform.position;

    //        // 레이 적중 타입 변수 정의
    //        RaycastHit hit;
    //        // 레이를 실제로 쏘는
    //        // 1. 레이의 위치. 2. 방향. 3. 맞은 위치. 4. 최대 범위. (5. 맞출 레이어 마스크)
    //        if (Physics.Raycast(transform.position, transform.forward, out hit, maxRayDistance))
    //        {
    //            Debug.DrawLine(transform.position, transform.forward, Color.green);
    //            if (hit.collider.tag == "PickingObject")
    //            {
    //                // 오브젝트의 중력을 없앤 다음
    //                hit.collider.gameObject.GetComponent<Rigidbody>().useGravity = false;
    //                hit.collider.gameObject.GetComponent<Rigidbody>().isKinematic = true;
    //                // 잡힌 오브젝트를 줍기 부분으로 보내고
    //                hit.collider.gameObject.GetComponent<Transform>().position = StartRayPos.position;
    //                // 쓰레기를 플레이어의 자식으로 지정해준다
    //                hit.collider.gameObject.GetComponent<Transform>().parent = this.transform;
    //                // 줍기 상태라는 플래그를 체크해준다
    //                isPickingUp = true;
    //            }
    //        }
    //    }
    //    else if (Input.GetButtonDown("Fire1") && isPickingUp)
    //    {
    //        #region < 주워야할 프리팹으로 생성되었을 경우 사용할 부분 >
    //        //// 주우면서 걷기 애니메이션을 해제해준다
    //        //PlayerAnim.SetBool("IsHoldWalk", false);

    //        //if (transform.FindChild("오브젝트명(Clone)"))
    //        //{
    //        //    // 오브젝트의 중력을 되돌려주고
    //        //    transform.FindChild("오브젝트명(Clone)").GetComponent<Rigidbody>().useGravity = true;
    //        //    transform.FindChild("오브젝트명(Clone)").GetComponent<Rigidbody>().isKinematic = false;
    //        //    // 부모자식 상태를 해제한다
    //        //    transform.FindChild("오브젝트명(Clone)").GetComponent<Transform>().parent = null;
    //        //}
    //        #endregion

    //        if (transform.FindChild("Fish"))
    //        {
    //            // 쓰레기의 중력을 되돌려주고
    //            transform.FindChild("Fish").GetComponent<Rigidbody>().useGravity = true;
    //            transform.FindChild("Fish").GetComponent<Rigidbody>().isKinematic = false;
    //            // 부모자식 상태를 해제한다
    //            transform.FindChild("Fish").GetComponent<Transform>().parent = null;
    //        }
    //        // 줍기 상태 플래그를 해제해준다
    //        isPickingUp = false;
    //    }
    //}
    #endregion

    #endregion

    #region < 애니메이션 관련 함수>

    // 일정 시간 이후 Idle 상태로 돌아가게 해주는 함수
    void ReturnIdle()
    {
        isIdle = true;
        isOther = false;
    }

    #region < 위치 상관 없이 보여줄 애니메이션 >
    void ShowAnimation()
    {
        // 달리기 애니메이션을 보여준다
        if (isRunning && !isJumping)
        {
            #region < 레거시 애니메이션을 사용할 경우>
            //_animation.clip = anim.Run;
            //_animation.Play();
            #endregion
        }
        // 점프 애니메이션을 보여준다
        if (Input.GetAxis("Jump") != 0)
        {
            #region < 레거시 애니메이션을 사용할 경우>
            //_animation.clip = anim.Jump;
            //_animation.Play();
            #endregion
        }
    }
    #endregion

    #region < 땅에 닿아있을 때만 보여줄 애니메이션 >
    void ShowAnimationOnGround()
    {
        // 키보드 입력값을 기준으로 동작할 애니메이션 수행
        if (v >= 0.1f || v <= -0.1f || h >= 0.1f || h <= -0.1f)
        {
            #region < 레거시 애니메이션을 사용할 경우>
            //_animation.CrossFade(anim.Walk.name, 0.3f);
            #endregion
        }
        else
        {
            // Q를 누르면 아이들 동작이 아닌 다른 애니메이션 재생
            if (Input.GetKeyDown(KeyCode.Q))
            {
                isOther = true;
                isIdle = false;

                if (isOther)
                {
                    #region < 레거시 애니메이션을 사용할 경우>
                    //_animation.clip = anim.Other;
                    //_animation.Play();
                    #endregion

                    // 일정시간 이후 아이들 상태로 되돌아간다
                    Invoke("ReturnIdle", ReturnIdleTime);
                }
            }

            if (isIdle && !isOther)
            {
                #region < 레거시 애니메이션을 사용할 경우>
                //_animation.clip = anim.Idle;
                //_animation.Play();
                #endregion
            }
        }
    }
    #endregion

    #endregion
}