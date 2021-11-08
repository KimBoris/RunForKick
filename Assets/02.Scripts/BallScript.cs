using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BallScript : MonoBehaviour
{
    Vector3 Bdir; // ���� ����
    Rigidbody Brb;

    public GameObject finishEff; //�ǴϽö��� �ε������� ����Ʈ
    public GameObject Kickedeff; //á���� ����Ʈ
    public GameObject Crasheff;  //�ٴڿ� �ε������� ����Ʈ

    float kickPow;//moveSpeed �ӵ�
    float kickAngle;//PlayerAngle ����

    public bool KickedBall; //���� �������� ����

    bool BallGround; //���� ���ư��� ���� ��Ҵ��� ����

    int groundCount; //���������� ���� �ε��� Ƚ��

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
        if (BallGround == true)                  //���� ���� �������
        {
            if (groundCount >= 8)                //���� ���� ����Ƚ�� Ƣ��� �Ǹ�
            {
                Brb.drag += Time.deltaTime * 10f;//������ ũ���Ͽ� ���� ���߰� �Ѵ�
            }
        }
    }
    public void ballMove()
    {
        Instantiate<GameObject>(Kickedeff, this.gameObject.transform.position, Quaternion.identity);//���� á���� ����Ʈ

        Time.timeScale = 2f;

        PlayerScript ps = GameObject.Find("Player").GetComponent<PlayerScript>();
        kickPow = ps.moveSpeed;//�÷��̾��� ���ǵ�
        kickAngle = ps.playerAngle;//�÷��̾��� �Է� ����
        //AddForce - ������ : ���� ������������ ���ڿ�������.
        //Brb.AddForce(Bdir*(kickPow*1000f) + (Vector3.up*kickAngle*2));
        Brb.velocity = new Vector3(0, kickAngle * 0.7f, kickPow * 60f);//(0, �Է°���, ���ǵ�)
        KickedBall = true;//�� á���� ����
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Stage")//���� �ε����� 
        {
            Instantiate<GameObject>(Crasheff, this.transform.position, Quaternion.identity);//����Ʈ
            groundCount++;//���� �ε��� Ƚ��(������ ���߰� �ϴ� ���)
            BallGround = true;//���� ���� �ִ��� ����
        }
        else if (collision.gameObject.tag == "Finish")//�ǴϽ� ���ο� �ִ� �ݶ��̴��� �浹��
        {
            Instantiate<GameObject>(finishEff, this.transform.position, Quaternion.identity);
        }
    }
    private void OnDrawGizmos()
    {
        //���� forward�� ��Ÿ���� �����
        Gizmos.color = Color.red;
        Gizmos.DrawLine(this.transform.position, this.transform.position + Vector3.forward);
    }
}



