using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GetHitText : MonoBehaviour
{
    private Text getHitText;
    private PlayerController playerController;
    void Start()
    {
        getHitText = GetComponent<Text>();
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
       // getHitText.text = playerController.Gethit + "コンボ".ToString();  
    }

 
    void Update()
    {
        if (playerController.Gethit >= 1)
        {
            getHitText.text = playerController.Gethit + "コンボ発生中！".ToString();//コンボ数が変わると同時に数字も変化
        }
        else if(playerController.Gethit >= 0)
        {
            //getHitText.text = "".ToString();//何も表示しない
        }
    }
}
