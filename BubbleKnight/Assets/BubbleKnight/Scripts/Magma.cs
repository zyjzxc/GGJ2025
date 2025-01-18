using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magma : MonsterController
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager._instance.killRole();
    }
}
