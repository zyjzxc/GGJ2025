using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterState
{
    Idle,
    Move,
    Attack,
    Dead
}

public class MonsterController : MonoBehaviour
{
    public MonsterState monsterState;
    public int point = 3;
    public int health2 = 1;
    protected bool isHurted = false;

    public void InitMonster(Vector3 pos)
    {
        this.transform.position = pos;
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        //isHurted = false ;
    }


    public void MoveAbility(Vector3 start, Vector3 end)
    {
        // Æô¶¯Ð­³ÌA->B

    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager._instance.roleControl.IsCauseDamage())
        {
            Hurt();
        }
    }

    public virtual void Hurt()
    {
        //if (isHurted)
        //    return;
        if (monsterState == MonsterState.Dead) return;

        health2--;

        UIManager.instance.TipsText((GameManager._instance.continueHitTime + 1) + " Hit", transform.position);
        if (health2 <= 0)
        {
            Die();
        } else
        {
            Color c = GetComponent<SpriteRenderer>().color;
            GetComponent<SpriteRenderer>().color = new Color(c.r, c.g, c.b, c.a / 2);
        }
    }


    virtual protected void Die()
    {
        monsterState = MonsterState.Dead;
        GameManager._instance.AddSpeed(point);
        ScreenShake.instance.InduceStress(0.7f);
        Destroy(gameObject, 0.1f);
    }
}
