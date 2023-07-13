using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class Enemy : MonoBehaviour
{
    private int hp;
    public int score;
    public float speed;
    public float attackspeed;
    public float distance;
    private bool movejudge;
    private bool particlejudge;
    public bool attackjudge;
    public bool onetime;
    public bool Atonetime;
    // 自身のTransform
    [SerializeField] private Transform _self;

    // ターゲットのTransform
    [SerializeField] private Transform _target;

    // 前方の基準となるローカル空間ベクトル
    [SerializeField] private Vector3 _forward = Vector3.forward;

    [SerializeField]
    [Tooltip("発生させるエフェクト(パーティクル)")]
    private ParticleSystem particle;
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("あたり");
        movejudge = false;
    }
    private void OnCollisionExit(Collision collision)
    {
        StartCoroutine(DelayCoroutine());

    }
    // Start is called before the first frame update
    void Start()
    {
        speed = 0.01f;
        attackspeed = 0.01f;
        hp = 100;
        score = 100;
        movejudge = true;
        particlejudge = false;
        onetime = true;
        Atonetime = true;
        attackjudge = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (distance <= 10)
        {
            movejudge = false;
        }
        else { movejudge = true; }
        Move();
        StartCoroutine(Interruption());
        //RotateSelf();
        Attack();
        Dmg();
        distance = Vector3.Distance(_self.position, _target.position);

        StartParticle();

    }
    private void Move()
    {
        // 自動前進
        //transform.position += transform.forward * speed;
        if (movejudge)
        {
            transform.position += transform.forward * speed;
        }
    }
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
    void Attack()
    {
        if (distance <= 10.0f)
        {
            if (Atonetime)
            {
                attackjudge = true;
                Atonetime = false;
            }
        }
        else { Atonetime = true; }
        if (attackjudge)
        {
            transform.position += transform.forward * attackspeed;
            StartCoroutine(DelayAttackCoroutine());
        }
    }
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
    private IEnumerator DelayCoroutine()
    {
        yield return new WaitForSeconds(1);
        movejudge = true;
    }
    private IEnumerator DelayAttackCoroutine()
    {
        yield return new WaitForSeconds(1);
        attackjudge = false;
    }
    private IEnumerator OneSecDelayCoroutine()
    {
        yield return new WaitForSeconds(1);

    }
    private IEnumerator Interruption()
    {
        //attackjudgeがfalseになるまで待機
        yield return new WaitWhile(() => attackjudge);
        RotateSelf();
    }
    void StartParticle()
    {
        // 当たった相手が"Player"タグを持っていたら
        if (distance <= 10.0f)
        {
            if (onetime)
            {
                particlejudge = true;
                onetime = false;
            }
        }
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
            particlejudge = false;
        }
    }
}