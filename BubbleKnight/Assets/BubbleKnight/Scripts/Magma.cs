using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Magma : MonsterController
{
    public float magmaSpeed = 0;
    const float MIN_HEIGHT = -7;
    const float MAX_HEIGHT = -1;
    const float DEATH_HEIGHT = -4;

    private void Update()
    {
        float deltaTime = Time.deltaTime;
        if (GameManager._instance.GetDangerous())
        {
            magmaSpeed = 0.2f;
        }
        else
        {
            magmaSpeed = -0.5f;
        }
        if (gameObject.transform.position.y > DEATH_HEIGHT)
        {
            magmaSpeed = 1;
        }
        

        float y = Mathf.Min(MAX_HEIGHT, gameObject.transform.position.y + magmaSpeed * deltaTime);
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, y, gameObject.transform.position.z);
        if (gameObject.transform.position.y < MIN_HEIGHT - 0.5f)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, MIN_HEIGHT, gameObject.transform.position.z); ;
            gameObject.SetActive(false);
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Body>())
            GameManager._instance.killRole();
    }
}
