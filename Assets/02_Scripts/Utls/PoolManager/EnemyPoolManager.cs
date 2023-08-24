using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoolManager : MonoBehaviour
{
    public PlayerMove playerMoveScript;

    public bool enableSpawn = false;
    float safeDistance = 2f;

    // 풀 매니저
    public static EnemyPoolManager instance;

    private Queue<EnemyFSM> enemyPool = new();

    [SerializeField] private EnemyFSM enemyPrefab;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        InvokeRepeating("SpawnEnemy", 1, 5);

        for (int i = 0; i < 15; i++)
        {
            EnemyFSM obj = Instantiate(enemyPrefab, transform);

            obj.gameObject.SetActive(false);

            enemyPool.Enqueue(obj);
        }
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
            EnemyFSM enemy = EnemySpawn(enemyPos);
            enemy.GetComponent<EnemyFSM>().EnemyInit(playerMoveScript);
        }
        else
        {
            EnemyFSM enemy = EnemySpawn(enemyPos);
            enemy.GetComponent<EnemyFSM>().EnemyInit(playerMoveScript);
        }

    }

    public EnemyFSM EnemySpawn(Vector3 _pos)
    {
        if (enemyPool.Count == 0)
        {
            EnemyFSM newObj = Instantiate(enemyPrefab, _pos, Quaternion.identity);
            newObj.gameObject.SetActive(false);
            enemyPool.Enqueue(newObj);
        }

        EnemyFSM obj = enemyPool.Dequeue();
        obj.transform.position = _pos;
        obj.gameObject.SetActive(true);
        return obj;
    }

    public void PoolReturn(EnemyFSM obj)
    {
        obj.gameObject.SetActive(false);
        enemyPool.Enqueue(obj);
    }
}