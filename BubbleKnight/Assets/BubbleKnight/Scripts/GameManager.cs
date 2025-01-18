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


    public RoleControl roleControl;
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
        nowHeight = 0;
        state = GameState.Prepare;
        health = 3;
        GameStart();
    }

    void GameStart()
    {
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

        //int dangrousSpeed = nowHeight / 10;
        nowHeight += (deltaTime * upSpeed);
        //if (state == GameState.Running)
        {
            if (nowHeight >= MAX_HEIGHT)
            {
                state = GameState.Win;
                GameEnd();
            }
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
        upSpeed = (int)((float)upSpeed * 0.9f);
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
            roleControl.Dead();
            state = GameState.Lose;
            GameEnd();
        } else
        {
            roleControl.AddWuDiTime(wuDiTime);
        }
    }
}
