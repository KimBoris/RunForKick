using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartScript : MonoBehaviour
{

   string PlayScene = "PlayScene";

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            SceneManager.LoadScene(PlayScene);
        }
    }
}
