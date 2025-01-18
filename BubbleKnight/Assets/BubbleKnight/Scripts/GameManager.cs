using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

enum GameState
{
    Prepare,
    Running,
    Lose,
    Win
}

public struct GameData
{
    float m0;

    int dangrousSpeed;
}

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;
    const int MAX_HEART = 3;
    public const float MAX_HEIGHT = 10000;
    public float nowHeight = 0;
    public float upSpeed = 0;
    public int health = MAX_HEART;

    const float slowSpeedGapTime = 0.5f;
    float slowTimer = 0;
    public int nowLvl = 0;

    // 500, 1000, 1500, 2500, 4000, 6000, 1000
    const int MAX_LVL = 6;
    public float[] levelHeight = { 500, 1000, 1500, 2500, 4000, 6000, 10000 };
    public float [] dangerousSpeeds = { 20, 20, 30, 60, 80, 120, 150 };

    public bool GetDangerous()
    {
        return upSpeed < dangerousSpeeds[nowLvl];
    }

    public float dangerousTimer = 1f;

    public GameObject magma;


    public RoleControl roleControl;
    public MonsterSpawner monsterSpawner;
    public float wuDiTime = 0.5f;

    GameState state = GameState.Prepare;
    // Start is called before the first frame update

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    void Start()
    {
        
        GameReset();
    }

    void GameReset()
    {
        state = GameState.Prepare;
        
        GameStart();
    }

    void GameStart()
    {
        nowHeight = 0;
        health = 3;
        nowLvl = 0;
        state = GameState.Running;
        upSpeed = 10;
    }

    void GameEnd()
    {
        Debug.Log("ÓÎÏ·½áÊø");
        upSpeed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (state != GameState.Running) return;

        UIManager.instance.SetSpeed(upSpeed);
        UIManager.instance.SetTarget(nowHeight, MAX_HEIGHT);

        float deltaTime = Time.deltaTime;

        // slow speed
        slowTimer += deltaTime;
        if (slowTimer > slowSpeedGapTime)
        {
            upSpeed = Mathf.Max(upSpeed - 1, 0);
            slowTimer = 0;
        }

        nowHeight += (deltaTime * upSpeed);
        if (nowHeight > levelHeight[nowLvl]) {
            nowLvl = Mathf.Min(MAX_LVL, nowLvl + 1);
            dangerousTimer = 1f;
        }


        if (!magma.gameObject.activeSelf && GetDangerous())
        {
            dangerousTimer -= deltaTime;
        }
        else
        {
            dangerousTimer = 1;
        }
        if (dangerousTimer < 0)
        {
            magma.SetActive(true);
        }

        if (nowHeight >= MAX_HEIGHT)
        {
            state = GameState.Win;
            GameEnd();
        }
    }

    public void AddSpeed(int sp)
    {
        upSpeed += sp;
    }

    public void AddHearth(int sp)
    {
        health += sp;
        health = Mathf.Min(health, MAX_HEART);
        UIManager.instance.SetHealth(health);
    }

    public void SlowDown()
    {
        upSpeed = upSpeed * 0.9f;
    }

    public void killRole()
    {
        health = 0;
        UIManager.instance.SetHealth(health);
        roleControl.Dead();
        state = GameState.Lose;
        GameEnd();
    }

    public void TakeDamage()
    {
        if (roleControl.isWuDi == true)
            return;
        health--;
        UIManager.instance.SetHealth(health);
        Debug.Log("¿ÛÑª" + health);
        if (health == 0)
        {
            killRole();
        } else
        {
            roleControl.AddWuDiTime(wuDiTime);
        }
    }

    public void Boom(Vector3 p, float boomRange)
    {
        monsterSpawner.Boom(p , boomRange);
    }
}
