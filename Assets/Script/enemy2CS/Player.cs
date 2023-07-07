using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        move();
    }
    private void move()
    {
        //前
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            this.transform.Translate(0.0f, 0.0f, 0.01f);
        }
        //後ろ
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            this.transform.Translate(0.0f, 0.0f, -0.01f);
        }
        //右
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.Translate(0.01f, 0.0f, 0.0f);
        }
        //左
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.Translate(-0.01f, 0.0f, 0.0f);
        }
        //上
        if (Input.GetKey(KeyCode.Space))
        {
            this.transform.Translate(0.0f, 0.01f, 0.0f);
        }
        //下
        if (Input.GetKey(KeyCode.LeftShift))
        {
            this.transform.Translate(0.0f, -0.01f, 0.0f);
        }
    }
}

