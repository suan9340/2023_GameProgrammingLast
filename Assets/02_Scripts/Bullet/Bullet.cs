using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class Bullet : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    public bool isEnemy = false;

    private PlayerMove player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BORDER"))
        {
            Destroy(gameObject);
        }
        else if (collision.CompareTag("ENEMY"))
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
