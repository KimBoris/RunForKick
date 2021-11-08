using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    //게임 오버 여부
    bool isGameOver;
    public GameObject gameOverImg;  //게임 오브젝트

    //스코어 UI
    public Image image1;
    public Image image10;
    public Image image100;
    public Image image1000;

    //점수
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
        score = GameObject.Find("Ball").transform.position.z - 286; // 점수는 공의 위치값으로 계산
        float bsRd = GameObject.Find("Ball").GetComponent<Rigidbody>().drag;//공의 마찰값

        if (score > 0)
        {
            Score();
        }
        if (bsRd > 52) //공의 마찰계수
        {   //공의 자연스러운 연출과 완벽하게 멈춘후 게임을 종료하기 위해
            //마찰력을 높혀주어 일정 이상이 되면 게임오버
            gameOverFunc(); //게임오버 함수
        }

        if (isGameOver == true)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Time.timeScale = 1;                 //타임 스케일 초기화
                SceneManager.LoadScene("PlayScene");//플레이씬 다시 부르기
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
