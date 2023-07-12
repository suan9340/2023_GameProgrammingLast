using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 8.0f;
    private Rigidbody2D playerRigidbody;

    // �̻��� ������
    public GameObject missilePrefab;

    // �̻����� �߻�Ǵ� ������ �ӵ�
    public float launchSpeed = 10.0f;
    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
    }

    void Update()
    {
        Move();
    }

       

       void Move()
        {
            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(0.05f, 0f, 0f);
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(-0.05f, 0f, 0f);
            }
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(0f, 0.05f, 0f);
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(0f, -0.05f, 0f);
            }

        }
    
}