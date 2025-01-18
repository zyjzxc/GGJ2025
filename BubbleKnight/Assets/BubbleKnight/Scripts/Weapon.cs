using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
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
        if (collision.gameObject.GetComponent<Magma>())
            return;

        if(collision.gameObject.layer == 10) // 如果是子弹就砍不掉
        {
            return;
        }
        GameManager._instance.roleControl.StopAttack(false);
    }
}
