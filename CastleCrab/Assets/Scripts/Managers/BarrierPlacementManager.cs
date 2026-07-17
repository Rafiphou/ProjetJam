using UnityEngine;

public class BarrierPlacementManager : MonoBehaviour
{
    public static BarrierPlacementManager Instance { get; set; }

    public bool isOverPlacementArea = false;

    public Transform overBarrier;
    public GameObject chosenBarrier;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
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
