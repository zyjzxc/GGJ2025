using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : Prop
{
    override protected void Die()
    {
        base.Die();
        GameManager._instance.AddHearth(1);
        Destroy(gameObject, 0.1f);
    }
}
