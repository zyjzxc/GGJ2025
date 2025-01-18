using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalMonster : MonsterController
{
    public float moveSpeed = 2.0f; // �����ƶ��ٶ�
    public float floatSpeed = 1.0f; // ���¸����ٶ�
    public float floatAmplitude = 1.0f; // ���¸�������
    protected float initialY;
    protected float floatTimer = 0.0f;
    protected bool movingRight = true;


    void Start()
    {
        initialY = transform.position.y;
    }


    protected virtual void Update()
    {
        // �����ƶ�
        float horizontalMovement = moveSpeed * Time.deltaTime * (movingRight ? 1 : -1);
        transform.Translate(Vector3.right * horizontalMovement);


        // ����Ƿ񵽴���Ļ��Ե
        if (IsAtScreenEdge())
        {
            movingRight = !movingRight;
        }


        // ���¸���
        floatTimer += Time.deltaTime * floatSpeed;
        float verticalMovement = Mathf.Sin(floatTimer) * floatAmplitude;
        transform.position = new Vector3(transform.position.x, initialY + verticalMovement, transform.position.z);
    }


    bool IsAtScreenEdge()
    {
        // ����Ƿ񳬳���Ļ���ұ�Ե
        if ((transform.position.x > 4f && movingRight) || (transform.position.x < -4f && !movingRight))
        {
            return true;
        }


        return false;
    }
}
