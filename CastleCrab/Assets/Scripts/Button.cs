using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject barrierPrefab;
    
    
    
    public void SpawnBarrier()
    {
        BarrierPoolManager.Instance.GetFromBarrierPool(barrierPrefab, transform.position, barrierPrefab.transform.rotation);
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
