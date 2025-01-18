using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : Prop
{
    public float boomRange = 3.0f;
    override protected void Die()
    {
        base.Die();
        GameManager._instance.Boom(transform.position, boomRange);
        Destroy(gameObject, 0.1f);
    }
}
