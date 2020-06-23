using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HPManager : MonoBehaviour
{
    public Slider hpslider;
    //public GameObject damageScorePrefab;
    public GameObject fill;
    public void Init(HouseController houseController)
    {
        hpslider.maxValue = houseController.maxhp;
        hpslider.value = houseController.maxhp;
    }

    private void Start()
    {
       // GameObject damageScore = Instantiate(damageScorePrefab);
    }
    public void UpdateHP(int hp)
    {
       
        hpslider.value = hp;
         if(hpslider.value == 0)
        {
            fill.SetActive(false);
        }
    }

    



}
