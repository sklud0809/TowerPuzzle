using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour
{
    public int damage;
    public GameObject Effect;
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
            vec = new Vector3( other.transform.position.x,other.transform.position.y,other.transform.position.z);
            EffectGenerator();
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
