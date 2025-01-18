using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop : NormalMonster
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
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
