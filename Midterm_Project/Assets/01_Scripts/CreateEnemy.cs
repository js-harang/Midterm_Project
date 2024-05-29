using System.Collections.Generic;
using UnityEngine;

public class CreateEnemy : MonoBehaviour
{
    GameManager gameManager;

    [SerializeField]
    GameObject enemyPrefap;
    List<GameObject> enemyObjectPool;

    [SerializeField, Space(10)]
    GameObject[] createZone;

    private void Start()
    {
        gameManager = GameManager.gameManager;

        enemyObjectPool = new List<GameObject>();
        for (int i = 0; i < gameManager.enemyMaxCount; i++)
        {
            GameObject enemy = Instantiate(enemyPrefap);
            enemyObjectPool.Add(enemy);
            enemy.SetActive(false);
        }
    }

    private void Update()
    {
        if (enemyObjectPool.Count == 0)
            return;

        EnemyCreate();
    }

    private void EnemyCreate()
    {
        int createZoneNum = Random.Range(0, createZone.Length - 1);

        // 생성될 랜덤 위치 지정
        GameObject createObject = createZone[createZoneNum];
        float range_X = createObject.transform.localScale.x;
        float range_Y = createObject.transform.localScale.y;
        range_X = Random.Range((range_X / 2) * -1, range_X / 2);
        range_Y = Random.Range((range_Y / 2) * -1, range_Y / 2);
        Vector3 RandomPostion = new Vector3(range_X, range_Y, 0);

        GameObject enemy = enemyObjectPool[0];
        enemy.transform.position = createObject.transform.position + RandomPostion;
        enemy.SetActive(true);
        enemyObjectPool.Remove(enemy);
    }
}
