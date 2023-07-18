using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawManager : MonoBehaviour
{
    
    private float initialSpawnRate = 2f;
    private float spawnRateIncrease = 0.5f;
    public float spawnRateTimer = 0f;
    
    public GameObject playerObj;
    public GameObject coinPrefab;
    public GameObject enemyPrefab;
    public Transform playerTransform;
    public float spawnRate = 10f;
    public float minY = 5.4f;
    public float minX = -9.5f;
    public float maxX = 9.5f;

    [SerializeField] private GameObject[] powerUps;
    private GameManager gameManager;

    private void Start()
    {
        StartCoroutine(spawnEnemy());
        StartCoroutine(SpawnPowerUp());
        StartCoroutine(coinSpawner());
    }
    
    /// <summary>
    /// Spawns enemies with a specified spawn rate, and increases the spawn rate over time.
    /// </summary>
    private IEnumerator spawnEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);
            float randomX = Random.Range(minX, maxX);
            Vector3 randomPosition = new Vector3(randomX, minY, 0);
            GameObject newEnemy = Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
            newEnemy.GetComponent<AIDestinationSetter>().target = playerTransform;

            spawnRateTimer += spawnRate;
            if (spawnRateTimer >= 15f)
            {
                spawnRate -= spawnRateIncrease;
                spawnRate = Mathf.Max(spawnRate, 0.1f);
                spawnRateTimer = 0f;
            }
        }
    }
    
    /// <summary>
    /// Spawns power-ups with a specified spawn rate, and increases the spawn rate over time.
    /// </summary>
    IEnumerator SpawnPowerUp()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            int randomPowerUp = Random.Range(0, 2);
            Instantiate(powerUps[randomPowerUp], new Vector3(Random.Range(-7f, 7f), 4, 0), Quaternion.identity);
            
            spawnRateTimer += spawnRate;
            if (spawnRateTimer >= 7f)
            {
                spawnRate -= spawnRateIncrease;
                spawnRate = Mathf.Max(spawnRate, 0.3f);
                spawnRateTimer = 0f;
            }
        }
    }
    
    /// <summary>
    /// Spawns coins with a specified spawn rate, and increases the spawn rate over time.
    /// </summary>
    IEnumerator coinSpawner()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);
            Instantiate(coinPrefab, new Vector3(Random.Range(-7f, 7f), Random.Range(-4f, 4f), 0), Quaternion.identity);
            
            spawnRateTimer += spawnRate;
            if (spawnRateTimer >= 10f)
            {
                spawnRate -= spawnRateIncrease;
                spawnRate = Mathf.Max(spawnRate, 0.3f);
                spawnRateTimer = 0f;
            }
        }
    }
    
}
