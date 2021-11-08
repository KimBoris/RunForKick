using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerScript : MonoBehaviour
{
    public float moveSpeed; //���� �̵��ӵ�
    float maxSpeed;       //�ִ� �̵��ӵ�
    float minusSpeed;            //���ҽ�ų ���ǵ�
    float jumpForce;        //������
    public float currSpeed;      //����ӵ�
    public float playerAngle;    //�����̽��� ������ �����ϴ� ���� (0~90)

    public Vector3 moveDir;     //�̵� ����

    public Image speedBar;      //���ǵ� ������
    public Image angleBar;      //���� ������

    bool LeftKeyInput;  //���� �Է� ����
    bool RightKeyInput; //������ �Է� ����
    float InputLeftTime;        //���� �Է½� ����Ű �ߺ� ���� ���� �ð�����
    float InputRightTime;       //������ �Է½� ����Ű �ߺ� ����

    bool isJump;                //����
    bool isRunning;             //�޷����� �߿��� Ű �Է��ϱ�����
    bool isSpaceup;             //�� á���� ����
    bool isFullAngle;           //������UI�� ������ ó��
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
        // �޸��� ���϶� 
        if (isRunning == true)
        {   //��������Ű�� �����ؼ� �������ÿ� �ӵ� ���̳ʽ�
            minusSpeed = Random.Range(0.005f, 0.007f);
            moveDir = Vector3.forward;
            this.transform.position += moveDir * moveSpeed;
            //[Ű�Է�]
            ArrowButtonDown();

            //[����]
            Jump();

            //�ӵ�����
            if (moveSpeed >= maxSpeed)
            {//���� �ӵ��� �ְ� �ӵ����� ���ٸ� ����ӵ� = �ְ�ӵ�
                moveSpeed = maxSpeed;
            }
            if (moveSpeed <= 0)
            {//�ӵ� ���ҷ� ���� 0���� �۾������� �ӵ����� 0 
                moveSpeed = 0;
            }
            DisplaySpeedbar();//���ǵ� �������� ��Ÿ���� UI
        }
        else if (isRunning == false)//������ ����(���� ���°�)
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

    void HitTheBall()//���� ���� �����ϴ� �Լ�
    {
        //����
        if (isSpaceup == false) //�����̽� Ű�� ���� ���� �ʾҴٸ�
        {
            if (Input.GetKey(KeyCode.Space))//�����̽�Ű�� ������ ������ 
            {
                cm.changeCamera = 1;//ī�޶��� ��ġ ������ �Կ� = Sidecam
                                    //UI������ 0~90���� �ݺ����� ����

                //Bool�� isFullAngle�� Ȱ���Ͽ� UI ������ ����
                if (isFullAngle == false)//������ 90�� ���� �ȵƴٸ� ����
                {
                    playerAngle += Time.deltaTime * 100;
                    if (playerAngle >= 90)
                    {
                        isFullAngle = true;
                    }
                }
                else if (isFullAngle == true)//������ 0�� ���� �ȵƴٸ� ����
                {
                    playerAngle -= Time.deltaTime * 100;
                    if (playerAngle <= 0)
                    {
                        isFullAngle = false;
                    }
                }
                DisplayAnglebar();//Angle������

            }
            else if (Input.GetKeyUp(KeyCode.Space)) //�����̽� Ű�� ���� ����
            {
                // �� ��Ʈ��
                BallScript bs = GameObject.Find("Ball").GetComponent<BallScript>();
                bs.ballMove();      //���� �̵� ��ũ��Ʈ
                cm.changeCamera = 2;//ī�޶��� ��ġ = ���� ���󰣴�(Ÿ�� ����)
                isSpaceup = true;   //�����̽����� ���� ���ٸ�
            }
        }
        AngleMaxMIn();//�ޱ� �ִ�/�� �� ���Ѱɱ�
    }

    void AngleMaxMIn()
    {//�ޱ��� �ִ� ��, �ּҰ�
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
        {//���� Ű�� ��������
            if (LeftKeyInput == false)
            {
                moveSpeed += 0.015f;//�̵� �ӵ� ����

                LeftKeyInput = true;//����Ű �Է�
                if (Input.GetKeyDown(KeyCode.RightArrow))//������ ���·� ������ Ű�� ������
                {
                    LeftKeyInput = false;//����Ű�� �Է��� false�� ����
                    InputLeftTime = 0;
                }
            }
        }
        //���������� �ð��� �����ϰ� �Ǿ �ٸ� ���ǹ��� ���(LeftKeyInput)
        if (LeftKeyInput == true)//����Ű�� �Է��� ���¶��
        {
            InputLeftTime += Time.deltaTime;//InputleftTime ���� > �����ð� ���� Ű ��� ����
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {//������ Ű ������ �Ǹ� �ʱ�ȭ
                LeftKeyInput = false;
                InputLeftTime = 0;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {//����Ű�� �ѹ��� �����Եȴٸ� �ӵ� ����
                moveSpeed -= minusSpeed;
            }
        }
        if (InputLeftTime > 2)
        {//���� �ð� ���� �� ���ʹ���Ű ��� ����
            LeftKeyInput = false;
            InputLeftTime = 0f;
        }
        //������ ����Ű
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
        //����
        if (isJump == false)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {   //�߷��� �����Ƿ� ������ٵ� ���� ���� �ѹ� �ش�
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                moveSpeed -= 0.02f;
                isJump = true;
            }
        }
    }
}









