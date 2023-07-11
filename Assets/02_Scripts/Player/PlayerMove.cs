using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 8.0f;
    private Rigidbody2D playerRigidbody;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
       
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(0.05f,0f,0f);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-0.05f, 0f, 0f);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(0f, 0.05f, 0f);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(0f, -0.05f, 0f);
        }

        //RigidBody를 이용한 이동

        //transform.Translate(Vector3.right * Time.deltaTime);
        //transform.Translate(Vector3.right * Time.deltaTime);

        //float xInput = Input.GetAxis("Horizontal");
        //float yInput = Input.GetAxis("Vertical");
        //Vector3 newVelocity = new Vector2(xInput * moveSpeed, yInput * moveSpeed);
        //playerRigidbody.velocity = newVelocity;
    }
}