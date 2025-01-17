using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Role : MonoBehaviour
{
    public Vector2 speed = new Vector2(10,10);
    public Vector2 attackVector = new Vector2(0, 50);
    public float attackSpeed = 1;
    private RectTransform rectTransform;
    // Start is called before the first frame update
    void Start()
    {
        // 获取背景图像的 RectTransform 组件
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(Vector2 movement)
    {
        
        this.transform.position += new Vector3(movement.x * speed.x, 0, 0);
    }

    public void Attack()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(111);
    }
}
