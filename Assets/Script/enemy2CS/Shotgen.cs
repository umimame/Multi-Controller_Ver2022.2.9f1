using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class shotgen : MonoBehaviour
{

    public GameObject shotPrefab;
    bool canshot;
    private int count;
    void OnCollisionEnter(Collision collision)
    {
        canshot = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        canshot = false;
    }

    // Update is called once per frame
    void Update()
    {
        //shotinstance();

        count++;
        if (count % 60 == 0)
        {
            //test();
        }
    }

    void shotinstance()
    {
        if (canshot)
        {
            GameObject shot = Instantiate(shotPrefab, transform.position, Quaternion.identity);
            Rigidbody shotRb = shot.GetComponent<Rigidbody>();

            // íeë¨ÇÕé©óRÇ…ê›íË
            shotRb.AddForce(transform.forward * 500);


            // ÇTïbå„Ç…ñCíeÇîjâÛÇ∑ÇÈ
            Destroy(shot, 5.0f);
        }
    }
    void test()
    {
        GameObject shot = Instantiate(shotPrefab, transform.position, Quaternion.identity);
        Rigidbody shotRb = shot.GetComponent<Rigidbody>();

        // íeë¨ÇÕé©óRÇ…ê›íË
        shotRb.AddForce(transform.forward * 500);


        // ÇTïbå„Ç…ñCíeÇîjâÛÇ∑ÇÈ
        Destroy(shot, 5.0f);
    }
}