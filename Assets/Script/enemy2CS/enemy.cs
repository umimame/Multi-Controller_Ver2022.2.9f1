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
    private int hp;//���g��hp
    public int score;//�|�����Ƃ��Ƀv���C���[�ɉ��Z�����X�R�A
>>>>>>> 95f1cfdedd25eb7ff6977e479135cf4f219c2e6d
    public int attackdistance;//�U���J�n����
    public float moveSpeed;//�ʏ�ړ����̃X�s�[�h
    public float attackspeed;//�U�����̃X�s�[�h
    public float distance;//���g�ƃ^�[�Q�b�g�̋���
<<<<<<< HEAD
    public bool movejudge;//�ړ�����
    public bool particlejudge;//�p�[�e�B�N���o�����̔���
    public bool attackjudge;//�U�����邩����
    public bool onetime;//��񂾂����s�p��
    public bool atonetime;//�U���������s�p
>>>>>>> 95f1cfdedd25eb7ff6977e479135cf4f219c2e6d
    private Vector3 _forwardDirection = Vector3.forward;
    // ���g��Transform
    [SerializeField] private Transform _self;

    // �^�[�Q�b�g��Transform
    [SerializeField] private Transform _target;
<<<<<<< HEAD

   // public GameObject _target;
>>>>>>> 95f1cfdedd25eb7ff6977e479135cf4f219c2e6d
    // �O���̊�ƂȂ郍�[�J����ԃx�N�g��
    [SerializeField] private Vector3 _forward = Vector3.forward;

    [SerializeField]
    [Tooltip("����������G�t�F�N�g(�p�[�e�B�N��)")]
    private ParticleSystem particle;


    //�e�X�g�p

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("������");
        movejudge = false;
        //�v���C���[�^�O���t�������̂ɐڐG�����玩�g��j�󂷂�
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

        // �v���C���[�̃I�u�W�F�N�g���������A���� Transform �R���|�[�l���g�� _target �Ɋi�[
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
        //RotateSelf��UpDate�Ŏ�������ƍU�������v���C���[�̕�����������ǔ��U���ɂȂ�
        //RotateSelf();

        Move();
<<<<<<< HEAD
        Attack();
        Dmg();
       //�U������ɂ��悤����A���g�̃^�[�Q�b�g�Ɏw�肵���Ώۂ̋����𑪂�
>>>>>>> 95f1cfdedd25eb7ff6977e479135cf4f219c2e6d
        distance = Vector3.Distance(_self.position, _target.position);

        //StartParticle();

    }


    //movejude��true�̊Ԏ��g�̌����i�v���C���[�̂�������j�Ɉړ�
    //distance��attackdistance�����ɂȂ�����movejudge��false�ɂ���
    void Move()
    {
<<<<<<< HEAD
        if (distance < attackdistance)//���g�ƃ^�[�Q�b�g�̋������P�O�����Ȃ�
        {
            movejudge = false;
        }
        else { StartCoroutine(DelayCoroutine()); }//�P�b�҂���movejudge��true�ɂ���
>>>>>>> 95f1cfdedd25eb7ff6977e479135cf4f219c2e6d
        if (movejudge)
        {
            Vector3 moveDirection = transform.TransformDirection(_forwardDirection);
            rb.velocity = moveDirection * moveSpeed;
        }
    }
<<<<<<< HEAD
    //�v���C���[�̕���������
>>>>>>> 95f1cfdedd25eb7ff6977e479135cf4f219c2e6d
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
    //���g�ƃv���C���[������attackdistance�����ɂȂ�����ːi
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
            //attackjudge��false�ɂ���
            StartCoroutine(DelayAttackCoroutine());
        }
    }
    //G����������hp-10,hp���O�ɂȂ����玩�g��j��
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
    //�P�b�҂���movejudge��true�ɂ���
    private IEnumerator DelayCoroutine()
    {
        yield return new WaitForSeconds(1);
        movejudge = true;
    }
    //1�b�҂���attackjudge��false�ɂ���
    private IEnumerator DelayAttackCoroutine()
    {
        yield return new WaitForSeconds(1);
        attackjudge = false;
    }
    //�P�b�҂���particlejudge��false�ɂ���
    private IEnumerator OneSecDelayCoroutine()
    {
        yield return new WaitForSeconds(1);
        particlejudge = false;
    }
    //attackjudge��false�ɂȂ�܂�RotateSelf�𒆒f
    private IEnumerator Interruption()
    {
        //attackjudge��false�ɂȂ�܂őҋ@
        yield return new WaitWhile(() => attackjudge);
        RotateSelf();
    }
    void StartParticle()
    {
        // �G�Ƃ̋�����10�����Ȃ�
        if (distance < 10.0f)
        {
            if (onetime)
            {
                particlejudge = true;
                onetime = false;
            }
        }
        //�v���C���[�Ƃ̋�����10�ȏ�Ȃ�
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
            StartCoroutine(OneSecDelayCoroutine());
            //particlejudge = false;
        }
    }


    IEnumerator GenerateParticle()
    {
        while (true)
        {
<<<<<<< HEAD
            // �G�Ƃ̋�����10������particlejudge��true�Ȃ�
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