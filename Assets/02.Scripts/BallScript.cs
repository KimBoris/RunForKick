using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BallScript : MonoBehaviour
{
    Vector3 Bdir; // 공의 방향
    Rigidbody Brb;

    public GameObject finishEff; //피니시라인 부딪쳤을때 이펙트
    public GameObject Kickedeff; //찼을때 이펙트
    public GameObject Crasheff;  //바닥에 부딪쳤을때 이펙트

    float kickPow;//moveSpeed 속도
    float kickAngle;//PlayerAngle 각도

    public bool KickedBall; //공이 차였는지 여부

    bool BallGround; //공이 날아가서 땅에 닿았는지 여부

    int groundCount; //스테이지에 공이 부딪힌 횟수

    void Start()
    {
        BallGround = false;
        KickedBall = false;
        Brb = GetComponent<Rigidbody>();

        Bdir = Vector3.forward;

        groundCount = 0;
    }
    void Update()
    {
        if (BallGround == true)                  //공이 땅에 닿았을때
        {
            if (groundCount >= 8)                //공이 땅에 일정횟수 튀기게 되면
            {
                Brb.drag += Time.deltaTime * 10f;//마찰을 크게하여 공을 멈추게 한다
            }
        }
    }
    public void ballMove()
    {
        Instantiate<GameObject>(Kickedeff, this.gameObject.transform.position, Quaternion.identity);//공을 찼을때 이펙트

        Time.timeScale = 2f;

        PlayerScript ps = GameObject.Find("Player").GetComponent<PlayerScript>();
        kickPow = ps.moveSpeed;//플레이어의 스피드
        kickAngle = ps.playerAngle;//플레이어의 입력 각도
        //AddForce - 문제점 : 힘이 빠질때쯔음에 부자연스럽다.
        //Brb.AddForce(Bdir*(kickPow*1000f) + (Vector3.up*kickAngle*2));
        Brb.velocity = new Vector3(0, kickAngle * 0.7f, kickPow * 60f);//(0, 입력각도, 스피드)
        KickedBall = true;//공 찼는지 여부
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Stage")//땅에 부딪히면 
        {
            Instantiate<GameObject>(Crasheff, this.transform.position, Quaternion.identity);//이펙트
            groundCount++;//땅에 부딪힌 횟수(게임을 멈추게 하는 요소)
            BallGround = true;//공이 땅에 있는지 여부
        }
        else if (collision.gameObject.tag == "Finish")//피니시 라인에 있는 콜라이더와 충돌시
        {
            Instantiate<GameObject>(finishEff, this.transform.position, Quaternion.identity);
        }
    }
    private void OnDrawGizmos()
    {
        //공의 forward를 나타내는 기즈모
        Gizmos.color = Color.red;
        Gizmos.DrawLine(this.transform.position, this.transform.position + Vector3.forward);
    }
}



