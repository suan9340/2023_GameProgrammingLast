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

    [SerializeField] private Transform directionObject;

    [Header("ī�޶� ����")]
    public CinemachineVirtualCamera playerVcam;
    public Camera cam;

    [Header("ũ�� ����")]
    float radius = 1f;
    float sizeSpeed = 1f;

    [Header("�Ŵ��� ����")]
    public UIManager uiManager;

    public enum Level
    {
        Level_1,
        Level_2,
        Level_3,
        Level_4,
        Level_5,
    }

    public float Radius
    {
        get { return radius; }
        set
        {
            radius = value;
            transform.DOScale(Vector2.one * radius, 0.3f).SetEase(Ease.OutElastic);

            if (2f < radius && radius < 4f)
            {
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

    public float speed = 1f;
    public Transform bulletSpawnPoint2;

    void Update()
    {

        float myLerp = 1 - (Mathf.Clamp(transform.localScale.x, 0, 5) / 5);
        sizeSpeed = Mathf.Lerp(1, 5, myLerp);

        if (uiManager.GetComponent<UIManager>().isGameStart == false)
        {
            return;
        }

        Move();

        if (Input.GetMouseButtonDown(0))
        {
            var bulletGo = ObjectPoolManager.instance.Pool.Get();
            Shoot(bulletGo);
        }

        transform.rotation = Quaternion.Euler(0, 0, 0);
        RotateDirectionObject();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ENEMY"))
        {
            PlayerDie();
        }
        if (collision.gameObject.CompareTag("ENEMYBULLET") && transform.localScale.x > 0
           && transform.localScale.y > 0)
        {
            Radius -= 0.5f;
        }
    }

    void Move()
    {
        Vector2 moveInput;
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();

        playerRigidbody.velocity = moveInput * sizeSpeed;

        Vector2 mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        Vector2 dirVec = mousePos - (Vector2)transform.position;
        transform.up = dirVec.normalized;

        if (transform.localScale.x <= 0 && transform.localScale.y <= 0)
        {
            PlayerDie();
        }
    }

    void Shoot(GameObject _obj)
    {
        Vector3 dir = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0)) - gameObject.transform.position;
        dir.z = 0;
        dir.Normalize();

        _obj.GetComponent<Bullet>().Init(this);
        _obj.transform.position = bulletSpawnPoint.position;
        _obj.GetComponent<Rigidbody2D>().AddForce(dir * bulletSpeed, ForceMode2D.Impulse);

        PlayerScale();
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

 
    public void PlayerScaleControll()
    {
        // �� ��������.
        Radius += 0.5f;
    }

    void PlayerScale()
    {
        // �Ѿ� �߻�������.
        //transform.localScale = new Vector3(transform.localScale.x - 100f * moveSpeed * Time.deltaTime,
        //    transform.localScale.y - 100f * moveSpeed * Time.deltaTime, 0);
        Radius -= 0.1f;
    }

    public void PlayerDie()
    {
        uiManager.GetComponent<UIManager>().GameOverPanel();
        gameObject.SetActive(false);
        Time.timeScale = 0;
    }
}