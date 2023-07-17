using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("총알 관련")]
    public GameObject bulletPrefab; // 발사할 총알 프리팹
    public Transform bulletSpawnPoint; // 총알이 발사될 위치
    public float bulletSpeed = 10f;

    // 참조 -> 복합형 -> 단순변수
    //[Header("정리")]
    //public GameObject A;
    //public Vector2 B;
    //public int C;

    [Header("이동 관련")]
    private Rigidbody2D playerRigidbody;
    public float moveSpeed;


    [Header("카메라 관련")]
    public CinemachineVirtualCamera playerVcam;
    public Camera cam;

    [Header("크기 괸련")]
    int ScaleCount = 0;
    float radius = 1f;
    public float Radius
    {
        get { return radius; }
        set
        {
            radius = value;
            transform.DOScale(Vector2.one * radius, 0.3f).SetEase(Ease.OutElastic);
            playerVcam.m_Lens.OrthographicSize = radius * 5;
        }
    }


    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Move()
    {
        Vector2 moveInput;
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();

        playerRigidbody.velocity = moveInput * moveSpeed;
        //transform.Translate(moveInput.normalized * Time.deltaTime * moveSpeed);
    }

    void Shoot()
    {
        Vector3 dir = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0)) - cam.transform.position;
        dir.Normalize();
        GameObject bullet = Instantiate(bulletPrefab);

        bullet.transform.position = bulletSpawnPoint.position;
        bullet.gameObject.GetComponent<Rigidbody2D>().AddForce(dir * bulletSpeed, ForceMode2D.Impulse);

        ScaleCount++;
        Radius += 0.2f;
    }
}