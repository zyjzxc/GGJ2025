using System.Collections;
using System.Collections.Generic;
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

    public const int MAX_HEIGHT = 10000;
    public int nowHeight = 0;
    public int upSpeed = 0;
    public int health = 3;


    GameState state = GameState.Prepare;
    // Start is called before the first frame update
    void Start()
    {
        GameReset();
    }

    void GameReset()
    {
        nowHeight = 0;
        upSpeed = 0;
        state = GameState.Prepare;
        health = 3;
    }

    void GameStart()
    {
        state = GameState.Running;
    }

    // Update is called once per frame
    void Update()
    {
        //int dangrousSpeed = nowHeight / 10;
        float deltaTime = Time.deltaTime;
        nowHeight += (int)(deltaTime * upSpeed);
        if (state == GameState.Running)
        {
            if (nowHeight >= MAX_HEIGHT)
            {
                state = GameState.Win;
            }
        }
    }

    void TakeDamage()
    {
        health--;
        if (health < 0)
        {
            state = GameState.Lose;
        }
    }
}
