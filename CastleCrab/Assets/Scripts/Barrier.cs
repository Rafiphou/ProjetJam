using UnityEngine;

public class Barrier : MonoBehaviour
{
    public bool isPlacingBarrier = false;

    private Vector3 offset = new Vector3(1f, 1f, 0f); // Offset to position the barrier slightly above the mouse cursor

    private void OnEnable()
    {
        isPlacingBarrier = true;
        BarrierPlacementManager.Instance.chosenBarrier = this.gameObject;
    }

    private void Update()
    {
        if (isPlacingBarrier)
        {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            Color c = sr.color;
            c.a = 0.5f; // Set alpha to 50%
            sr.color = c;

            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10f; // Set this to the distance from the camera to the object
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = new Vector3(worldPosition.x + offset.x, worldPosition.y + offset.y, 0f);
        }
    }

    public void PlacingBarrier()
    {
        if (isPlacingBarrier && BarrierPlacementManager.Instance.isOverPlacementArea)
        {
            Debug.Log("Barrier placed at: " + BarrierPlacementManager.Instance.overBarrier.position);
            isPlacingBarrier = false;
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            Color c = sr.color;
            c.a = 1f; // Set alpha back to 100%
            sr.color = c;
            transform.position = BarrierPlacementManager.Instance.overBarrier.position;
        }
    }
}
