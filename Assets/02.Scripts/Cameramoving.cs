using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameramoving : MonoBehaviour
{
    Transform tr;

    public Transform targetisBall;
    public Transform target;//카메라의 추적 대상

    public float moveDamping = 10f; //이동 속도 계수
    public float distance = 1.8f;   //추적 대상과의 거리
    public float height = 1.5f;     // 추적 대상과의 높이
    public float targetOffset = 1f; //추적 좌표의 오프셋

    public int changeCamera;//카메라 바뀌는 시점 (공을 찬 순간)
    //0. 플레이어 뒷통수
    //1. 사이드 캠
    //2. 공을 따라가는 카메라

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

    void BasicCam()//기본 카메라
    {
        var camPos = target.position - (target.forward * distance) + (target.up * height);
        tr.position = Vector3.Slerp(tr.position, camPos, Time.deltaTime * moveDamping);
        tr.LookAt(target.position + (target.up * targetOffset));
    }
    public void sideBall()//사이드 카메라
    {//목적지 도착 후 사이드에서 찍는 캠
        moveDamping = 10f;

        this.transform.eulerAngles = new Vector3(0, 70, 0);
        var camPos2 = targetisBall.position - new Vector3(2, 0, 1);
        tr.position = Vector3.Slerp(tr.position, camPos2, Time.deltaTime * moveDamping);

    }
    void focusBall()//공을 타겟으로 잡는 카메라
    {
        moveDamping = 20f;//이동 속도 계수

        this.transform.eulerAngles = new Vector3(-2, 0, 0);
        var camPos3 = targetisBall.position - new Vector3(0, 0, 5);
        tr.position = Vector3.Slerp(tr.position, camPos3, Time.deltaTime * moveDamping);
        //tr.LookAt(targetisBall.position + (targetisBall.up * targetOffset));
    }


}
