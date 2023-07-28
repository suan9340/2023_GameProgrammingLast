using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
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

    public float Radius
    {
        get { return radius; }
        set
        {
            radius = value;
            transform.DOScale(Vector2.one * radius, 0.3f).SetEase(Ease.OutElastic);

            //시네머신 카메라 
            playerVcam.m_Lens.OrthographicSize = radius * 3;
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

        // ScreenToWorldPoint() 함수를 이용해 마우스의 좌표를 게임 좌표로 변환한다.
        // 2D게임이기에 Vector2로 변환
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        // (마우스 위치 - 오브젝트 위치)로 마우스의 방향을 구한다.
        Vector2 dirVec = mousePos - (Vector2)transform.position;

        // 방향벡터를 정규화한 다음 transform.up 벡터에 계속 대입
        transform.up = dirVec.normalized;
    }

    void Shoot()
    {
        Vector3 dir = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0)) - cam.transform.position;
        dir.Normalize();
        GameObject bullet = Instantiate(bulletPrefab);

        bullet.GetComponent<Bullet>().Init(this);


        bullet.transform.position = bulletSpawnPoint.position;
        bullet.GetComponent<Rigidbody2D>().AddForce(dir * bulletSpeed, ForceMode2D.Impulse);

    }

    public void PlayerScaleControll()
    {
        Radius += 0.5f;
    }
}