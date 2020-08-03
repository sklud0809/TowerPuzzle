using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class DragonController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator anim;
    public GameObject house; //ハウスを標的
    public int hp = 200;
    public Collider AttackCollider;//子オブジェクト取得用の箱を宣言
    public Collider attackHitCollider;
    private float cooldown = 3.5f;//攻撃後のクールタイム
    float timer = 0.0f;//攻撃後のタイマー
    float dieTimer = 0.0f; //死亡後のタイマー
    public GameObject disappearanceEffct; //死亡後のエフェクト

    private int hit;  //PlayerController側にコンボ数を更新させるための箱
    private PlayerController playerController;
    private TimeManager tm;
    //UI関連
    public int damageScore;
    public bool dmgCh = false;
    public int dcdie = 0; //SceneManagerにて切り替え判定としての条件
    void Start()
    {
        //ナビメッシュエージェント取得
        agent = GetComponent<NavMeshAgent>();
        //アニメーターコンポーネント取得
        anim = GetComponent<Animator>();
        attackHitCollider.enabled = false;

        //子オブジェクトのコライダーをオフ
        AttackCollider.enabled = true;
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        tm = GameObject.Find("TimeManager").GetComponent<TimeManager>();//TimeManagerのスクリプトを取得
    }


    void Update()
    {
        agent.destination = house.transform.position;
        //移動のアニメーションを設定
        anim.SetFloat("Speed", agent.remainingDistance);

        if (AttackCollider.enabled == false)
        {
           
            timer += Time.deltaTime;
            if (timer >= cooldown)
            {
                AttackCollider.enabled = true;
                timer = 0.0f;
                
            }
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idel"))
        {
            agent.speed = 1.3f;
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
            attackHitCollider.enabled = false;
            agent.destination = this.transform.position;
            agent.speed = 0;
            if (dieTimer >= 2f)
            {
                DieEffect();
                dieTimer = 0.0f;
                dcdie++;
                Destroy(this.gameObject);
                
            }
        }

        //ゲームクリア時に攻撃をさせない
        if (tm.countDown <= 0)
        {
            AttackCollider.enabled = false;
            

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
                Damage(damageMana.damage / 2);
                damageScore += damageMana.damage / 2;// ダメージスコアを加算
                dmgCh = true;
                hit = 0;
                playerController.Gethit = hit;//コンボ数のリセット
            }
            
        }
        else if (other.gameObject.tag == "BlueSphere")
        {
            if (damageMana != null)
            {

                hit++;　//　Enemyの中hitに１＋する
                playerController.Gethit = hit;　//プレイヤーのコンボ数にカウントをいれる
                for (int i = 0; i <= hit; i++)
                {
                    anim.SetTrigger("GetHit");
                    Damage(damageMana.damage * 2);
                    damageScore += damageMana.damage * 2;//ダメージスコアを加算
                    dmgCh = true;
                   // Debug.Log(damageMana.damage  + "のダメージを与えた");
                }
            }
        }
        else if (other.gameObject.tag == "GreenSphere")
        {
            if (damageMana != null)
            {
                Damage(damageMana.damage);
                damageScore += damageMana.damage;// ダメージスコアを加算
                dmgCh = true;
                hit = 0;
                playerController.Gethit = hit;//コンボ数のリセット
               
               // Debug.Log(damageMana.damage  + "のダメージを与えた");
            }
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
       
       // Debug.Log("ドラゴンの残りHP" + hp);
        
    }

    void DieEffect()
    {
        // エフェクトを発生
        GameObject dieEffect = Instantiate(disappearanceEffct) as GameObject;
        //エフェクトの位置を指定 
        dieEffect.transform.position = this.transform.position;
        Destroy(dieEffect, 2f);
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
        attackHitCollider.enabled = true;

    }
    public void AttackHitFinishe()
    {
        agent.speed = 1.3f;
        agent.angularSpeed = 120;
        attackHitCollider.enabled = false;
     
    }
}
