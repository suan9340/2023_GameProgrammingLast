using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Pool;

public class GameManager : MonoBehaviour
{
    #region Pool
    /*
    public GameObject bulletPrefab; // 프리팹화된 탄환
    public GameObject spawnPoint; // 생성 지점
    private int poolSize = 100; // 풀 크기
    private float coolDown = 2f, coolDownCounter; // 생성 쿨타임
    private List<GameObject> pools = new List<GameObject>(); // 풀

    void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.gameObject.SetActive(false);
            pools.Add(bullet);
        } // 풀 초기화

        coolDownCounter = coolDown;
    }

    void Update()
    {
        coolDownCounter -= Time.deltaTime;

        if (coolDownCounter < 0)
        {
            for (int i = 0; i < poolSize; i++)
            {
                if (!pools[i].activeInHierarchy) // 하이라키 창에 pools[i]가 비활성화일 때
                {
                    pools[i].AddComponent<Rigidbody>(); // 속도를 주기 위해 RigidBody 추가
                    pools[i].transform.position = spawnPoint.transform.position;
                    pools[i].transform.rotation = spawnPoint.transform.rotation;
                    pools[i].SetActive(true); // 탄환 사용
                    pools[i].GetComponent<Rigidbody>().velocity = -transform.right; // 방향
                    break;
                }
            }
            coolDownCounter = coolDown;
        }
    }
    */
    #endregion

  

}
