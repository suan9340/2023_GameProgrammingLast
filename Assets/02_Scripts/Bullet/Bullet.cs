using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class Bullet : MonoBehaviour
{
    public float moveSpeed = 10.0f;
 
    private void Update()
    {
      //  transform.Translate(Vector3.up * 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BORDER")|| collision.CompareTag("ENEMY"))
        {
            Destroy(gameObject);
        }
    }
}
