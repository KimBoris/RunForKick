using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    //���� ���� ����
    bool isGameOver;
    public GameObject gameOverImg;  //���� ������Ʈ

    //���ھ� UI
    public Image image1;
    public Image image10;
    public Image image100;
    public Image image1000;

    //����
    public float score; 

    void Start()
    {
        isGameOver = false;
        score = 0;
    }

    public void gameOverFunc()
    {
        Time.timeScale = 0;
        gameOverImg.SetActive(true);
        isGameOver = true;
    }

    void Update()
    {
        score = GameObject.Find("Ball").transform.position.z - 286; // ������ ���� ��ġ������ ���
        float bsRd = GameObject.Find("Ball").GetComponent<Rigidbody>().drag;//���� ������

        if (score > 0)
        {
            Score();
        }
        if (bsRd > 52) //���� �������
        {   //���� �ڿ������� ����� �Ϻ��ϰ� ������ ������ �����ϱ� ����
            //�������� �����־� ���� �̻��� �Ǹ� ���ӿ���
            gameOverFunc(); //���ӿ��� �Լ�
        }

        if (isGameOver == true)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Time.timeScale = 1;                 //Ÿ�� ������ �ʱ�ȭ
                SceneManager.LoadScene("PlayScene");//�÷��̾� �ٽ� �θ���
            }
        }
    }
    public void Score()
    {
        //Debug.Log(score);
        int n1000 = (int)(score / 1000);
        int n100 = (int)(score % 1000) / 100;
        int n10 = ((int)score % 100) / 10;
        int n1 = ((int)score % 10) / 1;

        string fileName = "number";
        image1000.sprite = Resources.Load<Sprite>(fileName + n1000);
        image100.sprite = Resources.Load<Sprite>(fileName + n100);
        image10.sprite = Resources.Load<Sprite>(fileName + n10);
        image1.sprite = Resources.Load<Sprite>(fileName + n1);
    }

}
