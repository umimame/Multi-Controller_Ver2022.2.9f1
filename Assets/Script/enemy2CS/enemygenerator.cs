using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemygenerator : MonoBehaviour
{
    public GameObject enemyPrefab; // 敵のプレハブをInspectorから設定

    void Start()
    {
        InvokeRepeating("InstantiateEnemy", 0f, 5f);
    }

    void InstantiateEnemy()
    {
        // 敵を生成する位置をランダムに決定
        Vector3 spawnPosition = new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f));

        // 敵を生成
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
