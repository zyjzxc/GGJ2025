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
        // ×óÓÒÒÆ¶¯
        float horizontalMovement = moveSpeed * Time.deltaTime * (movingRight ? 1 : -1);
        transform.Translate(Vector3.right * horizontalMovement);


        // ¼ì²éÊÇ·ñµ½´ïÆÁÄ»±ßÔµ
        if (IsAtScreenEdge())
        {
            movingRight = !movingRight;
        }


        // ÉÏÏÂ¸¡¶¯
        floatTimer += Time.deltaTime * floatSpeed;
        float verticalMovement = Mathf.Sin(floatTimer) * floatAmplitude;
        transform.position = new Vector3(transform.position.x, initialY + verticalMovement, transform.position.z);
    }


    bool IsAtScreenEdge()
    {
        // ¼ì²éÊÇ·ñ³¬³öÆÁÄ»×óÓÒ±ßÔµ
        if ((transform.position.x > 4f && movingRight) || (transform.position.x < -4f && !movingRight))
        {
            return true;
        }
        return false;
    }
}
