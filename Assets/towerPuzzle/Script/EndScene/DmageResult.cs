using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DmageResult : MonoBehaviour
{
    private int score;
    public Text scoreText;

    void Start()
    {
        score = DamageScoreText.GetEndScore();

        scoreText.text = score.ToString();
    }

    
    void Update()
    {
        
    }
}
