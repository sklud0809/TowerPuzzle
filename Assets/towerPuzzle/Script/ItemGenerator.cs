using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    public GameObject redSpherePrefab;
    public GameObject blueSpherePrefab;
    public GameObject greenSpherePrefab;
    float xMinPosition = -25;
    float xMaxPosition = -8;
    float yMaxPosition = 0.3f;
    float yMinPosition = 0.3f;
    float zMinPosition = -15;
    float zMaxPosition = -5;
    int createCount;
    private float time = 0.0f;
    private float interval;
    public float minTime;
    public float maxTime;
    void Start()
    {
        createCount = Random.Range(1, 4);
        GameObject redSphrere = Instantiate(redSpherePrefab) as GameObject;
 
        redSphrere.transform.position = GetRandomPosition();
        GameObject blueSphrere = Instantiate(blueSpherePrefab) as GameObject;
        blueSphrere.transform.position = GetRandomPosition();
        GameObject greenSphrere = Instantiate(greenSpherePrefab) as GameObject;
        greenSphrere.transform.position =  GetRandomPosition();
    }

   
    void Update()
    {

        time += Time.deltaTime;
        if (time >= interval)
        {
            createCount = Random.Range(1, 3);
            for (int i = 0; i < createCount; i++)
            {
                GameObject redSphere = Instantiate(redSpherePrefab) as GameObject;
                redSphere.transform.position = GetRandomPosition();
            }
           
            for (int i = 0; i < createCount; i++)
            {
                GameObject greenSphere = Instantiate(greenSpherePrefab) as GameObject;
                greenSphere.transform.position = GetRandomPosition();
            }
            for(int i = 0; i < createCount; i++)
            {
                GameObject blueSphere = Instantiate(blueSpherePrefab) as GameObject;
                blueSphere.transform.position = GetRandomPosition();
            }
            

            time = 0.0f;
            interval = GetRandomTime();
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
