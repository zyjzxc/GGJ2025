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


        // ����ʱ���������ɼ����ͬʱ���ڵĹ�����δ�ﵽ����ʱ
        if (timer >= spawnInterval && activeMonsters.Count < maxMonsters)
        {
            SpawnMonster();
            timer = 0f;
        }


        // ��鲢�Ƴ������ٵĹ���
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


            // ʵ����ѡ�еĹ���Ԥ�Ƽ�
            GameObject spawnedMonster = Instantiate(selectedMonsterPrefab, selectedSpawnPoint.position, Quaternion.identity);


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
        // ��� activeMonsters �б��Ƴ������ٵĹ���
        for (int i = activeMonsters.Count - 1; i >= 0; i--)
        {
            if (activeMonsters[i] == null)
            {
                activeMonsters.RemoveAt(i);
            }
        }
    }
}