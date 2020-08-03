using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManagment : MonoBehaviour
{
    public int damage;//このスクリプトをつけたものにダメージを与えることが可能になる。
    public GameObject hitEffect;
    bool timerTrigger = false;
    private float timer;//エフェクト出す際に使用している。
    private AudioSource audioSource;
    private new Collider collider;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        collider = GetComponent<BoxCollider>();
        hitEffect.SetActive(false);
    }

    private void Update()
    {
        if (timerTrigger == true)
        {
            timer += Time.deltaTime;
            hitEffect.SetActive(true);
           
            if(timer >= 0.7)
            {
                hitEffect.SetActive(false);
                timer = 0.0f;
                timerTrigger = false;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "House")
        {
            timerTrigger = true;
            this.collider.enabled = false;
            audioSource.Play();
        }
    }

   
}
