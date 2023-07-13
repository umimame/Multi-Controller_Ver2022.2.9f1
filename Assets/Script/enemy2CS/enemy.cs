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
    // ���g��Transform
    [SerializeField] private Transform _self;

    // �^�[�Q�b�g��Transform
    [SerializeField] private Transform _target;

    // �O���̊�ƂȂ郍�[�J����ԃx�N�g��
    [SerializeField] private Vector3 _forward = Vector3.forward;

    [SerializeField]
    [Tooltip("����������G�t�F�N�g(�p�[�e�B�N��)")]
    private ParticleSystem particle;
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("������");
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
        // �����O�i
        //transform.position += transform.forward * speed;
        if (movejudge)
        {
            transform.position += transform.forward * speed;
        }
    }
    void RotateSelf()
    {
        // �^�[�Q�b�g�ւ̌����x�N�g���v�Z
        var dir = _target.position - _self.position;
        // �^�[�Q�b�g�̕����ւ̉�]
        var lookAtRotation = Quaternion.LookRotation(dir, Vector3.up);
        // ��]�␳
        var offsetRotation = Quaternion.FromToRotation(_forward, Vector3.forward);
        // ��]�␳���^�[�Q�b�g�����ւ̉�]�̏��ɁA���g�̌����𑀍삷��
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
        //attackjudge��false�ɂȂ�܂őҋ@
        yield return new WaitWhile(() => attackjudge);
        RotateSelf();
    }
    void StartParticle()
    {
        // �����������肪"Player"�^�O�������Ă�����
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
            // �p�[�e�B�N���V�X�e���̃C���X�^���X�𐶐�����B
            ParticleSystem newParticle = Instantiate(particle);
            // �p�[�e�B�N���̔����ꏊ�����̃X�N���v�g���A�^�b�`���Ă���GameObject�̏ꏊ�ɂ���B
            newParticle.transform.position = this.transform.position;
            // �p�[�e�B�N���𔭐�������B
            newParticle.Play();
            // �C���X�^���X�������p�[�e�B�N���V�X�e����GameObject���폜����B(�C��)
            // ����������newParticle�����ɂ���ƃR���|�[�l���g�����폜����Ȃ��B
            Destroy(newParticle.gameObject, 1.0f);
            particlejudge = false;
        }
    }
}