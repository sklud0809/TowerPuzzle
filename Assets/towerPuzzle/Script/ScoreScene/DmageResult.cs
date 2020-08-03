using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DmageResult : MonoBehaviour
{
    private int score;
    public Text scoreText;
    public Text higeScoreText;
    private int higeScore;
    private string key = "HIGE SCORE";


    void Start()
    {
        score = DamageScoreText.GetEndScore();

        scoreText.text = score.ToString();

        higeScore = PlayerPrefs.GetInt(key, 0);//保存していたハイスコアキーを呼び出し
        higeScoreText.text = "過去最大ダメージ:" + higeScore.ToString(); //ハイスコアの表示

        //PlayerPrefs.DeleteAll();//過去最大ダメージの記録をリセット※開発用
    }

    
    void Update()
    {
        if(score > higeScore)
        {
            higeScore = score;//ハイスコアの更新
            PlayerPrefs.SetInt(key, higeScore);
            higeScoreText.text = "過去最大ダメージ:" + higeScore.ToString();
        }
    }
}
