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

    public void InitMonster(Vector3 pos)
    {
        this.transform.position = pos;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void MoveAbility(Vector3 start, Vector3 end)
    {
        // Æô¶¯Ð­³ÌA->B

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(GameManager._instance.roleControl.IsCauseDamage())
            Die();
    }

    virtual protected void Die()
    {
        GameManager._instance.AddSpeed(point);
        Destroy(gameObject, 0.1f);
    }
}
