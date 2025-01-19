using UnityEngine;
using System.Collections.Generic;


public class MonsterSpawner : MonoBehaviour
{
    public GameObject[] monsterPrefabs;
    public float spawnInterval = 1f;
    public List<Transform> spawnPoints = new List<Transform>();
    public int maxMonsters = 5;

    private float timer = 0f;
    private List<GameObject> activeMonsters = new List<GameObject>();
    private HashSet<int> boomIndex = new HashSet<int>();
    private void Awake()
    {
        GameManager._instance.monsterSpawner = this;
    }

    private int GetMonsterNum()
    {
        int num = 0;
        foreach (var go in activeMonsters )
        {
            if (go && go.layer == 7)
                num++;
        }
        return num;
    }

    private int GetPropNum()
    {
        int num = 0;
        foreach (var go in activeMonsters)
        {
            if (go && go.layer == 8)
                num++;
        }
        return num;
    }

    void Update()
    {
        timer += Time.deltaTime;
        spawnInterval = GameManager._instance.GetSpawnInterval();
        monsterPrefabs = GameManager._instance.GetMonsterPrefabs();
        maxMonsters = GameManager._instance.GetMaxMonsterNum();
        // ����ʱ���������ɼ����ͬʱ���ڵĹ�����δ�ﵽ����ʱ
        if (timer >= spawnInterval && GetMonsterNum() < maxMonsters)
        {
            SpawnMonster();
            timer = 0f;
        }

        HurtBoom();
        // ��鲢�Ƴ������ٵĹ���
        CheckActiveMonsters();
    }


    void SpawnMonster()
    {
        if (monsterPrefabs.Length > 0 && spawnPoints.Count > 0)
        {
            int randomMonsterIndex = Random.Range(0, monsterPrefabs.Length);
            GameObject selectedMonsterPrefab = monsterPrefabs[randomMonsterIndex];
            if (selectedMonsterPrefab.layer == 8 && GetPropNum() >= 2)
            {
                selectedMonsterPrefab = monsterPrefabs[0];
            }

            int randomSpawnPointIndex = Random.Range(0, spawnPoints.Count);
            Transform selectedSpawnPoint = spawnPoints[randomSpawnPointIndex];


            // ʵ����ѡ�еĹ���Ԥ�Ƽ�
            GameObject spawnedMonster = Instantiate(selectedMonsterPrefab, selectedSpawnPoint.position, Quaternion.identity);

            spawnedMonster.transform.SetParent(GameManager._instance.gameObject.transform);


            // �������ɵĹ�����ӵ� activeMonsters �б���
            activeMonsters.Add(spawnedMonster);


        }
        else
        {
            Debug.LogWarning("MonsterPrefabs ����Ϊ�ջ� SpawnPoints �б�Ϊ�գ����飡");
        }
    }


    void CheckActiveMonsters()
    {
        activeMonsters.RemoveAll(monster => monster == null);
    }

    public void Boom(Vector3 p, float boomRange)
    {
        for (int i = 0; i < activeMonsters.Count; i ++)
        {
            var monster = activeMonsters[i];
            if (monster == null)
            {
                continue;
            }
            float dis = Vector2.Distance(new Vector2(p.x, p.y), new Vector2(monster.transform.position.x, monster.transform.position.y));
            if(dis < boomRange)
            {
                boomIndex.Add(i);
            }
        }
    }

    public void HurtBoom()
    {
        AudioManager.Instance.PlaySound(7);
        foreach (var item in boomIndex)
        {
            if(activeMonsters[item] != null)
                activeMonsters[item].GetComponent<MonsterController>().Hurt();
        }
        boomIndex.Clear();
    }
    
    public GameObject AddBuiBui(GameObject obj, Vector3 point)
    {
        // ʵ����ѡ�еĹ���Ԥ�Ƽ�
        GameObject spawnedMonster = Instantiate(obj, point, Quaternion.identity);
        activeMonsters.Add(spawnedMonster);
        spawnedMonster.transform.SetParent(GameManager._instance.gameObject.transform);
        return spawnedMonster;
    }
}