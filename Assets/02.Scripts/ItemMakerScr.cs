using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMakerScr : MonoBehaviour
{

    public GameObject boostshose;//�ӵ� ���� ������

    float item_delay;//������ ���� �ֱ�
    float item_timer;//������ ���� �ð�

    float itemPos;//������ ���� ��ġ

    void Start()
    {
        item_delay = 3;
        item_timer = 0;
    }


    void Update()
    {
        if (GameObject.Find("Player").transform.position.z < 280)//���� ����� ������ ������ ���� �ʰ�
        {
            itemPos = GameObject.Find("Player").transform.position.z;//player�� ��ġ�� �������� ������ ����
            item_timer += Time.deltaTime;
            if (item_timer >= item_delay)
            {
                item_timer -= item_delay;
                Instantiate(boostshose, new Vector3(0, 1.5f, itemPos + 15), Quaternion.identity);
            }
        }
    }

}
