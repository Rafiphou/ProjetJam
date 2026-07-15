using UnityEngine;
using System.Collections.Generic;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { get; private set; }

    [System.Serializable]
    public class PoolConfig
    {
        public GameObject enemyPrefab;
        public int poolSize;
    }

    public List<PoolConfig> poolConfigs;

    private Dictionary<GameObject, Queue<GameObject>> pools = new Dictionary<GameObject, Queue<GameObject>>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }

        foreach (var config in poolConfigs)
        {
            Queue<GameObject> queue = new Queue<GameObject>();
            for (int i = 0; i < config.poolSize; i++)
            {
                GameObject obj = Instantiate(config.enemyPrefab);
                obj.SetActive(false);
                queue.Enqueue(obj);
            }
            pools.Add(config.enemyPrefab, queue);
        }
    }

    public GameObject GetFromPool(GameObject enemyPrefab, Vector3 position, Quaternion rotation)
    {
        if (!pools.ContainsKey(enemyPrefab))
        {
            Debug.LogWarning($"No pool found for prefab: {enemyPrefab.name}");
            return null;
        }

        Queue<GameObject> queue = pools[enemyPrefab];

        if (queue.Count == 0)
        {
            return null;
        }

        GameObject obj = queue.Dequeue();
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        obj.SetActive(true);
        return obj;
    }

    public void ReturnToPool(GameObject enemyPrefab, GameObject instance)
    {
        instance.SetActive(false);
        pools[enemyPrefab].Enqueue(instance);
    }
}
