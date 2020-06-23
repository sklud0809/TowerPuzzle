using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour
{
    public int damage;
    public GameObject Effect;
    public AudioClip audioClip;
    private Vector3 vec;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            vec = new Vector3( other.transform.position.x,0,other.transform.position.z);
            EffectGenerator();
            AudioSource.PlayClipAtPoint(audioClip, other. transform.position);//音のなる座標となる音を指定
            Destroy(this.gameObject);
            
        }
    }

    private void EffectGenerator()
    {
        //エフェクトの生成
        GameObject _effect = Instantiate(Effect) as GameObject;
        //エフェクトの発生場所を特定
        _effect.transform.position = vec;
        Destroy(_effect, 3f);
       
    }
}
