using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DamageScoreText : MonoBehaviour
{
    public static int score ;
    private GameObject[] enemy = new GameObject[100];
    private int enemyScore;
    private EnemyController ec ; 
    private SlimeController slimeController;
    
    void Start()
    {
        score = 0;  

    }


    void Update()
    {
        enemy = GameObject.FindGameObjectsWithTag("Enemy" );
        for (int i = 0; i < enemy.Length; i++)
        {
            

           
            if (enemy != null)//ゲームオブジェクトが見つかった時に処理開始
            {
                if(enemy[i].GetComponent<EnemyController>())
                {
                     ec  = enemy[i].GetComponent<EnemyController>();//EnemyControllerを取得
                    if (ec.dmgCh == true)
                    {
                        enemyScore = ec.damageScore;
                        score += enemyScore;
                        ec.dmgCh = false;

                    }
                }
                else if (enemy[i].GetComponent<SlimeController>())
                {
                    slimeController = enemy[i].GetComponent<SlimeController>();
                    if (slimeController.dmgCh == true)
                    {
                        enemyScore = slimeController.damageScore;
                        score += enemyScore;
                        slimeController.dmgCh = false;
                    }
                }
                else if (enemy[i].GetComponent<DragonController>())
                {
                    DragonController dc = enemy[i].GetComponent<DragonController>();
                    if(dc.dmgCh == true)
                    {
                        enemyScore = dc.damageScore;
                        score += enemyScore;
                        dc.dmgCh = false;
                    }
                }
                
               

                
              
            }
        }
       this.GetComponent<Text>().text = score.ToString() + "  dmg";
    }
    public static int GetEndScore()
    {
        return score;
    }
}
