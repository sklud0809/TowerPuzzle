using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class TimeManager : MonoBehaviour
{
    private float sceneTime = 3;
    public float countDown = 120;
    public Text timeText;
    public Text gameClearText;
    private HouseController hc;
    void Start()
    {
        hc = GameObject.Find("Baker_house").GetComponent<HouseController>();
    }


    void Update()
    {
        if (hc.hp > 0)
        {


            countDown -= Time.deltaTime;
        }
        timeText.text = countDown.ToString("f1") + "秒";

        if (countDown <= 0)
        {
             sceneTime -= Time.deltaTime;
             gameClearText.text = "GAME CLEAR!!";
             countDown = 0;
            if (sceneTime <= 0)
            {
               SceneManager.LoadScene("ScoreScene");
            }
         }

        
    }
}
