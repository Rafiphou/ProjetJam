using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance { get; private set; }

    public float waveInterval = 10f; // Interval between waves in seconds
    public float waveDuration = 10f; // Duration of each wave in seconds
    public bool isWaveActive { get; private set; }
    public float gameTime { get; private set; }

    public float waveTimer { get; private set; }

    private float spawnTimer = 0f;
    public List<GameObject> Spawners;

    public float minimalSpawnRate = 5f;
    public float maximalSpawnRate = 7f;
    public float originalMinimalSpawnRate = 5f;
    public float originalMaximalSpawnRate = 7f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private bool CheckSpawnTimer(float time)
    {
        return (time >= 5.5 && time - 5.5 <= 0.0015) ||
               (time >= 6.0 && time - 6.0 <= 0.0015) ||
               (time >= 6.5 && time - 6.5 <= 0.0015);
    }

    // Update is called once per frame
    void Update()
    {
        gameTime += Time.deltaTime;
        waveTimer += Time.deltaTime;

        if (!isWaveActive && waveTimer >= waveInterval)
        {
            Debug.Log("Wave started!");
            isWaveActive = true;
            changeMinimalSpawnRate(1);
            changeMaximalSpawnRate(3);
            waveTimer = 0f;
        }
        else if (isWaveActive && waveTimer >= waveDuration)
        {
            Debug.Log("Wave ended!");
            isWaveActive = false;
            changeMinimalSpawnRate(originalMinimalSpawnRate);
            changeMaximalSpawnRate(originalMaximalSpawnRate);
            waveTimer = 0f;
        }
        
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= minimalSpawnRate)
        {
            if (spawnTimer >= maximalSpawnRate)
            {
                spawnTimer = 0f;
                InitiateSpawn();
            }
            else
            {
                if (CheckSpawnTimer(spawnTimer))
                {
                    float random = Random.Range(0f, 1f);
                    if (random < 0.5f)
                    {
                        spawnTimer = 0f;
                        InitiateSpawn();
                    }
                }
            }
        }
    }

    private void InitiateSpawn()
    {
        GameObject chosenSpawner = Spawners[Random.Range(0, Spawners.Count)];
        chosenSpawner.GetComponent<Spawner>().SpawnEnemy();
    }

    public void changeMinimalSpawnRate(float new_min)
    {
        minimalSpawnRate = new_min;
    }

    public void changeMaximalSpawnRate(float new_max)
    {
        maximalSpawnRate = new_max;
    }
}
