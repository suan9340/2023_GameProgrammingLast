using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    public bool isEnemy = false;

    private PlayerMove player;



private void OnCollisionEnter2D(Collision2D collision)
{
        if (collision.gameObject. CompareTag("BORDER"))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("ENEMY"))
        {
            isEnemy = true;
            Destroy(gameObject);

            player.PlayerScaleControll();
        }
    }

    public void Init(PlayerMove owner)
    {
        player = owner;
    }
}
