using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseController : MonoBehaviour
{
    public HPManager hpManager;
    public int maxhp = 1000;
    private int hp = 1000;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void Damage(int damage)
    {
        hp -= damage;
        if(hp < 0)
        {
            hp = 0;
            
        }
        hpManager.UpdateHP(hp);
        Debug.Log("ハウスの残りHP" + hp);

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
