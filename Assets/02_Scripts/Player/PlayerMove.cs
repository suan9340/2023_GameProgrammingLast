using Cinemachine;
using DG.Tweening;
using System;
using System.Collections;
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
    float scaleSpeed = 0.1f;

    [Header("����")]
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
                //�ó׸ӽ� ī�޶� 
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
        // Ű���� �Է� �ޱ�
        Vector2 moveInput;
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();

        // �÷��̾� �̵�
        playerRigidbody.velocity = moveInput * moveSpeed;

        // ���콺 ��ġ ������ ����
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

        //�Ѿ� �߻�
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