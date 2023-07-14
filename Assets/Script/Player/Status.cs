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
    public Level level = new Level();
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
        Debug.Log(entity + "set");
    }

    /// <summary>
    /// ParameterのUpdate<br/>
    /// trueでentityが0を下回る<br/>
    /// falseでminEntityを最低値にする<br/>
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
    public Vector3 moveSchedule;
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private float time;
    public Parameter speed;
    
    /// <summary>
    /// 宣言時にParameter型の速さを入れる
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