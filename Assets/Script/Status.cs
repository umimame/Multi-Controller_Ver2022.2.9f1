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
    public Level level;
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
        Debug.Log("set");
    }

    /// <summary>
    /// Parameter��Update<br/>
    /// true��entity��0�������<br/>
    /// false��minEntity���Œ�l�ɂ���<br/>
    /// </summary>
    /// <param name="belowZero"></param>
    public void Update(bool belowZero)
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
    [SerializeField] private Vector3 moveSchedule;
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private float time;
    public Parameter speed;
    
    /// <summary>
    /// �錾����Parameter�^�̑���������
    /// </summary>
    /// <param name="inSpeed"></param>
    public void Set(Parameter inSpeed)
    {
        speed = inSpeed;
    }
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    private void Update()
    {
        moveSchedule = new Vector3(0, 0, 0);

        FunctionInUpdate();

        rb.velocity = moveSchedule.normalized * speed.entity;
    }

    /// <summary>
    /// Update�֐����ōs���鏈�����L�q����
    /// </summary>
    protected virtual void FunctionInUpdate() 
    {
        Debug.Log("FunctionInUpdate");
    }
}
