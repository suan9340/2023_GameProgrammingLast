using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    private PlayerMove player;

    public float moveSpeed = 10.0f;

    public IObjectPool<GameObject> Pool { get; set; }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(ConstantManager.TAG_BORDER))
        {
            BulletPoolManager.instance.PoolReturn(this);
        }

        if (collision.gameObject.CompareTag(ConstantManager.TAG_ENEMY))
        {
            BulletPoolManager.instance.PoolReturn(this);
        }
    }

    public void Init(PlayerMove owner)
    {
        player = owner;
    }
}
