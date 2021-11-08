using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpScript : MonoBehaviour
{
    float SpeedUp;//스피드업 아이템 먹었을때 
    public GameObject itemEff;//아이템 먹었을때 이펙트
    void Start()
    {

    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            itemEff = Instantiate(itemEff, this.transform.position, Quaternion.identity);//아이템을 먹었을때 효과
            SpeedUp = 0.05f;
            GameObject.Find("Player").GetComponent<PlayerScript>().moveSpeed += SpeedUp;//스피드업시켜준다
            Destroy(this.gameObject);
            Destroy(itemEff, 0.75f);
        }
    }

}
