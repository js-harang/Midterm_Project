using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemyPrepab;
    private GameObject[] enemyObjectPool;

    public Transform[] spawnPoint = new Transform[3];

    public void Start()
    {
        //for (int i = 0; i < enemyObjectPool.Length; i++)
        //{
        //    GameObject enemy = enemyPrepab;
        //    enemyObjectPool[i] = Instantiate(enemy);
        //    enemy.SetActive(false);
        //}
    }

    public void Respawn()
    {
        for (int i = 0; i < enemyObjectPool.Length; i++)
        {
            enemyObjectPool[i].transform.position = spawnPoint[i].position;
            enemyObjectPool[i].SetActive(true);
        }


    }
}
