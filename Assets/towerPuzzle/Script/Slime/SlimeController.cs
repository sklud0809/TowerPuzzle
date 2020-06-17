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

    private PlayerController playerController;
    private int hit;
    public Collider attackHitCollider;

    public GameObject disappearanceEffct; //死亡後のエフェクト
    float dieTimer = 0.0f; //死亡後のタイマー

    void Start()
    {
        //ナビメッシュ取得
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        attackHitCollider.enabled = false;

        //プレハブから生成されたときに　シーン上にいるPlayerの中のPlayerControllerを取得している
        playerController = GameObject.Find("PlayerPrefab").GetComponent<PlayerController>();
    }


    void Update()
    {
        //家に向けて進行
        agent.destination = house.transform.position;
        //移動のアニメーションを設定
        anim.SetFloat("Speed", agent.remainingDistance);

        //死亡アニメーション中に時間を測定
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Die"))
        {
            dieTimer += Time.deltaTime;
            agent.destination = this. transform.position;
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
        SphereController damageMana = other.GetComponent<SphereController>();
        hit = playerController.Gethit;　//hitの中にプレイヤーのコンボ数を入れる。

        if (other.gameObject.tag == "RedSphere")
        {
            if (damageMana != null)
            {
                Damage(damageMana.damage / 2);
                Debug.Log(damageMana.damage / 2 + "のダメージを与えた");
            }
        }
        else if (other.gameObject.tag == "BlueSphere")
        {
            hit = 0;
            playerController.Gethit = hit;//コンボ数のリセット
        }
        else if (other.gameObject.tag == "GreenSphere")
        {
            if (damageMana != null)
            {
                hit++;
                playerController.Gethit = hit;
                for (int i = 1; i <= hit; i++)
                {
                    Damage(damageMana.damage * i);
                    //damageScore += damageMana.damage;
                    //dmgCh = true;
                    Debug.Log(damageMana.damage * i + "のダメージを与えた");
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
        Debug.Log("スライムの残りHP" + hp);
    }


    void DieEffect()
    {
        // エフェクトを発生
        GameObject dieEffect = Instantiate(disappearanceEffct) as GameObject;
        //エフェクトの位置を指定 
        dieEffect.transform.position = this.transform.position;
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

    }
    public void AttackHitFinishe()
    {
        attackHitCollider.enabled = false;
        agent.speed = 3.5f;
        agent.angularSpeed = 120;
    }
}
