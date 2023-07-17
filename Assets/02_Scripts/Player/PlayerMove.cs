using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("�Ѿ� ����")]
    public GameObject bulletPrefab; // �߻��� �Ѿ� ������
    public Transform bulletSpawnPoint; // �Ѿ��� �߻�� ��ġ
    public float bulletSpeed = 10f;

    // ���� -> ������ -> �ܼ�����
    //[Header("����")]
    //public GameObject A;
    //public Vector2 B;
    //public int C;

    [Header("�̵� ����")]
    private Rigidbody2D playerRigidbody;
    public float moveSpeed;


    [Header("ī�޶� ����")]
    public CinemachineVirtualCamera playerVcam;
    public Camera cam;

    [Header("ũ�� ����")]
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