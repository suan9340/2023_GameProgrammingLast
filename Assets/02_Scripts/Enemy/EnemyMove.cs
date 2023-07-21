using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{

    private Rigidbody2D enemyRigidbody2D;
    private Transform target;

    [Header("�߰� �ӵ�")]
    [SerializeField] [Range(1f,4f)] float moveSpeed = 3f;

    [Header("���� �Ÿ�")]
    [SerializeField][Range(1f, 4f)] float contact = 3f;




    void Update()
    {
      
      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BULLET"))
        {
            Destroy(gameObject);
        }
    }


}
