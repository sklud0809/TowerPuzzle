using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneratorScript : MonoBehaviour
{
    public GameObject enemyPrefab;

    private float interval;

    private float time = 0.0f;
    void Start()
    {
        GameObject enemy = Instantiate(enemyPrefab);
        interval = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if(time >= interval)
        {
            GameObject enemy = Instantiate(enemyPrefab);

            //生成したときの座標指定

            time = 0.0f; //タイマーのリセット
        }

        
    }
}
