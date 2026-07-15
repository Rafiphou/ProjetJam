using UnityEngine;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    public List<GameObject> checkpoints;
    
    public void SpawnEnemy()
    {
        //GameObject obj = PoolManager.Instance.GetFromPool(prefabChoisi, transform.position, Quaternion.identity);
        //if (obj != null)
        {
            //Enemy enemy = obj.GetComponent<Enemy>();
            //enemy.originPrefab = prefabChoisi;
            //enemy.SetRoute(checkpoints); // la liste de Transform de CE spawner
        }
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
