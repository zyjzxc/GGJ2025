using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalMonster : MonsterController
{
    public float moveSpeed = 2.0f; // 左右移动速度
    public float floatSpeed = 1.0f; // 上下浮动速度
    public float floatAmplitude = 1.0f; // 上下浮动幅度
    protected float initialY;
    protected float floatTimer = 0.0f;
    protected bool movingRight = true;


    void Start()
    {
        initialY = transform.position.y;
    }


    protected virtual void Update()
    {
        // 左右移动
        float horizontalMovement = moveSpeed * Time.deltaTime * (movingRight ? 1 : -1);
        transform.Translate(Vector3.right * horizontalMovement);


        // 检查是否到达屏幕边缘
        if (IsAtScreenEdge())
        {
            movingRight = !movingRight;
        }


        // 上下浮动
        floatTimer += Time.deltaTime * floatSpeed;
        float verticalMovement = Mathf.Sin(floatTimer) * floatAmplitude;
        transform.position = new Vector3(transform.position.x, initialY + verticalMovement, transform.position.z);
    }


    bool IsAtScreenEdge()
    {
        // 检查是否超出屏幕左右边缘
        if ((transform.position.x > 4f && movingRight) || (transform.position.x < -4f && !movingRight))
        {
            return true;
        }


        return false;
    }
}
