using UnityEngine;

public class BarrierPlacement : MonoBehaviour
{
    public GameObject baseBarrierPlacement;
    public GameObject activeBarrierPlacement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        baseBarrierPlacement.SetActive(true);
        activeBarrierPlacement.SetActive(false);
    }

    private void OnMouseOver()
    {
        baseBarrierPlacement.SetActive(false);
        activeBarrierPlacement.SetActive(true);
        if (BarrierPlacementManager.Instance.chosenBarrier.GetComponent<Barrier>().isPlacingBarrier)
        {
            BarrierPlacementManager.Instance.isOverPlacementArea = true;
            BarrierPlacementManager.Instance.overBarrier = this.transform;
        }
    }

    private void OnMouseExit()
    {
        baseBarrierPlacement.SetActive(true);
        activeBarrierPlacement.SetActive(false);
        BarrierPlacementManager.Instance.isOverPlacementArea = false;
    }

    private void OnMouseDown()
    {
        if (BarrierPlacementManager.Instance.chosenBarrier.GetComponent<Barrier>().isPlacingBarrier)
        {
                BarrierPlacementManager.Instance.chosenBarrier.GetComponent<Barrier>().PlacingBarrier();
        }
    }
}
