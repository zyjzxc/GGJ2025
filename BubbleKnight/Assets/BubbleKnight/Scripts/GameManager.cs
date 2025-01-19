using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
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
    public GameObject[] monsterPrefabs;
    public float dangerrousSpeed;
    public float levelHeight;
}

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;

    public GameObject EndUI;

    public GameObject EndText;

    public const int MAX_HEART = 5;
    public const float MAX_HEIGHT = 10000;
    public float nowHeight = 0;
    public float upSpeed = 0;
    public int health = MAX_HEART;

    float slowTimer = 0;
    public int nowLvl = 0;

    // 500, 1000, 1500, 2500, 4000, 6000, 1000
    const int MAX_LVL = 9;
    public GameObject[] monsterPrefabs0;
    public GameObject[] monsterPrefabs1;
    public GameObject[] monsterPrefabs2;
    public GameObject[] monsterPrefabs3;
    public GameObject[] monsterPrefabs4;
    private float[] levelHeight = { 200, 500, 1000, 1500, 2500, 4000, 6000, 10000, 15000, 25000};
    private float [] dangerousSpeeds = {2, 5, 15, 30, 60, 80, 110, 140, 180, 250};
    private int[] maxMonsterNum = { 5, 5, 5, 6, 6, 8, 8, 10, 10, 10};
    private float[] spawnIntervals = { 1f, 0.8f, 0.6f, 0.4f, 0.3f, 0.2f, 0.1f, 0.1f, 0.1f, 0.1f};
    private float[] slowSpeedGapTimes = { 0.3f, 0.3f, 0.25f, 0.25f, 0.2f, 0.15f, 0.1f, 0.05f, 0.04f, 0.02f};

    public float GetSpawnInterval()
    {
        return spawnIntervals[nowLvl];
    }

    public int GetMaxMonsterNum()
    {
        return maxMonsterNum[nowLvl];
    }

    public int continueHitTime = 0;

    public GameObject[] GetMonsterPrefabs()
    {
        switch (nowLvl) {
            case 0: return monsterPrefabs0;
            case 1: return monsterPrefabs1;
            case 2: return monsterPrefabs2;
            case 3: return monsterPrefabs3;
            case 4: return monsterPrefabs4;
            default: return monsterPrefabs4;
        }
    }

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

    public bool IsGameEnd()
    {
        return state != GameState.Running;
    }

    void Start()
    {
        
        GameReset();
    }

    public void GameReset()
    {
        state = GameState.Prepare;
        roleControl.Reset();
        BroadcastMessage("OnResetEvent");
        GameStart();
    }

    void GameStart()
    {
        continueHitTime = 0;
        nowHeight = 0;
        health = MAX_HEART;
        UIManager.instance.SetHealth(health);
        nowLvl = 0;
        state = GameState.Running;
        upSpeed = 10;
        EndUI.SetActive(false);
    }

    void GameEnd()
    {
        if (state == GameState.Win)
        {
            AudioManager.Instance.PlaySound(5);
        }
        else
        {
            AudioManager.Instance.PlaySound(6);
        }
        Debug.Log("ÓÎÏ·½áÊø");
        EndUI.SetActive(true);
        LocalLeaderboard.instance.SaveScore((int)nowHeight);
        LocalLeaderboard.instance.ShowLeaderboard();
        EndText.GetComponent<TextMeshProUGUI>().text = state.ToString(); 
        upSpeed = 0;
    }

    public void ConitnueHit(bool isMiss)
    {
        if (isMiss)
        {
            continueHitTime = 0;
            return;
        }
        continueHitTime ++;
        if (continueHitTime % 10 == 0)
        {
            AudioManager.Instance.PlaySound(4);
            upSpeed += MathF.Min(50, continueHitTime);
        }

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
        if (slowTimer > slowSpeedGapTimes[nowLvl])
        {
            upSpeed = Mathf.Max(upSpeed - 1, 0);
            slowTimer = 0;
        }

        nowHeight += (deltaTime * upSpeed);
        nowLvl = Mathf.Min(MAX_LVL, nowLvl);
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
        AudioManager.Instance.PlaySound(8);
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
        if (IsGameEnd()) return;
        if (roleControl.isWuDi == true)
            return;
        AudioManager.Instance.PlaySound(1);
        health--;
        UIManager.instance.SetHealth(health);
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
