using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonsterController
{
    public Vector3 moveVector = Vector3.zero;
    public float speed = 1;

    // Update is called once per frame
    protected override void  Update()
    {
        if (IsAtScreenEdge())
        {
            Destroy(gameObject, 0.1f);
        } else
        {
            transform.position += moveVector * Time.deltaTime * speed;
        }
    }

    bool IsAtScreenEdge()
    {
        // ¼ì²éÊÇ·ñ³¬³öÆÁÄ»×óÓÒ±ßÔµ
        if ((transform.position.x > 5.6f) || (transform.position.x < -5.6f) ||
            (transform.position.y > 5.6f) || (transform.position.y < -5.6f))
        {
            return true;
        }

        return false;
    }
}
