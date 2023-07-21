using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private Parameter speed = new Parameter();
    [SerializeField] private Engine engine;
    [SerializeField] private MoveByCameraForward move;
<<<<<<< HEAD
<<<<<<< HEAD
    
>>>>>>> e4c41e2d (Playerのクラスを整備。)
    [SerializeField] private Vector3 cameraDirection;
>>>>>>> 95f1cfdedd25eb7ff6977e479135cf4f219c2e6d
    private void Start()
    {
        speed.Set();

        engine = gameObject.AddComponent<Engine>();
        engine.SpeedSet(speed);

        move = gameObject.AddComponent<MoveByCameraForward>();
        move.Set(engine);
    }

    private void Update()
    {
        engine.UpdateOnAction(move.Update);
<<<<<<< HEAD
<<<<<<< HEAD
>>>>>>> e4c41e2d (Playerのクラスを整備。)
        cameraDirection = Camera.main.transform.position - transform.position;
        transform.eulerAngles = new Vector3(0.0f, cameraDirection.y, 0.0f);
>>>>>>> 95f1cfdedd25eb7ff6977e479135cf4f219c2e6d
    }

    private void InputMove()
    {
        if (Input.GetKey(KeyCode.W))
        {
            engine.moveSchedule += new Vector3(0, 0, speed.entity);
        }
        if (Input.GetKey(KeyCode.S))
        {
            engine.moveSchedule += new Vector3(0, 0, -speed.entity);
        }
        if (Input.GetKey(KeyCode.A))
        {
            engine.moveSchedule += new Vector3(-speed.entity, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            engine.moveSchedule += new Vector3(speed.entity, 0, 0);
        }
    }
}
