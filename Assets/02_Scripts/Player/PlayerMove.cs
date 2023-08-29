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

    [SerializeField] private Transform directionObject;

    [Header("카메라 관련")]
    public CinemachineVirtualCamera playerVcam;
    public Camera cam;

    [Header("크기 관련")]
    float radius = 1f;
    float sizeSpeed = 1f;
    bool isScaleChange = false;

    [Header("매니저 관련")]
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
            isScaleChange = true;
            radius = value;
            transform.DOScale(Vector2.one * radius, 0.3f).SetEase(Ease.OutElastic).OnComplete(() => isScaleChange = false);

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

    void Update()
    {
        if (uiManager.GetComponent<UIManager>().isGameStart == false)
        {
            return;
        }

        if(!isScaleChange)
        {

            // 서서히 줄어드는 것.
            radius -= Time.deltaTime * 0.02f;
            transform.localScale = Vector2.one * radius;
        }

        // 크기에 따라 속도가 달라지는 것.
        float myLerp = 1 - (Mathf.Clamp(transform.localScale.x, 0, 5) / 5);
        sizeSpeed = Mathf.Lerp(1, 5, myLerp);

        

        if (Input.GetMouseButtonDown(0))
        {
            Bullet bulletGo = BulletPoolManager.instance.BulletShoot();
            Shoot(bulletGo);
        }

        transform.rotation = Quaternion.Euler(0, 0, 0);
        RotateDirectionObject();
    }

    private void FixedUpdate()
    {
        Move();
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
            isScaleChange = true;
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

    void Shoot(Bullet _obj)
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
      //  transform.localEulerAngles = new Vector3(0, 0, angle);
    }

    public void PlayerScaleControll()
    {
        // 적 맞혔을때.
        Radius += 0.5f;
    }

    void PlayerScale()
    {
        // 총알 발사했을때.
        Radius -= 0.1f;
    }

    public void PlayerDie()
    {
        uiManager.GetComponent<UIManager>().GameOverPanel();
        gameObject.SetActive(false);
        Time.timeScale = 0;
    }
}