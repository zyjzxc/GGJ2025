using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

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

    public Sprite spriteIdle;
    public Sprite spriteAttack;
    public Sprite spriteAttack2;
    public Sprite spriteAttacking;
    public Sprite spriteDeath;

    private float attackTime = 4f;
    private float attackTimeAdd = 0;
    private bool attackEffect = false;

    public bool isWuDi = false;
    private float wuDiTime = 0;

    public GameObject weapon;

    // Start is called before the first frame update
    void Start()
    {
        weapon.SetActive(false);
        GameManager._instance.roleControl = this;
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Debug.Log("碰怪" + collision.name);
    //    StopAttack();
    //}

    // Update is called once per frame
    void Update()
    {
        if (roleState == RoleState.Dead)
        {
            spriteRenderer.sprite = spriteDeath;
            spriteRenderer.flipX = false;
            move = new Vector2(0, 0);
            body.velocity = move;
            return;
        }
        WuDi();
        Move();
        Attack();
        Attacking();
        OutScreenY();
        PostAttacking();

        SetAnimation();

        targetVelocity = move * maxSpeed;
        body.velocity = targetVelocity;
        body.velocity += Physics2D.gravity * 0.2f;


        Bound();
    }

    private void WuDi()
    {
        if(wuDiTime > 0)
        {
            wuDiTime -= Time.deltaTime;
            spriteRenderer.color = Color.yellow;
            isWuDi = true;
        } else
        {
            isWuDi = false;
            wuDiTime = 0;
            spriteRenderer.color = Color.white;
        }
    }

    public void AddWuDiTime(float time)
    {
        wuDiTime += time;
    }

    private void SetAnimation()
    {
        if (roleState == RoleState.Idle)
        {
            spriteRenderer.sprite = spriteIdle;
        } 
        else if (roleState == RoleState.Attack)
        {

            spriteRenderer.sprite = spriteAttack;
        }
        else if (roleState == RoleState.PostAttack)
        {
            if (attackEffect) // 需要播放攻击动画
            {
                spriteRenderer.sprite = spriteAttacking;
                attackEffect = false;
            } else
            {
                if(attackTimeAdd > attackTime)
                {
                    attackTimeAdd = 0;
                    spriteRenderer.sprite = spriteIdle;
                } else if (attackTimeAdd > attackTime/1.5)
                {
                    spriteRenderer.sprite = spriteAttack2;
                } else
                {
                    attackTimeAdd += Time.deltaTime;
                }
            }
        }
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            move.y = -attackSpeed;
            roleState = RoleState.Attack;
            Debug.Log("攻击");
            weapon.SetActive(true);
        }
    }

    private void Attacking()
    {
        if(roleState != RoleState.Attack)
        {
            return;
        }

        //if (transform.position.y > minY) // 下落
        //{
        //    // TODO检测怪物
        //}
        //else // 完成下落
        //{
        //    GameManager._instance.SlowDown();
        //    StopAttack(true);
        //}
    }

    private void OutScreenY()
    {
        if(transform.position.y < minY)
        {
            UIManager.instance.TipsText("Miss", transform.position + new Vector3(0,1.5f,0), 1.5f, Color.cyan);
            transform.position = new Vector3(transform.position.x, minY + 0.01f, transform.position.z);
            GameManager._instance.SlowDown();
            StopAttack(true);
        }

    }

    public void StopAttack(bool isMiss)
    {
        if (roleState == RoleState.Dead || GameManager._instance.IsGameEnd())
            return;
        AudioManager.Instance.PlaySound(0);
        GameManager._instance.ConitnueHit(isMiss);
        weapon.SetActive(false);
        roleState = RoleState.PostAttack;
        move.y = isMiss ? attackSpeed*0.5f : attackSpeed;
        attackEffect = true;
    }

    public void Dead()
    {
        roleState = RoleState.Dead;
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
        if(roleState == RoleState.Idle || roleState == RoleState.Attack || roleState == RoleState.PostAttack)
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

    public bool IsCauseDamage()
    {
        if (GameManager._instance.IsGameEnd())
            return false;
        if(roleState == RoleState.Attack)
        {
            return true ;
        }
        return false;
    }
}
