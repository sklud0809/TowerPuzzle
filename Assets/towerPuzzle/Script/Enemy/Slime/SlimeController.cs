using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class SlimeController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator anim;
    public GameObject house;
    private int hp = 100;
    private Vector3 housePosition;

    private Rigidbody rb;
    private PlayerController playerController;
    private TimeManager tm;//TimeManagerのスクリプト取得の宣言
    private int hit;
    public Collider attackCollider;
    public Collider attackHitCollider;
    private float cooldown = 1f;//攻撃後のクールタイム
    float timer = 0.0f;//攻撃後のタイマー
   
   
    public GameObject disappearanceEffct; //死亡後のエフェクト
    float dieTimer = 0.0f; //死亡後のタイマー
    public bool dmgCh = false;
    public int damageScore;


    private float xMinPosition = -25;//X座標の最小値
    private float xMaxPosition = -23;//x座標の最大値
    private float zMinPosition = -2f;//Z座標の最小値  
    private float zMaxPosition = 2f;//Z座標の最大値


    void Start()
    {
        //ナビメッシュ取得
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        attackHitCollider.enabled = false;

        //プレハブから生成されたときに　シーン上にいるPlayerの中のPlayerControllerを取得している
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        house.transform.position = GetRandomPosition();

        tm = GameObject.Find("TimeManager").GetComponent<TimeManager>();//TimeManagerのスクリプトを取得

    }


    void Update()
    {
        
        //家に向けて進行
        agent.destination = house.transform.position;
        //移動のアニメーションを設定
        anim.SetFloat("Speed", agent.remainingDistance);

        if (attackCollider.enabled == false)
        {
            // Debug.Log("クールダウン開始");
            timer += Time.deltaTime;
            if (timer >= cooldown)
            {
                attackCollider.enabled = true;
                timer = 0.0f;
                //   Debug.Log("クールダウン終了");
            }
        }

        //死亡アニメーション中に時間を測定
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Die"))
        {
            dieTimer += Time.deltaTime;
            attackHitCollider.enabled = false;
            agent.destination = this. transform.position;
            agent.speed = 0;
            if (dieTimer >= 2f)
            {
                DieEffect();
                dieTimer = 0.0f;
                Destroy(this.gameObject);
                
            }
        }

        //ゲームクリア時に攻撃をさせない
         if (tm.countDown <= 0)
        {
            attackCollider.enabled = false;
        }
    }

    //属性オブジェクトに当たった時に起きる
    private void OnTriggerEnter(Collider other)
    {
        SphereController damageMana = other.GetComponent<SphereController>();
        hit = playerController.Gethit;　//hitの中にプレイヤーのコンボ数を入れる。

        if (other.gameObject.tag == "RedSphere")
        {
            if (damageMana != null)
            {
                Damage(damageMana.damage);
                damageScore += damageMana.damage;
                dmgCh = true;
                hit = 0;
                playerController.Gethit = hit;//コンボ数のリセット
               // Debug.Log(damageMana.damage / 2 + "のダメージを与えた");
            }
        }
        else if (other.gameObject.tag == "BlueSphere")
        {
            if (damageMana != null)
            {
                Damage(damageMana.damage / 2);
                damageScore += damageMana.damage / 2;
                dmgCh = true;
                hit = 0;
                playerController.Gethit = hit;//コンボ数のリセット
            }
              
        }
        else if (other.gameObject.tag == "GreenSphere")
        {
            if (damageMana != null)
            {
                hit++;
                playerController.Gethit = hit;
                for (int i = 0; i <= hit; i++)
                {
                    Damage(damageMana.damage * 2 );
                    damageScore += damageMana.damage;
                    dmgCh = true;
                  //  Debug.Log(damageMana.damage * 2 + "のダメージを与えた");
                }
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
        //Debug.Log("スライムの残りHP" + hp);
    }


    void DieEffect()
    {
        // エフェクトを発生
        GameObject dieEffect = Instantiate(disappearanceEffct) as GameObject;
        //エフェクトの位置を指定 
        dieEffect.transform.position = this.transform.position;
        Destroy(dieEffect, 2f);
    }

    private Vector3 GetRandomPosition()
    {
        //それぞれの座標をランダムに生成する
        float x = Random.Range(xMinPosition, xMaxPosition);
        float z = Random.Range(zMinPosition, zMaxPosition);

        //Vector3型のPositionを返す
        return new Vector3(x, 0, z);
    }

    // 以下攻撃関連処理
    public void Attack()//子オジェクトのAttackCollierについているスクリプトに使用
    {
        anim.SetTrigger("Attack");//アタックアニメーションの再生
        agent.speed = 0;
        agent.angularSpeed = 0;
    }
    //アニメーター（Ctrl+6）の攻撃モーションにコライダーの操作
    public void AttackHitStart()
    {
        attackHitCollider.enabled = true;
        attackCollider.enabled = false;
    }
    public void AttackHitFinishe()
    {
        attackHitCollider.enabled = false;
        agent.speed = 3f;
        agent.angularSpeed = 120;
    }
}
