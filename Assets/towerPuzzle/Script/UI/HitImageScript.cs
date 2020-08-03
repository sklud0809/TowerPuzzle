using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitImageScript : MonoBehaviour
{
    private Animator anim;
    private PlayerController pc;
    private bool animTrigger = true;
    void Start()
    {
        anim = GetComponent<Animator>();
        pc = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

    }

    
    void Update()
    {
        ImageAnim();  
      
    }

    void ImageAnim()
    {
        if (pc.Gethit >= 1)
        {
            if (animTrigger == true)
            {
                anim.SetTrigger("hitImageTrigger");
                animTrigger = false;
            }
        }
        else if (pc.Gethit >= 0)
        {
            if (animTrigger == false)
            {
                animTrigger = true;
                anim.SetTrigger("hitImageTrigger2");
            }
        }
    }
}
