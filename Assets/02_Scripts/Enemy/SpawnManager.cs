using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public bool enableSpawn = false;
    public GameObject Enemy; //Prefab�� ���� public ���� �Դϴ�.
    void SpawnEnemy()
    {
        float randomX = Random.Range(-25f, 25f); 
        float randomY = Random.Range(-17f, 17f);

        if (enableSpawn)
        {
            GameObject enemy = (GameObject)Instantiate(Enemy, new Vector2(randomX, randomY), Quaternion.identity);
            //������ ��ġ��, ȭ�� ���� ������ Enemy�� �ϳ� �������ݴϴ�.
        }
    }
    void Start()
    {
        InvokeRepeating("SpawnEnemy", 1, 10); //3���� ����, SpawnEnemy�Լ��� 1�ʸ��� �ݺ��ؼ� ���� ��ŵ�ϴ�.
    }
    void Update()
    {

    }
}