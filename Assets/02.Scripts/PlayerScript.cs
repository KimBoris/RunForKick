using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerScript : MonoBehaviour
{
    public float moveSpeed; //현재 이동속도
    float maxSpeed;       //최대 이동속도
    float minusSpeed;            //감소시킬 스피드
    float jumpForce;        //점프력
    public float currSpeed;      //현재속도
    public float playerAngle;    //스페이스를 누르면 증가하는 각도 (0~90)

    public Vector3 moveDir;     //이동 방향

    public Image speedBar;      //스피드 게이지
    public Image angleBar;      //각도 게이지

    bool LeftKeyInput;  //왼쪽 입력 여부
    bool RightKeyInput; //오른쪽 입력 여부
    float InputLeftTime;        //왼쪽 입력시 같은키 중복 방지 위해 시간적용
    float InputRightTime;       //오른쪽 입력시 같은키 중복 방지

    bool isJump;                //점프
    bool isRunning;             //달려가는 중에만 키 입력하기위해
    bool isSpaceup;             //공 찼는지 여부
    bool isFullAngle;           //게이지UI의 증감을 처리
    Rigidbody rb;

    Cameramoving cm;



    void Start()
    {
        maxSpeed = 1.0f;
        jumpForce = 5f;
        moveSpeed = 0f;
        LeftKeyInput = false;
        RightKeyInput = false;
        InputLeftTime = 0f;
        InputRightTime = 0f;

        rb = GetComponent<Rigidbody>();
        cm = GameObject.Find("CameraRig").GetComponent<Cameramoving>();

        isJump = false;
        isRunning = true;
        isSpaceup = false;
        isFullAngle = false;
    }

    void Update()
    {
        // 달리기 중일때 
        if (isRunning == true)
        {   //같은방향키를 연속해서 눌렀을시에 속도 마이너스
            minusSpeed = Random.Range(0.005f, 0.007f);
            moveDir = Vector3.forward;
            this.transform.position += moveDir * moveSpeed;
            //[키입력]
            ArrowButtonDown();

            //[점프]
            Jump();

            //속도조절
            if (moveSpeed >= maxSpeed)
            {//현재 속도가 최고 속도보다 높다면 현재속도 = 최고속도
                moveSpeed = maxSpeed;
            }
            if (moveSpeed <= 0)
            {//속도 감소로 인해 0보다 작아질때에 속도값은 0 
                moveSpeed = 0;
            }
            DisplaySpeedbar();//스피드 게이지를 나타내는 UI
        }
        else if (isRunning == false)//목적지 도착(공을 차는곳)
        {
            HitTheBall();
            if (playerAngle < 0)
            {
                playerAngle = 0;
            }
            if (playerAngle > 90)
            {
                playerAngle = 90;
            }
            return;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isJump = false;
        }
    }

    void HitTheBall()//공을 찰때 실행하는 함수
    {
        //각도
        if (isSpaceup == false) //스페이스 키를 아직 떼지 않았다면
        {
            if (Input.GetKey(KeyCode.Space))//스페이스키를 누르고 있으면 
            {
                cm.changeCamera = 1;//카메라의 위치 옆에서 촬영 = Sidecam
                                    //UI게이지 0~90까지 반복적인 구현

                //Bool값 isFullAngle을 활용하여 UI 게이지 구현
                if (isFullAngle == false)//각도가 90이 아직 안됐다면 증가
                {
                    playerAngle += Time.deltaTime * 100;
                    if (playerAngle >= 90)
                    {
                        isFullAngle = true;
                    }
                }
                else if (isFullAngle == true)//각도가 0이 아직 안됐다면 감소
                {
                    playerAngle -= Time.deltaTime * 100;
                    if (playerAngle <= 0)
                    {
                        isFullAngle = false;
                    }
                }
                DisplayAnglebar();//Angle게이지

            }
            else if (Input.GetKeyUp(KeyCode.Space)) //스페이스 키를 떼는 순간
            {
                // 공 히트시
                BallScript bs = GameObject.Find("Ball").GetComponent<BallScript>();
                bs.ballMove();      //공의 이동 스크립트
                cm.changeCamera = 2;//카메라의 위치 = 공을 따라간다(타겟 변경)
                isSpaceup = true;   //스페이스에서 손을 땠다면
            }
        }
        AngleMaxMIn();//앵글 최대/소 값 제한걸기
    }

    void AngleMaxMIn()
    {//앵글의 최대 값, 최소값
        if (playerAngle < 0)
        {
            playerAngle = 0;
        }
        if (playerAngle > 90)
        {
            playerAngle = 90;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Wall")
        {
            moveDir = Vector3.zero;
            isRunning = false;
        }
    }
    void DisplayAnglebar()
    {
        angleBar.fillAmount = ((playerAngle + 270) / 360);
    }

    void DisplaySpeedbar()
    {
        currSpeed = moveSpeed;
        speedBar.fillAmount = (currSpeed / maxSpeed);
    }
    void ArrowButtonDown()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {//왼쪽 키를 눌렀을때
            if (LeftKeyInput == false)
            {
                moveSpeed += 0.015f;//이동 속도 증가

                LeftKeyInput = true;//왼쪽키 입력
                if (Input.GetKeyDown(KeyCode.RightArrow))//현재의 상태로 오른쪽 키를 누르면
                {
                    LeftKeyInput = false;//왼쪽키의 입력이 false로 변경
                    InputLeftTime = 0;
                }
            }
        }
        //눌렀을때만 시간이 증가하게 되어서 다른 조건문을 사용(LeftKeyInput)
        if (LeftKeyInput == true)//왼쪽키를 입력한 상태라면
        {
            InputLeftTime += Time.deltaTime;//InputleftTime 증가 > 일정시간 이후 키 잠금 해제
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {//오른쪽 키 누르게 되면 초기화
                LeftKeyInput = false;
                InputLeftTime = 0;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {//왼쪽키를 한번더 누르게된다면 속도 감소
                moveSpeed -= minusSpeed;
            }
        }
        if (InputLeftTime > 2)
        {//일정 시간 지난 후 왼쪽방향키 잠금 해제
            LeftKeyInput = false;
            InputLeftTime = 0f;
        }
        //오른쪽 방향키
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (RightKeyInput == false)
            {
                moveSpeed += 0.015f;
                RightKeyInput = true;
            }
        }
        if (RightKeyInput == true)
        {
            InputRightTime += Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                RightKeyInput = false;
                InputRightTime = 0;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                moveSpeed -= minusSpeed;
            }
        }
        if (InputRightTime > 2)
        {
            RightKeyInput = false;
            InputRightTime = 0f;
        }
    }
    void Jump()
    {
        //점프
        if (isJump == false)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {   //중력이 있으므로 리지드바디를 통해 힘을 한번 준다
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                moveSpeed -= 0.02f;
                isJump = true;
            }
        }
    }
}









