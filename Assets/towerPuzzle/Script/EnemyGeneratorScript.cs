using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneratorScript : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject slimePrefab;
    public GameObject dragonPrefab;
   
    private float interval;
    public float minTime = 12f;
    public float maxTime = 20f;
    private int enemyNumber = 0;
    private int slimeNumber = 0;
    private int dragonNumber = 0;
    private float time = 0.0f;
    //X座標の最小値
    private float xMinPosition = 10f;
    //X座標の最大値
    private float xMaxPosition = 15f;
    //Y座標の最小値
    private float yMinPosition = 0f;
    //Y座標の最大値
    private float yMaxPosition = 0f;
    //Z座標の最小値
    private float zMinPosition = -5f;
    //Z座標の最大値
    private float zMaxPosition = 10f;



    void Start()
    {
       // GameObject enemy = Instantiate(enemyPrefab);

       // GameObject slime = Instantiate(slimePrefab);
       
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time >= interval)
        {
            GameObject enemy = Instantiate(enemyPrefab); // enemyの出現
            enemy.transform.position = GetRandomPosition(); //生成座標の指定をランダム
            time = 0.0f; //タイマーのリセット
            enemyNumber++;

            GameObject slime = Instantiate(slimePrefab); //スライムの出現
            slime.transform.position = GetRandomPosition(); //生成座標の指定をランダム
            time = 0.0f; //タイマーのリセット
            slimeNumber++;
            interval = GetRandomTime();
        }
         else if ((enemyNumber >= 5) && slimeNumber >= 5 && dragonNumber  == 0)
         {
                GameObject dragon = Instantiate(dragonPrefab);
                dragon.transform.position = GetRandomPosition();
                dragonNumber++;
                interval = 20f;
               
           
       
         
         }
        
     
    }

    private float GetRandomTime()
    {
        return Random.Range(minTime, maxTime);
    }

    //ランダムな位置を生成する関数
    private Vector3 GetRandomPosition()
    {
        //それぞれの座標をランダムに生成する
        float x = Random.Range(xMinPosition, xMaxPosition);
        float y = Random.Range(yMinPosition, yMaxPosition);
        float z = Random.Range(zMinPosition, zMaxPosition);

        //Vector3型のPositionを返す
        return new Vector3(x, y, z);
    }
}
