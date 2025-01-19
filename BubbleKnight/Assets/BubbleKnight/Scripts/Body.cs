using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.layer == 7 || collision.gameObject.layer == 10)
        {
            Vector3 pos = collision.transform.position;

            Vector3 vec = pos - this.transform.position;

            float dotProduct = Vector2.Dot(new Vector2(vec.x, vec.y).normalized, new Vector2(0, -1));
            // 计算夹角（弧度）
            float angleInRadians = Mathf.Acos(dotProduct);
            // 转换为角度
            float angleInDegrees = Mathf.Rad2Deg * angleInRadians;
            // 判断夹角是否在 90 度以内
            if (angleInDegrees < 90 && collision.gameObject.layer == 7)
            {
                // 不掉血
            } else
            {
                GameManager._instance.TakeDamage();
            }
            
        }
    }
}
