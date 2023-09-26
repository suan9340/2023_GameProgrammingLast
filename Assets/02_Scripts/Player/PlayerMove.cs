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
    public Transform bulletPos;

    [Header("카메라 관련")]
    public CinemachineVirtualCamera playerVcam;
    public Camera cam;

    [Header("크기 관련")]
    float radius = 1f;
    float sizeSpeed = 1f;
    bool isScaleChange = false;

    [Header("매니저 관련")]
    public UIManager uiManager;


    public float Radius
    {
        get { return radius; }
        set
        {
            isScaleChange = true;
            radius = value;
            transform.DOScale(Vector2.one * radius, 0.7f).SetEase(Ease.OutQuad).OnComplete(() => isScaleChange = false);

            if (2f < radius && radius < 4f)
            {
                playerVcam.m_Lens.OrthographicSize = radius * 3;
            }

        }
    }

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();

        EventManager.StartListening(ConstantManager.PLAYER_BIG, PlayerScaleControll);
    }

    private void OnDisable()
    {
        EventManager.StopListening(ConstantManager.PLAYER_BIG, PlayerScaleControll);
    }

    void Update()
    {
        if (GameManager.Instance.gameState != DefineManager.GameState.Playing)
        {
            return;
        }

        CheckingPlayerState();
        InputKey();
        Move();
        RotateDirectionObject();
        //Debug.Log(Radius);
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.gameState != DefineManager.GameState.Playing)
        {
            return;
        }

        GameOverCheck();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag(ConstantManager.TAG_ENEMYBULLET))
        {
            CollisionWithEnemyBullet();
        }

        if (collision.gameObject.CompareTag(ConstantManager.TAG_ENEMY))
        {
            CollisionWithEnemyBullet();
        }

        if (collision.gameObject.CompareTag(ConstantManager.TAG_BORDER))
        {

        }
    }


    private void CheckingPlayerState()
    {
        if (uiManager.GetComponent<UIManager>().isGameStart == false)
        {
            return;
        }

        if (!isScaleChange)
        {
            // 서서히 줄어드는 것.
            radius -= Time.deltaTime * 0.04f;
            transform.localScale = Vector2.one * radius;
        }

        // 크기에 따라 속도가 달라지는 것.
        float myLerp = 1 - (Mathf.Clamp(transform.localScale.x, 0, 5) / 5);
        sizeSpeed = Mathf.Lerp(1, 5, myLerp);

    }

    private void InputKey()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CameraShake.Instance.ShakeCamera(0.8f, 0.1f);
            Bullet bulletGo = BulletPoolManager.instance.BulletShoot();
            Shoot(bulletGo);
        }
    }

    private void CollisionWithEnemyBullet()
    {
        GameManager.Instance.MinusScore(1f);
        EventManager.TriggerEvent(ConstantManager.PLAYER_DAMAGED_EF);
        CameraShake.Instance.ShakeCamera(1f, 0.5f);

        if (transform.localScale.x > 0 && transform.localScale.y > 0)
        {
            isScaleChange = true;

            var _sa = Radius / 5;
            Radius -= _sa;
        }
    }

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        transform.Translate(new Vector2(x, y) * Time.deltaTime * sizeSpeed);

        Vector2 mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        Vector2 dirVec = mousePos - (Vector2)transform.position;
        transform.up = dirVec.normalized;
    }



    private void GameOverCheck()
    {
        if (transform.localScale.x <= 0 && transform.localScale.y <= 0)
        {
            PlayerDie();
        }
    }

    private void Shoot(Bullet _obj)
    {
        SoundManager.Instance.BulletShoot();
        Vector3 dir = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0)) - gameObject.transform.position;
        dir.z = 0;
        dir.Normalize();

        _obj.GetComponent<Bullet>().Init(this);
        _obj.transform.position = bulletPos.transform.position;
        _obj.GetComponent<Rigidbody2D>().AddForce(dir * bulletSpeed, ForceMode2D.Impulse);

        PlayerScale();
    }

    private void RotateDirectionObject()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);

        Vector3 mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        mousePos.z = 0;

        Vector3 dir = (mousePos - transform.position).normalized;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        directionObject.localEulerAngles = new Vector3(0, 0, angle);
    }

    // 적 맞혔을때.
    public void PlayerScaleControll()
    {
        Radius += 0.4f;
    }

    // 총알 발사했을때.
    void PlayerScale()
    {
        Radius -= 0.04f;
    }

    public void PlayerDie()
    {
        uiManager.GetComponent<UIManager>().GameOverPanel();
        gameObject.SetActive(false);
        Time.timeScale = 0;
    }
}