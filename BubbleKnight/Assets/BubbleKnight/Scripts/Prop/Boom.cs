using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : Prop
{
    public float boomRange = 3.0f;
    override protected void Die()
    {
        GameManager._instance.Boom(transform.position, boomRange);
        base.Die();
        Destroy(gameObject, 0.1f);
    }
}
