using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{

    public Vector2 size;
    public LayerMask whatIsLayer;


    void Start()
    {
      
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, size);
    }
    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            
            Collider2D hit = Physics2D.OverlapBox(transform.position, size, 0, whatIsLayer);
            Debug.Log(hit.name);
        }
      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }


}
