using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private float mx;
    [SerializeField] private float my;
    public GameObject player;
    [SerializeField] private float distance;
    [SerializeField] private Vector3 offset;
    private void Update()
    {
        PlayerCamera();
    }

    private void PlayerCamera()
    {

        mx += Input.GetAxis("Mouse X");
        my -= Input.GetAxis("Mouse Y");

        Quaternion rotation = Quaternion.Euler(my, mx, 0);
        transform.position = player.transform.position + (rotation * new Vector3(0, 0, distance));

        transform.LookAt(player.transform.position);    // �w�肵���I�u�W�F�N�g�̕���������
        transform.eulerAngles += new Vector3(0, 0, -transform.eulerAngles.z);   // �J�������X���Ȃ��悤�ɕύX
    }
}
