using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
public class PlayerController : MonoBehaviour {
    public float moveSpeed = 10f;
    private Rigidbody rb;
    private Animator anim;
    public int Gethit;
    private AudioSource audioSource;
    
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        Gethit = 0;
        
    }
	
	
	void Update ()
    {
        //移動　マルチプラットフォームにすることでジョイスティック操作可能
        float moveX = CrossPlatformInputManager.GetAxis("Horizontal");
       float moveZ = CrossPlatformInputManager.GetAxis("Vertical");

        Vector3 move = new Vector3(moveX, 0, moveZ);
        Vector3 direction = transform.position + new Vector3(moveX, 0, moveZ);
        transform.LookAt(direction);
        //リジッドボディに移動できるよう力を加える
        rb.velocity = (move * moveSpeed);

        anim.SetFloat("Speed", rb.velocity.magnitude); // 歩くアニメーションの再生

       
	}

    private void OnCollisionEnter(Collision collision)
    {
        //各属性のスフィアのみ当たり判定を指定
        if(collision.gameObject.tag == "BlueSphere" || collision.gameObject.tag == "RedSphere" || collision.gameObject.tag == "GreenSphere")
        {
            //各属性スフィアに当たった際、キャラに少しでも近くに寄せる。
           Vector3 pos = (collision.gameObject.transform.position - collision.gameObject.transform.position) / 3;
            collision.gameObject.transform.position -= pos;
            Collider col = collision.gameObject.GetComponent<Collider>();
             if(col != null)
            {
                col.isTrigger = true;　//スフィアのコライダーにトリガーをオンにして判定を変える
            }

            //スフィアをキャラの子オブジェクトにして吸着を再現
            collision.gameObject.transform.parent = this.gameObject.transform;//当たったオブジェクトは子になる
            audioSource.Play();
        }
    }

}
