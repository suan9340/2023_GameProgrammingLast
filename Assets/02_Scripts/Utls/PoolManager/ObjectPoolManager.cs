using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolManager : MonoBehaviour
{
   

    public int defaultCapacity = 10;
    public int maxPoolSize = 15;

    public GameObject bulletPrefab;
    public GameObject pooling;

    public static ObjectPoolManager instance;

    public IObjectPool<GameObject> Pool { get; private set; }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
        Init();
    }

    private void Init()
    {
        Pool = new ObjectPool<GameObject>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool,
        OnDestroyPoolObject, true, defaultCapacity);

        // �̸� ������Ʈ ���� �س���
        for (int i = 0; i < defaultCapacity; i++)
        {
            Bullet bullet = CreatePooledItem().GetComponent<Bullet>();
            bullet.Pool.Release(bullet.gameObject);
        }
    }

    // ����
    private GameObject CreatePooledItem()
    {
        GameObject poolGo = Instantiate(bulletPrefab);

        poolGo.transform.SetParent(pooling.transform, false);


        poolGo.GetComponent<Bullet>().Pool = this.Pool;
        return poolGo;
    }

    // ���
    private void OnTakeFromPool(GameObject poolGo)
    {
        poolGo.SetActive(true);
    }

    // ��ȯ
    private void OnReturnedToPool(GameObject poolGo)
    {
        poolGo.SetActive(false);
    }

    // ����
    private void OnDestroyPoolObject(GameObject poolGo)
    {
        Destroy(poolGo);
    }
}