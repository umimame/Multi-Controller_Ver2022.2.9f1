using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shot : MonoBehaviour
{

    //[SerializeField] private Transform _self;

    //// ターゲットのTransform
    //[SerializeField] private Transform _target;

    //// 前方の基準となるローカル空間ベクトル
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
        // 自動前進
        //transform.position += transform.forward * Time.deltaTime;
        transform.position += transform.forward * speed;

    }

}

