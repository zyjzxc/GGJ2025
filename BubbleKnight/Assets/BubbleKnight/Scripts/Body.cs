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
        Debug.Log("ล๖นึ" + collision.name);
        if(collision.gameObject.layer == 7 || collision.gameObject.layer == 10)
        {
            GameManager._instance.TakeDamage();
        }
    }
}
