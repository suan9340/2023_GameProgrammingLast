using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM : MonoBehaviour
{
    [Header("적 스테이트 관련")]
    State curState;
    float stateTimer;

    [Header("적 이동 관련")]
    float enemySpeed = 3f;
    Vector2 idleMoveDir;
    PlayerMove player; // Target 플레이어

    [Header("적 총알 관련")]
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 2f;

    [Header("EnemyInfo")]
    public float hp = 20;
    enum State
    {
        Idle,
        Chase,
        Die,
    }

    void ChangeState(State state)
    {
        stateTimer = 0;
        curState = state;
    }

    private void Start()
    {
        InvokeRepeating("Shoot", 0, 2);
    }

    private void Update()
    {
        stateTimer += Time.deltaTime;
        switch (curState)
        {
            case State.Idle:
                IdleState();
                break;
            case State.Chase:
                ChaseState();
                break;
            case State.Die:
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(ConstantManager.TAG_BULLET))
        {
            CollisionWithBullet(5f);
        }
        if (collision.gameObject.CompareTag(ConstantManager.TAG_BORDER))
        {
            Vector3 reflectVector = Vector3.Reflect(idleMoveDir, collision.contacts[0].normal);
            idleMoveDir = reflectVector;
        }
    }

    private void CollisionWithBullet(float _damage)
    {
        hp -= _damage;

        if (hp <= 0)
        {
            Destroy(gameObject);
            ParticleManager.Instance.AddParticle(ParticleManager.ParticleType.enemyDie, gameObject.transform.position);
            EventManager.TriggerEvent(ConstantManager.PLAYER_BIG);
        }
    }

    void IdleState()
    {

        float dist = Vector3.Distance(transform.position, player.gameObject.transform.position);

        if (stateTimer > 0)
        {
            stateTimer = -Random.Range(2.5f, 5f);
            idleMoveDir = Random.insideUnitCircle.normalized;
        }

        transform.position += (Vector3)idleMoveDir * Time.deltaTime * enemySpeed;

        if (dist < 10)
        {
            ChangeState(State.Chase);
        }
    }

    void ChaseState()
    {

        float dist = Vector3.Distance(transform.position, player.gameObject.transform.position);
        Vector3 dir = (player.gameObject.transform.position - transform.position).normalized;
        transform.position += dir * enemySpeed * Time.deltaTime;

        //만약 거리가 10보다 멀면 idle으로!
        if (dist > 12)
        {
            ChangeState(State.Idle);
        }
    }

    void Shoot()
    {
        GameObject enemyBullet = Instantiate(bulletPrefab);
        enemyBullet.transform.position = transform.position;

        Vector2 bulletDir = player.gameObject.transform.position - transform.position;
        bulletDir.Normalize();
        enemyBullet.GetComponent<Rigidbody2D>().AddForce(bulletDir * bulletSpeed, ForceMode2D.Impulse);
    }

    public void EnemyInit(PlayerMove owner)
    {
        player = owner;
    }
}