using UnityEngine;
using System.Collections.Generic;

public class BarrierPoolManager : MonoBehaviour
{
    public static BarrierPoolManager Instance { get; private set; }

    [System.Serializable]
    public class barrierPoolConfig
    {
        public GameObject barrierPrefab;
        public int barrierPoolSize;
    }

    public List<barrierPoolConfig> barrierPoolConfigs;

    private Dictionary<GameObject, Queue<GameObject>> barrierPools = new Dictionary<GameObject, Queue<GameObject>>();

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
        foreach (var config in barrierPoolConfigs)
        {
            Queue<GameObject> queue = new Queue<GameObject>();
            for (int i = 0; i < config.barrierPoolSize; i++)
            {
                GameObject obj = Instantiate(config.barrierPrefab);
                obj.SetActive(false);
                queue.Enqueue(obj);
            }
            barrierPools.Add(config.barrierPrefab, queue);
        }
    }

    public GameObject GetFromBarrierPool(GameObject barrierPrefab, Vector3 position, Quaternion rotation)
    {
        if (!barrierPools.ContainsKey(barrierPrefab))
        {
            Debug.LogWarning($"No pool found for prefab: {barrierPrefab.name}");
            return null;
        }
        Queue<GameObject> queue = barrierPools[barrierPrefab];
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

    public void ReturnToBarrierPool(GameObject barrierPrefab, GameObject obj)
    {
        if (!barrierPools.ContainsKey(barrierPrefab))
        {
            Debug.LogWarning($"No pool found for prefab: {barrierPrefab.name}");
            Destroy(obj);
            return;
        }
        obj.SetActive(false);
        barrierPools[barrierPrefab].Enqueue(obj);
    }
}
