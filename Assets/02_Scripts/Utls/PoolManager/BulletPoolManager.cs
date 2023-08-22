using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPoolManager : MonoBehaviour
{
    //공간 공간에 넣을 물건 자기 자신
    public static BulletPoolManager instance;

    private Queue<Bullet> bulletPool = new();

    [SerializeField] private Bullet bulletPrefab;

    private void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        for (int i = 0; i < 15; i++)
        {
            Bullet obj = Instantiate(bulletPrefab, transform);

            obj.gameObject.SetActive(false);

            bulletPool.Enqueue(obj);
        }
    }
    public Bullet BulletShoot()
    {
        if (bulletPool.Count == 0)
        {
            Bullet newObj = Instantiate(bulletPrefab, transform);
            newObj.gameObject.SetActive(false);
            bulletPool.Enqueue(newObj);
        }

        Bullet obj = bulletPool.Dequeue();
        obj.gameObject.SetActive(true);
        return obj;
    }

    public void PoolReturn(Bullet obj)
    {
        obj.gameObject.SetActive(false);
        bulletPool.Enqueue(obj);
    }

}
