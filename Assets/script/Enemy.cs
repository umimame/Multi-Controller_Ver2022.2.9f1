using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    //[SerializeField] private PlayerController _playerController;
    private NavMeshAgent _agent;
    private RaycastHit[] _raycastHits = new RaycastHit[10];
    private int count;
    // Start is called before the first frame update
    [SerializeField]
    [Tooltip("�e�̔��ˏꏊ")]
    private GameObject firingPoint;

    [SerializeField]
    [Tooltip("�e")]
    private GameObject bullet;

    [SerializeField]
    [Tooltip("�e�̑���")]
    private float speed = 30f;
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Destroy(gameObject);
        }
    }
    public void OnDetectObject(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            var positionDiff = collider.transform.position - transform.position;
            var distance = positionDiff.magnitude;
            var direction = positionDiff.normalized;
            var hitCount = Physics.RaycastNonAlloc(transform.position, direction, _raycastHits, distance);
            Debug.Log("hitCount:" + hitCount);
            if (hitCount == 1)
            {
                _agent.isStopped = false;
                _agent.destination = collider.transform.position;
            }
            else
            {
                _agent.isStopped = true;
            }
            count += 1;
            if (count % 60 == 0)
            {
                LauncherShot();
            }
        }
    }
    private void LauncherShot()
    {
        // �e�𔭎˂���ꏊ���擾
        Vector3 bulletPosition = firingPoint.transform.position;
        // ��Ŏ擾�����ꏊ�ɁA"bullet"��Prefab���o��������
        GameObject newBall = Instantiate(bullet, bulletPosition, transform.rotation);
        // �o���������{�[����forward(z������)
        Vector3 direction = newBall.transform.forward;
        // �e�̔��˕�����newBall��z����(���[�J�����W)�����A�e�I�u�W�F�N�g��rigidbody�ɏՌ��͂�������
        newBall.GetComponent<Rigidbody>().AddForce(direction * speed, ForceMode.Impulse);
        // �o���������{�[���̖��O��"bullet"�ɕύX
        newBall.name = bullet.name;
        // �o���������{�[����0.8�b��ɏ���
        Destroy(newBall, 0.8f);
    }

}
