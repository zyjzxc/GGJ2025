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
    public float maxY = 2.12f; // 最高高度
    public float attackSpeed = 4;

    public Vector2 boundMin;
    public Vector2 boundMax;

    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("碰怪" + collision.name);
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

        Bound();
    }

    private void Bound()
    {
        float y = Mathf.Min(transform.position.y, boundMax.y);
        float x = Mathf.Max(transform.position.x, boundMin.x);
        x = Mathf.Min(x, boundMax.x);
        transform.position = new Vector3(x, y, 0);
    }

    private void Move()
    {
        if (!CanMove())
            return;

        if (Input.GetKey(KeyCode.D)) // 左移
        {
            move.x = 1;
        }
        else if (Input.GetKey(KeyCode.A)) // 右移
        {
            move.x = -1;
        }
        else
        {
            move.x = 0;
        }

        if (move.x > 0.01f)
            spriteRenderer.flipX = false;
        else if (move.x < -0.01f)
            spriteRenderer.flipX = true;
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

        if (transform.position.y > minY) // 下落
        {
            // TODO检测怪物

        }
        else // 完成下落
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

        if (transform.position.y < maxY) // 上升
        {
            // TODO检测怪物
        }
        else // 完成下落
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
