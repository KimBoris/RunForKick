using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpScript : MonoBehaviour
{
    float SpeedUp;//���ǵ�� ������ �Ծ����� 
    public GameObject itemEff;//������ �Ծ����� ����Ʈ
    void Start()
    {

    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            itemEff = Instantiate(itemEff, this.transform.position, Quaternion.identity);//�������� �Ծ����� ȿ��
            SpeedUp = 0.05f;
            GameObject.Find("Player").GetComponent<PlayerScript>().moveSpeed += SpeedUp;//���ǵ�������ش�
            Destroy(this.gameObject);
            Destroy(itemEff, 0.75f);
        }
    }

}
