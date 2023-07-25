using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawManager : MonoBehaviour
{
    
    private float initialSpawnRate = 2f;
    private float spawnRateIncrease = 0.1f;
    public float spawnRateTimer = 0f;
    
    public GameObject playerObj;
    public GameObject coinPrefab;
    public GameObject enemyPrefab;
    public GameObject bossPrefab;
    private bool isBossSpawned = false;
    public Transform playerTransform;
    public float spawnRate = 10f;
    public float minY = -15f;
    public float maxY = 16f;//--------------------------------------------------
    public float minX = -15f;
    public float maxX = 16f;

    [SerializeField] private GameObject[] powerUps;
    private GameManager gameManager;

    private void Start()
    {
        StartCoroutine(spawnEnemy());
        StartCoroutine(SpawnPowerUp());
        StartCoroutine(coinSpawner());
    }

    private void Update()
    {
        if (!isBossSpawned && GameManager.instance.score >= 50)
        {
            StartCoroutine(spawnBoss());
            isBossSpawned = true;
        }
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
            float randomY = Random.Range(minY, maxY);
            Vector3 randomPosition = new Vector3(randomX, randomY, 0);
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
            Instantiate(powerUps[randomPowerUp], new Vector3(Random.Range(-16f, 16f), 16f, 0), Quaternion.identity);
            
            spawnRateTimer += spawnRate;
            if (spawnRateTimer >= 5f)
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
            Instantiate(coinPrefab, new Vector3(Random.Range(-16f, 16f), Random.Range(-16f, 16f), 0), Quaternion.identity);
            
            spawnRateTimer += spawnRate;
            if (spawnRateTimer >= 10f)
            {
                spawnRate -= spawnRateIncrease;
                spawnRate = Mathf.Max(spawnRate, 0.3f);
                spawnRateTimer = 0f;
            }
        }
    }
    
    IEnumerator spawnBoss ()
    {
        yield return new WaitForSeconds(2f);
        StopAllCoroutines();
        GameObject newBoss = Instantiate(bossPrefab, new Vector3(0, 16f, 0), Quaternion.identity);
        newBoss.GetComponent<AIDestinationSetter>().target = playerTransform;
    }
    
}
