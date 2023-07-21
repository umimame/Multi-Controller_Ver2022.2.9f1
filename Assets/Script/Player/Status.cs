using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEditor;
using UnityEngine;

public class Status : MonoBehaviour
{

}

/// <summary>
/// Chara�N���X�ɐ錾����<br/>
/// level:���x��<br/>
/// increment:���x�����̑���<br/>
/// basic:�x�[�X�ɂȂ鐔�l<br/>
/// enitty:���ۂ̐��l<br/>
/// maxEntity:�ō��l<br/>
/// minEntity:�Œ�l<br/>
/// �C���X�y�N�^������͂��K�v�ȕϐ�{<br/>
/// level,increment,basic<br/>
/// }
/// </summary>
[Serializable] public class Parameter
{
    public Level level = new Level();
    [SerializeField] private float increment;
    [SerializeField] private float basic;
    public float entity;
    [SerializeField] private float maxEntity;
    [SerializeField] private float minEntity;

    /// <summary>
    /// Parametern����level�������Ɠ����ɂ���
    /// </summary>
    /// <param name="inLevel"></param>
    public void LevelReference(Level inLevel)
    {
        level = inLevel;
    }

    public void Set()
    {
        level.Update();
        float adjust = increment * level.now;
        entity = maxEntity = basic + adjust;
        Debug.Log(entity + "set");
    }

    /// <summary>
    /// Parameter��Update<br/>
    /// true��entity��0�������<br/>
    /// false��minEntity���Œ�l�ɂ���<br/>
    /// </summary>
    /// <param name="belowZero"></param>
    public void ParamUpdate(bool belowZero)
    {
        level.Update();
        if (belowZero)
        {
            entity = (entity <= 0) ? 0 : entity;
        }
        else
        {
            entity = (entity <= minEntity) ? minEntity : entity;
        }

        entity = (entity >= maxEntity) ? maxEntity : entity;
    }
}

/// <summary>
/// ���x���̊T�O<br/>
/// now:���݃��x��<br/>
/// max:�ō����x��<br/>
/// </summary>
[Serializable] public class Level
{
    public int now;
    public int max;

    /// <summary>
    /// now��max������Ȃ��悤�ɂ���
    /// </summary>
    public void Update()
    {
        now = (now >= max) ? max : now;
    }
}

[Serializable] public class Engine : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    public Vector3 moveSchedule;
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private float time;
    public Parameter speed;
    
    /// <summary>
    /// �錾����Parameter�^�̑���������
    /// </summary>
    /// <param name="inSpeed"></param>
    public void SpeedSet(Parameter inSpeed)
    {
        speed = new Parameter();
        speed = inSpeed;
    }
    
    public void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        Debug.Log("Engine.Start");
    }

    public void UpdateOnAction(Action action)
    {
        moveSchedule = new Vector3(0, 0, 0);

        action();

        rb.velocity = moveSchedule.normalized * speed.entity;
    }
}

[Serializable] public class MoveByCameraForward : MonoBehaviour
{
    [SerializeField] private Engine engine;
    [SerializeField] private Vector3 cameraPos;
    [SerializeField] private Vector3 cameraForward;
    [SerializeField] private Vector3 cameraToDirection;
    [SerializeField] private Vector3 moveDirection;

    public MoveByCameraForward(Engine e)
    {
        engine = e;
    }

    public void Set(Engine e)
    {
        engine = e;
    }

    public void Start()
    {
        
    }

    public void Update()
    {
        cameraPos = Camera.main.transform.position;
        moveDirection = Vector3.zero;

        cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0.0f;


        InputMove();

    }

    private void InputMove()
    {
        if (Input.GetKey(KeyCode.W))
        {
            engine.moveSchedule += Camera.main.transform.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            engine.moveSchedule -= Camera.main.transform.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            engine.moveSchedule -= Camera.main.transform.right;
        }
        if (Input.GetKey(KeyCode.D))
        {
            engine.moveSchedule += Camera.main.transform.right;
        }
    }
}