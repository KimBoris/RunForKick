using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameramoving : MonoBehaviour
{
    Transform tr;

    public Transform targetisBall;
    public Transform target;//ī�޶��� ���� ���

    public float moveDamping = 10f; //�̵� �ӵ� ���
    public float distance = 1.8f;   //���� ������ �Ÿ�
    public float height = 1.5f;     // ���� ������ ����
    public float targetOffset = 1f; //���� ��ǥ�� ������

    public int changeCamera;//ī�޶� �ٲ�� ���� (���� �� ����)
    //0. �÷��̾� �����
    //1. ���̵� ķ
    //2. ���� ���󰡴� ī�޶�

    void Start()
    {
        tr = GetComponent<Transform>();
        changeCamera = 0;
    }

    void LateUpdate()
    {
        if (changeCamera == 0)
        {
            BasicCam();
        }
        else if (changeCamera == 1)
        {
            sideBall();
        }
        else
        {
            focusBall();
        }


    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(target.position + (target.up * targetOffset), 0.1f);
        Gizmos.DrawLine(target.position + (target.up * targetOffset), transform.position);
    }

    void BasicCam()//�⺻ ī�޶�
    {
        var camPos = target.position - (target.forward * distance) + (target.up * height);
        tr.position = Vector3.Slerp(tr.position, camPos, Time.deltaTime * moveDamping);
        tr.LookAt(target.position + (target.up * targetOffset));
    }
    public void sideBall()//���̵� ī�޶�
    {//������ ���� �� ���̵忡�� ��� ķ
        moveDamping = 10f;

        this.transform.eulerAngles = new Vector3(0, 70, 0);
        var camPos2 = targetisBall.position - new Vector3(2, 0, 1);
        tr.position = Vector3.Slerp(tr.position, camPos2, Time.deltaTime * moveDamping);

    }
    void focusBall()//���� Ÿ������ ��� ī�޶�
    {
        moveDamping = 20f;//�̵� �ӵ� ���

        this.transform.eulerAngles = new Vector3(-2, 0, 0);
        var camPos3 = targetisBall.position - new Vector3(0, 0, 5);
        tr.position = Vector3.Slerp(tr.position, camPos3, Time.deltaTime * moveDamping);
        //tr.LookAt(targetisBall.position + (targetisBall.up * targetOffset));
    }


}
