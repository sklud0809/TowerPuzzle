using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyController : MonoBehaviour
{
   
    private NavMeshAgent agent;
    private Animator anim; //アニメーターを取得
    public GameObject house; //ハウスを標的
    private int maxhp = 100;//敵Hp表示の際に使うのもの　※現在は未設定
    private int hp = 200;
    private int hit;  //PlayerController側にコンボ数を更新させるための箱
    private　PlayerController playerController;

    public GameObject disappearanceEffct; //死亡後のエフェクト
    public Collider Axe;
    public Collider AttackCollider;//子オブジェクト取得用の箱を宣言
    private float cooldown = 2.5f;//攻撃後のクールタイム
    float timer = 0.0f;//攻撃後のタイマー
    float dieTimer = 0.0f; //死亡後のタイマー

    //UI関連
    public int damageScore;
    public bool dmgCh = false;
 

    void Start()
    {
        //ナビメッシュエージェント取得
        agent = GetComponent<NavMeshAgent>();
        //アニメーターコンポーネント取得
        anim = GetComponent<Animator>();
       
       //アックスのコライダーをオフ 
        Axe.enabled = false;
       
        //子オブジェクトのコライダーをオフ
        AttackCollider.enabled = true;
        //プレハブから生成されたときに　シーン上にいるPlayerの中のPlayerControllerを取得している
        playerController = GameObject.Find("PlayerPrefab").GetComponent<PlayerController>();
        
    }

    
    void Update()
    {
        //家に向けて進行していく
        agent.destination = house.transform.position;
        //移動のアニメーションを設定
        anim.SetFloat("Speed", agent.remainingDistance);


        if (AttackCollider.enabled == false)
        {
           // Debug.Log("クールダウン開始");
            timer += Time.deltaTime;
            if ( timer >= cooldown)
            {
                AttackCollider.enabled = true;
                timer = 0.0f;
             //   Debug.Log("クールダウン終了");
            }
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idel"))
        {
            agent.speed = 3;
            agent.angularSpeed = 120;
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("GetHit"))
        {
            agent.speed = 0;
            agent.angularSpeed = 0;
        }

        //死亡アニメーション中に時間を測定
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Die"))
        {
            dieTimer += Time.deltaTime;
            agent.destination = this.transform.position;
            agent.speed = 0;
            if (dieTimer >= 2f)
            {
                DieEffect();
                dieTimer = 0.0f;
                Destroy(this.gameObject);
            }
        }
    }
    
    
    //属性オブジェクトに当たった時に起きる
    private void OnTriggerEnter(Collider other)
    {
        //ダメージとコンボ数を管理
        SphereController damageMana = other.GetComponent<SphereController>();
         
        hit = playerController.Gethit;　//hitの中にプレイヤーのコンボ数を入れる。

        if (other.gameObject.tag == "RedSphere")
        { 
            if (damageMana != null)
            {

                hit ++;　//　Enemyの中hitに１＋する
                playerController.Gethit = hit;　//プレイヤーのコンボ数にカウントをいれる
                for (int i = 1; i <= hit; i++)
                {
                   
                    Damage(damageMana.damage * i);
                    damageScore += damageMana.damage;
                    dmgCh = true;

                    Debug.Log(damageMana.damage * i + "のダメージを与えた");

                }
                
                
            }
        }
        else if(other.gameObject.tag == "BlueSphere")
        {
            if(damageMana != null)
            {
                Damage(damageMana.damage / 2) ;
                Debug.Log(damageMana.damage / 2 + "のダメージを与えた");
                hit = 0; 
                playerController.Gethit = hit;//コンボ数のリセット
            }
        }
        else if(other.gameObject.tag == "GreenSphere")
        {
            hit = 0;
            playerController.Gethit = hit;//コンボ数のリセット
        }
    }

    void Damage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            hp = 0;
            anim.SetTrigger("Die");
            

            
        }
        //hpManager.UpdateHP(hp); HP表示の際に使うもの　現在未設定
        Debug.Log("オークの残りHP" + hp);
        anim.SetTrigger("GetHit");
    }

     void DieEffect()
    {
        // エフェクトを発生
        GameObject dieEffect = Instantiate(disappearanceEffct) as GameObject;
        //エフェクトの位置を指定 
        dieEffect.transform.position = this.transform.position;
    }

    // 以下攻撃関連
    public void Attack()//子オジェクトのAttackCollierについているスクリプトに使用
    {
        anim.SetTrigger("Attack");//アタックアニメーションの再生
        AttackCollider.enabled = false;
        agent.speed = 0f;
        agent.angularSpeed = 0f;
    }

    //アニメーター（Ctrl+6）の攻撃モーションにコライダーの操作
    public void AttackHitStart()
    {
        Axe.enabled = true;
        
    }
    public void AttackHitFinishe()
    {
        Axe.enabled = false;
        agent.speed = 3f;
        agent.angularSpeed = 120;
    }
}
