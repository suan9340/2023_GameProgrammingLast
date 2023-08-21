using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public PlayerMove playerMoveScript;

    public bool enableSpawn = false;
    float safeDistance = 2f;

    void Start()
    {
        InvokeRepeating("SpawnEnemy", 1, 5);
    }

    void SpawnEnemy()
    {
        float randomX = Random.Range(-25f, 25f);
        float randomY = Random.Range(-17f, 17f);

        Vector3 playerPos = playerMoveScript.transform.position;
        Vector3 enemyPos = new Vector2(randomX, randomY);

        // 적이 나오는 위치 = 플레이어 위치 + 방향 * 안전거리 * 랜덤(1.0f, 1.5f);
      
        // 아래 if문 작동 안됨. 고칠 것!
        if (Vector3.Distance(playerPos, enemyPos) < safeDistance && enableSpawn)
        {
            Vector3 dir = playerPos - enemyPos;
            dir.Normalize();
            enemyPos = playerPos + dir * safeDistance * Random.Range(1f, 1.5f);
            GameObject enemy = (GameObject)Instantiate(enemyPrefab, enemyPos, Quaternion.identity);
            enemy.GetComponent<EnemyFSM>().EnemyInit(playerMoveScript);
        }
        else
        {
            GameObject enemy = (GameObject)Instantiate(enemyPrefab, enemyPos, Quaternion.identity);
            enemy.GetComponent<EnemyFSM>().EnemyInit(playerMoveScript);
        }

    }
}