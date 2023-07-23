using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private float enemyMoveSpeed =1.5f;

    private PlayerMove target;

    public void EnemyInit(PlayerMove owner)
    {
        target = owner;
    }

    void Update()
    {
        float dis = Vector2.Distance(transform.position, target .gameObject.transform.position);

        if (dis <= 10) 
        {
            Move();
        }
        else return;
    }

    void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, enemyMoveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BULLET"))
        {
            Destroy(gameObject);
        }
    }

    
}
