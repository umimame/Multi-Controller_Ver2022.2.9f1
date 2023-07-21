using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject player;
    [SerializeField] private Vector3 posOffset;


    private void Update()
    {
        transform.position = player.transform.position + posOffset;
    }
}
