using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class Enemy : MonoBehaviour
{
    private Rigidbody rb;
<<<<<<< HEAD
    private int hp;//自身のhp
    public int score;//倒したときにプレイヤーに加算されるスコア
>>>>>>> 95f1cfdedd25eb7ff6977e479135cf4f219c2e6d
    public int attackdistance;//攻撃開始距離
    public float moveSpeed;//通常移動時のスピード
    public float attackspeed;//攻撃時のスピード
    public float distance;//自身とターゲットの距離
<<<<<<< HEAD
    public bool movejudge;//移動判定
    public bool particlejudge;//パーティクル出すかの判定
    public bool attackjudge;//攻撃するか判定
    public bool onetime;//一回だけ実行用の
    public bool atonetime;//攻撃を一回実行用
>>>>>>> 95f1cfdedd25eb7ff6977e479135cf4f219c2e6d
    private Vector3 _forwardDirection = Vector3.forward;
    // 自身のTransform
    [SerializeField] private Transform _self;

    // ターゲットのTransform
    [SerializeField] private Transform _target;
<<<<<<< HEAD

   // public GameObject _target;
>>>>>>> 95f1cfdedd25eb7ff6977e479135cf4f219c2e6d
    // 前方の基準となるローカル空間ベクトル
    [SerializeField] private Vector3 _forward = Vector3.forward;

    [SerializeField]
    [Tooltip("発生させるエフェクト(パーティクル)")]
    private ParticleSystem particle;


    //テスト用

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("あたり");
        movejudge = false;
        //プレイヤータグが付いたものに接触したら自身を破壊する
        if (collision.gameObject.CompareTag("player"))
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        //StartCoroutine(DelayCoroutine());

    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        hp = 100;
        score = 100;
        attackdistance = 10;
        moveSpeed = 10.0f;
        attackspeed = 15.0f;

        movejudge = true;
        particlejudge = true;
        onetime = true;
        atonetime = true;
        attackjudge = false;


        StartCoroutine(GenerateParticle());
<<<<<<< HEAD

        // プレイヤーのオブジェクトを検索し、その Transform コンポーネントを _target に格納
        GameObject player = GameObject.FindGameObjectWithTag("player");
        if (player != null)
        {
            _target = player.transform;
        }
>>>>>>> 95f1cfdedd25eb7ff6977e479135cf4f219c2e6d


    }
    // Update is called once per frame
    void Update()
    {
        //RotateSelfをUpDateで実装すると攻撃中もプレイヤーの方を向くから追尾攻撃になる
        //RotateSelf();

        Move();
<<<<<<< HEAD
        Attack();
        Dmg();
       //攻撃判定にしようする、自身のターゲットに指定した対象の距離を測る
>>>>>>> 95f1cfdedd25eb7ff6977e479135cf4f219c2e6d
        distance = Vector3.Distance(_self.position, _target.position);

        //StartParticle();

    }


    //movejudeがtrueの間自身の向き（プレイヤーのいる方向）に移動
    //distanceがattackdistance未満になったらmovejudgeをfalseにする
    void Move()
    {
<<<<<<< HEAD
        if (distance < attackdistance)//自身とターゲットの距離が１０未満なら
        {
            movejudge = false;
        }
        else { StartCoroutine(DelayCoroutine()); }//１秒待ってmovejudgeをtrueにする
>>>>>>> 95f1cfdedd25eb7ff6977e479135cf4f219c2e6d
        if (movejudge)
        {
            Vector3 moveDirection = transform.TransformDirection(_forwardDirection);
            rb.velocity = moveDirection * moveSpeed;
        }
    }
<<<<<<< HEAD
    //プレイヤーの方向を向く
>>>>>>> 95f1cfdedd25eb7ff6977e479135cf4f219c2e6d
    void RotateSelf()
    {
        // ターゲットへの向きベクトル計算
        var dir = _target.position - _self.position;
        // ターゲットの方向への回転
        var lookAtRotation = Quaternion.LookRotation(dir, Vector3.up);
        // 回転補正
        var offsetRotation = Quaternion.FromToRotation(_forward, Vector3.forward);
        // 回転補正→ターゲット方向への回転の順に、自身の向きを操作する
        _self.rotation = lookAtRotation * offsetRotation;

    }
    //自身とプレイヤー距離がattackdistance未満になったら突進
    void Attack()
    {
        StartCoroutine(Interruption());
        if (distance < attackdistance)
        {
            if (atonetime)
            {
                attackjudge = true;
                atonetime = false;
            }
        }
        else { atonetime = true; }
        if (attackjudge)
        {
            Vector3 moveDirection = transform.TransformDirection(_forwardDirection);
            rb.velocity = moveDirection * attackspeed;
            //attackjudgeをfalseにする
            StartCoroutine(DelayAttackCoroutine());
        }
    }
    //Gを押したらhp-10,hpが０になったら自身を破壊
    void Dmg()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            hp -= 10;
            Debug.Log(this.hp);
        }
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
    //１秒待ってmovejudgeをtrueにする
    private IEnumerator DelayCoroutine()
    {
        yield return new WaitForSeconds(1);
        movejudge = true;
    }
    //1秒待ってattackjudgeをfalseにする
    private IEnumerator DelayAttackCoroutine()
    {
        yield return new WaitForSeconds(1);
        attackjudge = false;
    }
    //１秒待ってparticlejudgeをfalseにする
    private IEnumerator OneSecDelayCoroutine()
    {
        yield return new WaitForSeconds(1);
        particlejudge = false;
    }
    //attackjudgeがfalseになるまでRotateSelfを中断
    private IEnumerator Interruption()
    {
        //attackjudgeがfalseになるまで待機
        yield return new WaitWhile(() => attackjudge);
        RotateSelf();
    }
    void StartParticle()
    {
        // 敵との距離が10未満なら
        if (distance < 10.0f)
        {
            if (onetime)
            {
                particlejudge = true;
                onetime = false;
            }
        }
        //プレイヤーとの距離が10以上なら
        else { onetime = true; }

        if (particlejudge)
        {
            // パーティクルシステムのインスタンスを生成する。
            ParticleSystem newParticle = Instantiate(particle);
            // パーティクルの発生場所をこのスクリプトをアタッチしているGameObjectの場所にする。
            newParticle.transform.position = this.transform.position;
            // パーティクルを発生させる。
            newParticle.Play();
            // インスタンス化したパーティクルシステムのGameObjectを削除する。(任意)
            // ※第一引数をnewParticleだけにするとコンポーネントしか削除されない。
            Destroy(newParticle.gameObject, 1.0f);
            StartCoroutine(OneSecDelayCoroutine());
            //particlejudge = false;
        }
    }


    IEnumerator GenerateParticle()
    {
        while (true)
        {
<<<<<<< HEAD
            // 敵との距離が10未満でparticlejudgeがtrueなら
>>>>>>> 95f1cfdedd25eb7ff6977e479135cf4f219c2e6d
            if (distance < attackdistance && particlejudge)
            {
                GenerateNewParticle();
                particlejudge = false;
                yield return new WaitForSeconds(1.0f);
                particlejudge = true;
            }
            yield return null;
        }
    }

    void GenerateNewParticle()
    {
        ParticleSystem newParticle = Instantiate(particle, transform.position, Quaternion.identity);
        newParticle.Play();
        Destroy(newParticle.gameObject, 1.0f);
    }

}