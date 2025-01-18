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
    public const int MAX_HEIGHT = 10000;
    public int nowHeight = 0;
    public int upSpeed = 0;
    public int health = 3;

    const float slowSpeedGapTime = 0.5f;
    float slowTimer = 0;


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
        upSpeed = 5;
        state = GameState.Prepare;
        health = 3;
    }

    void GameStart()
    {
        state = GameState.Running;
    }

    void GameEnd()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (state != GameState.Running) return;

        float deltaTime = Time.deltaTime;

        // slow speed
        slowTimer += deltaTime;
        if (slowTimer > slowSpeedGapTime)
        {
            upSpeed--;
            slowTimer = 0;
        }

        //int dangrousSpeed = nowHeight / 10;
        nowHeight += (int)(deltaTime * upSpeed);
        //if (state == GameState.Running)
        {
            if (nowHeight >= MAX_HEIGHT)
            {
                state = GameState.Win;
                GameEnd();
            }
        }
    }

    void AddSpeed(int sp)
    {
        upSpeed += sp;
    }

    void TakeDamage()
    {
        health--;
        if (health < 0)
        {
            state = GameState.Lose;
            GameEnd();
        }
    }
}
