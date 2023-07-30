using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private float enemyMoveSpeed =2f;

    private PlayerMove target;

    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 2f;

    private void Start()
    {
        InvokeRepeating("Shoot", 1, 2);
    }

    void Update()
    {
        float dis = Vector2.Distance(transform.position, target.gameObject.transform.position);

        if (dis <= 10)
        {
            Move();
        }
        else
        {
            RandomMove();
        };

      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BULLET"))
        {
            Destroy(gameObject);
        }
    }

    void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, 
            target.transform.position, enemyMoveSpeed * Time.deltaTime);
    }

    void Shoot()
    {
       GameObject enemyBullet = Instantiate(bulletPrefab);
       enemyBullet.transform.position = transform.position;

        Vector2 dir = target.gameObject.transform.position - transform.position;
        dir.Normalize();
        enemyBullet.GetComponent<Rigidbody2D>().AddForce(dir * bulletSpeed, ForceMode2D.Impulse);
    }

    void RandomMove()
    {
        float randomX = Random.Range(-25f, 25f);
        float randomY = Random.Range(-17f, 17f);

        transform.position = Vector2.MoveTowards(transform.position, 
            new Vector3(randomX,randomY,0), enemyMoveSpeed * Time.deltaTime);
    }

    public void EnemyInit(PlayerMove owner)
    {
        target = owner;
    }

}
