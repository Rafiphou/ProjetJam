using UnityEngine;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    public List<Transform> checkpoints;
    public List<GameObject> specialEnemies;

    public AnimationCurve baseProbabilityCurve; // probabilité de spécial, hors vague, selon le temps de jeu
    public AnimationCurve waveProbabilityCurve; // probabilité de spécial, pendant une vague, selon le temps de jeu
    public GameObject prefabBaseEnemy;

    public AnimationCurve baseLobsterProbabilityCurve;
    public AnimationCurve waveLobsterProbabilityCurve;


    public float GetSpecialEnemyProbability(float gameTime, bool isWaveActive)
    {
        return isWaveActive ? waveProbabilityCurve.Evaluate(gameTime) : baseProbabilityCurve.Evaluate(gameTime);
    }

    public float GetLobsterProbability(float gameTime, bool isWaveActive)
    {
        return isWaveActive ? waveLobsterProbabilityCurve.Evaluate(gameTime) : baseLobsterProbabilityCurve.Evaluate(gameTime);
    }

    public void SpawnEnemy()
    {
        float gameTime = WaveManager.Instance.gameTime;
        bool isWave = WaveManager.Instance.isWaveActive;

        float specialProbability = GetSpecialEnemyProbability(gameTime, isWave);
        GameObject prefabChoisi = (Random.value < specialProbability) ? ChooseSpecialEnemy() : prefabBaseEnemy;
        GameObject obj = PoolManager.Instance.GetFromPool(prefabChoisi, transform.position, Quaternion.identity);
        if (obj != null)
        {
            Enemy enemy = obj.GetComponent<Enemy>();
            enemy.originalPrefab = prefabChoisi;
            enemy.SetRoute(checkpoints); // la liste de Transform de CE spawner
        }
    }

    GameObject ChooseSpecialEnemy()
    {
        float gameTime = WaveManager.Instance.gameTime;
        bool isWave = WaveManager.Instance.isWaveActive;

        float lobsterProbability = GetLobsterProbability(gameTime, isWave);
        return (Random.value < lobsterProbability) ? specialEnemies[0] : specialEnemies[1];
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
