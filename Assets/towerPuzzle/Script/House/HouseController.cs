using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class HouseController : MonoBehaviour
{
    public HPManager hpManager;
    public int maxhp = 1000;
    public int hp = 1000;
    private GameObject stateText;
    private float timer;
    void Start()
    {
        stateText = GameObject.Find("GameOverText");
        hpManager.Init(this);
    }

    void Update()
    {
        if(hp <= 0)
        {
           // Debug.Log("GameOverTimer");
            timer += Time.deltaTime;
            if (timer > 3)
            {
             //   Debug.Log("GameOver");
                SceneManager.LoadScene("GameOverScene");
            }
        }
    }

    void Damage(int damage)
    {
        hp -= damage;
        if(hp < 0)
        {
            hp = 0;
            this.stateText.GetComponent<Text>().text = "GAME OVER";
        }
        hpManager.UpdateHP(hp);
       // Debug.Log("ハウスの残りHP" + hp);

    }

    private void OnTriggerEnter(Collider other)
    {
        DamageManagment damageMana = other.GetComponent<DamageManagment>();

        if(damageMana != null)
        {
            Damage(damageMana.damage);
        }

    }
}
