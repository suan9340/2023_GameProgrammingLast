using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFSM : MonoBehaviour
{
    State curState;
    float stateTimer = 0f;
    Transform player;
    bool isCanAttack = true;

    void ChangeState(State state)
    {
        stateTimer = 0;
        curState = state;
    }

    private void Update()
    {
        stateTimer += Time.deltaTime;
        switch (curState)
        {
            case State.Idle:
                IdleState();
                break;
            case State.Move:
                MoveState();
                break;
            case State.Attack:
                AttackState();
                break;
        }
    }

    void IdleState()
    {
        float dist = Vector3.Distance(transform.position, player.position);
        if (dist < 10)
        {
            ChangeState(State.Move);
        }
    }

    void MoveState()
    {
        float dist = Vector3.Distance(transform.position, player.position);
        Vector3 dir = (player.position - transform.position).normalized;
        transform.position += dir * 10 * Time.deltaTime;

        if (dist > 12.5f)
        {
            ChangeState(State.Idle);
        }
        else if (dist < 2.5f)
        {
            ChangeState(State.Attack);
        }
    }

    void AttackState()
    {
        if (isCanAttack)
        {
            // player.GetComponent<> 아무튼 공격
        }
        isCanAttack = false;

        if (stateTimer > 2f)
        {
            ChangeState(State.Idle);
            isCanAttack = true;
        }
    }

    enum State
    {
        Idle,
        Move,
        Attack,
    }
}