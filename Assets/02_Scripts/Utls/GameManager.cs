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
    public GameObject bulletPrefab; // ������ȭ�� źȯ
    public GameObject spawnPoint; // ���� ����
    private int poolSize = 100; // Ǯ ũ��
    private float coolDown = 2f, coolDownCounter; // ���� ��Ÿ��
    private List<GameObject> pools = new List<GameObject>(); // Ǯ

    void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.gameObject.SetActive(false);
            pools.Add(bullet);
        } // Ǯ �ʱ�ȭ

        coolDownCounter = coolDown;
    }

    void Update()
    {
        coolDownCounter -= Time.deltaTime;

        if (coolDownCounter < 0)
        {
            for (int i = 0; i < poolSize; i++)
            {
                if (!pools[i].activeInHierarchy) // ���̶�Ű â�� pools[i]�� ��Ȱ��ȭ�� ��
                {
                    pools[i].AddComponent<Rigidbody>(); // �ӵ��� �ֱ� ���� RigidBody �߰�
                    pools[i].transform.position = spawnPoint.transform.position;
                    pools[i].transform.rotation = spawnPoint.transform.rotation;
                    pools[i].SetActive(true); // źȯ ���
                    pools[i].GetComponent<Rigidbody>().velocity = -transform.right; // ����
                    break;
                }
            }
            coolDownCounter = coolDown;
        }
    }
    */
    #endregion

  

}
