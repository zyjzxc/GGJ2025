using UnityEngine;
using System.Collections.Generic;


public class MonsterSpawner : MonoBehaviour
{
    public GameObject[] monsterPrefabs;
    public float spawnInterval = 0.5f;
    public List<Transform> spawnPoints = new List<Transform>();
    public int maxMonsters = 10;


    private float timer = 0f;
    private List<GameObject> activeMonsters = new List<GameObject>();


    void Update()
    {
        timer += Time.deltaTime;


        // 当计时器超过生成间隔且同时存在的怪物数未达到上限时
        if (timer >= spawnInterval && activeMonsters.Count < maxMonsters)
        {
            SpawnMonster();
            timer = 0f;
        }


        // 检查并移除已销毁的怪物
        CheckActiveMonsters();
    }


    void SpawnMonster()
    {
        if (monsterPrefabs.Length > 0 && spawnPoints.Count > 0)
        {
            int randomMonsterIndex = Random.Range(0, monsterPrefabs.Length);
            GameObject selectedMonsterPrefab = monsterPrefabs[randomMonsterIndex];


            int randomSpawnPointIndex = Random.Range(0, spawnPoints.Count);
            Transform selectedSpawnPoint = spawnPoints[randomSpawnPointIndex];


            // 实例化选中的怪物预制件
            GameObject spawnedMonster = Instantiate(selectedMonsterPrefab, selectedSpawnPoint.position, Quaternion.identity);


            // 将新生成的怪物添加到 activeMonsters 列表中
            activeMonsters.Add(spawnedMonster);


        }
        else
        {
            Debug.LogWarning("MonsterPrefabs 数组为空或 SpawnPoints 列表为空，请检查！");
        }
    }


    void CheckActiveMonsters()
    {
        // 检查 activeMonsters 列表，移除已销毁的怪物
        for (int i = activeMonsters.Count - 1; i >= 0; i--)
        {
            if (activeMonsters[i] == null)
            {
                activeMonsters.RemoveAt(i);
            }
        }
    }
}