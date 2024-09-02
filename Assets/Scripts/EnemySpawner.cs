using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] spawnpoints;
    public List<GameObject> enemyPrefabs;

    private void Start()
    {
        InvokeRepeating("SpawnEnemy", 2f, 3f);
    }

    void SpawnEnemy()
    {
        int randomSpawnIndex = Random.Range(0, spawnpoints.Length);
        Transform spawnPoint = spawnpoints[randomSpawnIndex];

        int randomEnemyIndex = Random.Range(0, enemyPrefabs.Count);
        GameObject selectedEnemy = enemyPrefabs[randomEnemyIndex];

        Vector3 spawnPosition = new Vector3(spawnPoint.position.x, spawnPoint.position.y, 0);
        GameObject myEnemy = Instantiate(selectedEnemy, spawnPosition, Quaternion.identity);
    }
}
