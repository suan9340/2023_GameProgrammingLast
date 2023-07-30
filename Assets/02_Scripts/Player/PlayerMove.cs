using Cinemachine;
using DG.Tweening;
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

    private void Start()
    {
        StartCoroutine("PlayerScale", 0.5f);
    }

    void Update()
    {
        Move();

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        transform.rotation = Quaternion.Euler(0, 0, 0);
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
    }

    void Shoot()
    {
        Vector3 dir = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0)) - cam.transform.position;
        dir.Normalize();

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

    IEnumerator PlayerScale(float delayTime)
    {
        if(transform.localScale.x < 0 && transform.localScale.y < 0)
        {
          //  PlayerDie();
            yield return 0;
        }
        else
        {

            PlayerScale();

             yield return new WaitForSeconds(delayTime);

            StartCoroutine("PlayerScale", 0.5f);
        }
    }

    void PlayerDie()
    {
        gameObject.SetActive(false);
        Debug.Log("���� ����");
    }

    void PlayerScale()
    {
        transform.localScale = new Vector3(transform.localScale.x - 100f * scaleSpeed * Time.deltaTime,
               transform.localScale.y - 100f * scaleSpeed * Time.deltaTime, 0);
    }
}