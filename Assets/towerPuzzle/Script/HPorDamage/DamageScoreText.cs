using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DamageScoreText : MonoBehaviour
{
    private int score = 0;
    private GameObject enemy;
    private int enemyScore;
    private EnemyController enemyController;
    void Start()
    {
       

    }


    void Update()
    {
        enemy = GameObject.Find("EnemyPrefab(Clone)");//常にゲームオブジェクトを探している。
        //Debug.Log(enemy);
        if (enemy != null)//ゲームオブジェクトが見つかった時に処理開始
        {
            enemyController = enemy.GetComponent<EnemyController>();//EnemyControllerを取得し、スコアの加算を行う。
            //Debug.Log(enemyController);
            if (enemyController.dmgCh == true)
            {
                enemyScore = enemyController.damageScore;
                score += enemyScore;
                enemyController.dmgCh = false;

            }
        }
       this.GetComponent<Text>().text = this.score.ToString() + "  dmg";
    }
}
