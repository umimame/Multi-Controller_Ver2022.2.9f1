using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemygenerator : MonoBehaviour
{
    public GameObject enemyPrefab; // �G�̃v���n�u��Inspector����ݒ�

    void Start()
    {
        InvokeRepeating("InstantiateEnemy", 0f, 5f);
    }

    void InstantiateEnemy()
    {
        // �G�𐶐�����ʒu�������_���Ɍ���
        Vector3 spawnPosition = new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f));

        // �G�𐶐�
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
