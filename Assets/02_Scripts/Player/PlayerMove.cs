using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // ���� -> ������ -> �ܼ�����
    //[Header("����")]
    //public GameObject A;
    //public Vector2 B;
    //public int C;

    [Header("�Ѿ� ����")]
    public GameObject bulletPrefab; 
    public Transform bulletSpawnPoint; 
    public float bulletSpeed = 10f;

    [Header("�̵� ����")]
    private Rigidbody2D playerRigidbody;
    public float moveSpeed;

    [Header("ī�޶� ����")]
    public CinemachineVirtualCamera playerVcam;
    public Camera cam;

    [Header("ũ�� ����")]
    float radius = 1f;

    public float Radius
    {
        get { return radius; }
        set
        {
            radius = value;
            transform.DOScale(Vector2.one * radius, 0.3f).SetEase(Ease.OutElastic);

            //�ó׸ӽ� ī�޶� 
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

        // ScreenToWorldPoint() �Լ��� �̿��� ���콺�� ��ǥ�� ���� ��ǥ�� ��ȯ�Ѵ�.
        // 2D�����̱⿡ Vector2�� ��ȯ
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        // (���콺 ��ġ - ������Ʈ ��ġ)�� ���콺�� ������ ���Ѵ�.
        Vector2 dirVec = mousePos - (Vector2)transform.position;

        // ���⺤�͸� ����ȭ�� ���� transform.up ���Ϳ� ��� ����
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