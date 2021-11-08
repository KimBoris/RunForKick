using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMakerScr : MonoBehaviour
{

    public GameObject boostshose;//속도 증가 아이템

    float item_delay;//아이템 생성 주기
    float item_timer;//아이템 생성 시간

    float itemPos;//아이템 생성 위치

    void Start()
    {
        item_delay = 3;
        item_timer = 0;
    }


    void Update()
    {
        if (GameObject.Find("Player").transform.position.z < 280)//맵을 벗어나면 아이템 생성이 되지 않게
        {
            itemPos = GameObject.Find("Player").transform.position.z;//player의 위치를 기준으로 아이템 생성
            item_timer += Time.deltaTime;
            if (item_timer >= item_delay)
            {
                item_timer -= item_delay;
                Instantiate(boostshose, new Vector3(0, 1.5f, itemPos + 15), Quaternion.identity);
            }
        }
    }

}
