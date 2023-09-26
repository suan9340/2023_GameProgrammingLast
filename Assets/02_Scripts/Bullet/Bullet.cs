using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    private PlayerMove player;
    private Animator myAnim = null;
    private Rigidbody2D myrigid;
    public IObjectPool<GameObject> Pool { get; set; }

    private void Start()
    {
        myAnim = GetComponent<Animator>();
        myrigid = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(ConstantManager.TAG_BORDER))
        {
            StopBullet();
        }

        if (collision.gameObject.CompareTag(ConstantManager.TAG_ENEMY))
        {
            StopBullet();
        }

        if (collision.gameObject.CompareTag(ConstantManager.TAG_ENEMYBULLET))
        {
            Debug.Log("qwe");
        }
    }

    private void StopBullet()
    {
        myAnim.SetTrigger("isDie");
        myrigid.velocity = Vector3.zero;
        Invoke(nameof(BulletAnim), 1.34f);
    }

    private void BulletAnim()
    {
        BulletPoolManager.instance.PoolReturn(this);
    }

    public void Init(PlayerMove owner)
    {
        player = owner;
    }
}
