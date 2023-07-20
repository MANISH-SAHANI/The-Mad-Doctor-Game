using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public static EnemySpawner instance;

    [SerializeField] private GameObject enemyPrefab;

    private GameObject newEnemy;

    [SerializeField] private Transform[] spawnPositions;

    [SerializeField] private int enemySpawnLimit = 10;

    [SerializeField] private List<GameObject> spawnedEnemies = new List<GameObject>();

    [SerializeField] private float minSpawnTime = 2f, maxSpawnTime = 5f;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    private void Start() {
        Invoke("SpawnEnemy",Random.Range(minSpawnTime, maxSpawnTime));

    }

    void SpawnEnemy() {

        Invoke("SpawnEnemy", Random.Range(minSpawnTime, maxSpawnTime));

        if (spawnedEnemies.Count == enemySpawnLimit) {
            return;
        }

        newEnemy = Instantiate(enemyPrefab, spawnPositions[Random.Range(0,spawnPositions.Length)].position,Quaternion.identity);

        spawnedEnemies.Add(newEnemy);


    }

    public void EnemyDied(GameObject enemy) {
        spawnedEnemies.Remove(enemy);
    }


}
