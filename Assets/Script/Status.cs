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
/// Charaクラスに宣言する<br/>
/// level:レベル<br/>
/// increment:レベル毎の増分<br/>
/// basic:ベースになる数値<br/>
/// enitty:実際の数値<br/>
/// maxEntity:最高値<br/>
/// minEntity:最低値<br/>
/// インスペクタから入力が必要な変数{<br/>
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
    /// Parametern内のlevelを引数と同じにする
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
    /// ParameterのUpdate<br/>
    /// trueでentityが0を下回る<br/>
    /// falseでminEntityを最低値にする<br/>
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
/// レベルの概念<br/>
/// now:現在レベル<br/>
/// max:最高レベル<br/>
/// </summary>
[Serializable] public class Level
{
    public int now;
    public int max;

    /// <summary>
    /// nowがmaxを上回らないようにする
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
    /// 宣言時にParameter型の速さを入れる
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
    /// Update関数内で行われる処理を記述する
    /// </summary>
    protected virtual void FunctionInUpdate() 
    {
        Debug.Log("FunctionInUpdate");
    }
}
