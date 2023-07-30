using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float moveSpeed = 10.0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BORDER"))
        {
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
