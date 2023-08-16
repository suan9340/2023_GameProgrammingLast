using Cinemachine;
using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // 참조 -> 복합형 -> 단순변수
    //[Header("정리")]
    //public GameObject A;
    //public Vector2 B;
    //public int C;

    [Header("총알 관련")]
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 10f;

    [Header("이동 관련")]
    private Rigidbody2D playerRigidbody;
    public float moveSpeed;

    [Header("카메라 관련")]
    public CinemachineVirtualCamera playerVcam;
    public Camera cam;

    [Header("크기 관련")]
    float radius = 1f;
    float scaleSpeed = 0.1f;

    [Header("방향")]
    [SerializeField] private Transform directionObject;

    public float Radius
    {
        get { return radius; }
        set
        {
            radius = value;
            transform.DOScale(Vector2.one * radius, 0.3f).SetEase(Ease.OutElastic);

            if (2f < radius && radius < 3.5f)
            {
                //시네머신 카메라 
                playerVcam.m_Lens.OrthographicSize = radius * 3;
            }
            
        }
    }

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        StartCoroutine("PlayerScale", 1f);
    }

    void Update()
    {
        Move();

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        transform.rotation = Quaternion.Euler(0, 0, 0);

        RotateDirectionObject();
    }

    private void RotateDirectionObject()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint
            (new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        mousePos.z = 0;
        Vector3 dir = (mousePos - transform.position).normalized;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        directionObject.localEulerAngles = new Vector3(0, 0, angle);
    }

    void Move()
    {
        // 키보드 입력 받기
        Vector2 moveInput;
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();

        // 플레이어 이동
        playerRigidbody.velocity = moveInput * moveSpeed;

        // 마우스 위치 방향을 보기
        Vector2 mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        Vector2 dirVec = mousePos - (Vector2)transform.position;
        transform.up = dirVec.normalized;

        if (transform.localScale.x <= 0 && transform.localScale.y <= 0)
        {
            PlayerDie();     
        }
    }

    void Shoot()
    {
        Vector3 dir = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0)) - gameObject.transform.position;
        dir.z = 0;
        dir.Normalize();

        //총알 발사
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.GetComponent<Bullet>().Init(this);
        bullet.transform.position = bulletSpawnPoint.position;
        bullet.GetComponent<Rigidbody2D>().AddForce(dir * bulletSpeed, ForceMode2D.Impulse);

        PlayerScale();
    }

    public void PlayerScaleControll()
    {
        Radius += 0.5f;
    }

    public void PlayerDie()
    {
        gameObject.SetActive(false);
    }

    void PlayerScale()
    {
        transform.localScale = new Vector3(transform.localScale.x - 100f * scaleSpeed * Time.deltaTime,
               transform.localScale.y - 100f * scaleSpeed * Time.deltaTime, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ENEMY"))
        {
            PlayerDie();
        }
        if (collision.gameObject.CompareTag("ENEMYBULLET"))
        {
            Radius -= 0.3f;
        }
    }
 
}