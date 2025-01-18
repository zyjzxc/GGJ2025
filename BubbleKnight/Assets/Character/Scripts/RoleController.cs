using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoleState
{
    Idle,
    Attack,
    PostAttack,
    Dead
}

public class RoleControl : MonoBehaviour
{
    private Rigidbody2D body;

    public Vector2 move;
    public Vector2 targetVelocity;
    public float maxSpeed;
    public RoleState roleState = RoleState.Idle;

    public float minY = -4;
    public float maxY = 2.12f; // ��߸߶�
    public float attackSpeed = 4;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Attack();
        Attacking();
        PostAttacking();

        targetVelocity = move * maxSpeed;
        body.velocity = targetVelocity;
    }

    private void Move()
    {
        if (!CanMove())
            return;

        if (Input.GetKey(KeyCode.D)) // ����
        {
            move.x = 1;
        }
        else if (Input.GetKey(KeyCode.A)) // ����
        {
            move.x = -1;
        }
        else
        {
            move.x = 0;
        }
    }

    private void Attack()
    {
        if (!CanAttack())
            return;
        if (Input.GetKey(KeyCode.Space))
        {
            move.y = -attackSpeed;
            roleState = RoleState.Attack;
        }
    }

    private void Attacking()
    {
        if(roleState != RoleState.Attack)
        {
            return;
        }

        if (transform.position.y > minY) // ����
        {
            // TODO������

        }
        else // �������
        {
            roleState = RoleState.PostAttack;
            move.y = attackSpeed;
        }
    }

    private void PostAttacking()
    {
        if(roleState != RoleState.PostAttack)
        {
            return;
        }

        if (transform.position.y < maxY) // ����
        {
            // TODO������
        }
        else // �������
        {
            roleState = RoleState.Idle;
            move.y = 0;
        }
    }

    private bool CanMove()
    {
        if(roleState == RoleState.Idle || roleState == RoleState.Attack)
        {
            return true;
        }

        return false;
    }

    private bool CanAttack()
    {
        if (roleState == RoleState.Idle || roleState == RoleState.PostAttack)
        {
            return true;
        }

        return false;
    }
}
