using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    private PlayerMove player;

    public float moveSpeed = 10.0f;
    public bool isEnemy = false;

    public IObjectPool<GameObject> Pool { get; set; }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("BORDER"))
        {
            Pool.Release(this.gameObject);
        }
        else if (collision.gameObject.CompareTag("ENEMY"))
        {
            isEnemy = true;
            Pool.Release(this.gameObject);
             player.PlayerScaleControll();
        }
    }

    public void Init(PlayerMove owner)
    {
        player = owner;
    }


}
