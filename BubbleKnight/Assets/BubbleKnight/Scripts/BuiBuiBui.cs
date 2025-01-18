using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuiBuiBui : NormalMonster
{
    public float buiTime = 3;
    private float buiTimeAdd = 0;
    public GameObject buiPrefab;

    private Vector3 GetRoleVector()
    {
        Vector3 rolePosition = GameManager._instance.roleControl.transform.position;
        Vector3 roleVector = (rolePosition - transform.position).normalized;
        return roleVector;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        buiTimeAdd += Time.deltaTime;

        if(buiTime < buiTimeAdd)
        {
            Vector3 roleVector = GetRoleVector();
            Vector3 pos = GetRoleVector() * 0.5f + transform.position;
            GameObject bullet = GameManager._instance.monsterSpawner.AddBuiBui(buiPrefab, new Vector3(pos.x, pos.y, transform.position.z));
            bullet.GetComponent<Bullet>().moveVector = roleVector;
            buiTimeAdd = 0;
        }
    }
}
