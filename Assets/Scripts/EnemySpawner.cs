using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefabEnemy;
    [SerializeField] private Transform[] spawnPoints;
    private int randomSpawnPosition;
    private bool readyToSpawn;
    private float spawnDelay;
    public static int numOfEnemies = 30;
    private int enemyCounter = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spawnDelay += Time.deltaTime;
        readyToSpawn = true;
        StartCoroutine(SpawnEnemy());
        if (spawnDelay >= 1f && readyToSpawn && enemyCounter < numOfEnemies)
        {
            randomSpawnPosition = Random.Range(0, transform.childCount);
            Instantiate(prefabEnemy, spawnPoints[randomSpawnPosition].position, Quaternion.identity);
            spawnDelay = 0;
            enemyCounter++;
            readyToSpawn = false;
        }
    }
    IEnumerator SpawnEnemy()
    {
        yield return new WaitUntil(() => readyToSpawn == true);
    }
}
