using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shot : MonoBehaviour
{

    //[SerializeField] private Transform _self;

    //// �^�[�Q�b�g��Transform
    //[SerializeField] private Transform _target;

    //// �O���̊�ƂȂ郍�[�J����ԃx�N�g��
    //[SerializeField] private Vector3 _forward = Vector3.forward;


    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 0.02f;
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }
    private void move()
    {
        // �����O�i
        //transform.position += transform.forward * Time.deltaTime;
        transform.position += transform.forward * speed;

    }

}

