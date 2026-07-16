using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance { get; private set; }

    public float waveInterval = 60f; // Interval between waves in seconds
    public float waveDuration = 20f; // Duration of each wave in seconds
    public bool isWaveActive { get; private set; }
    public float gameTime { get; private set; }

    public float waveTimer { get; private set; }

    public float SpawnRate;
    public List<GameObject> Spawners;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        gameTime += Time.deltaTime;
        waveTimer += Time.deltaTime;

        if (!isWaveActive && waveTimer >= waveInterval)
        {
            isWaveActive = true;
            waveTimer = 0f;
        }
        else if (isWaveActive && waveTimer >= waveDuration)
        {
            isWaveActive = false;
            waveTimer = 0f;
        }


    }

    private void Start()
    {
        GameObject chosenSpawner = Spawners[Random.Range(0, Spawners.Count)];
        chosenSpawner.GetComponent<Spawner>().SpawnEnemy();
    }
}
