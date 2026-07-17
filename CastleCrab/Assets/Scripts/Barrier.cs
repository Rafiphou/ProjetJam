using System.Collections;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    public bool isPlacingBarrier = false;

    private Vector3 offset = new Vector3(1f, 1f, 0f); // Offset to position the barrier slightly above the mouse cursor

    private int healthPoints = 100; // Example health points for the barrier

    public GameObject originalPrefab; // Reference to the original prefab for pooling

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
            isPlacingBarrier = false;
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            Color c = sr.color;
            c.a = 1f; // Set alpha back to 100%
            sr.color = c;
            transform.position = BarrierPlacementManager.Instance.overBarrier.position;
        }
    }

    public void TakeDamage(int damage)
    {
        healthPoints -= damage;
        if (healthPoints <= 0)
        {
            StartCoroutine(DestroyRoutine());
        }
        Debug.Log("Aieu! Barrier took damage. Current health: " + healthPoints);
    }

    private IEnumerator DestroyRoutine()
    {
        // Play any destruction animation or effects here

        yield return new WaitForSeconds(1f); // Wait for any death animation or effects

        BarrierPoolManager.Instance.ReturnToBarrierPool(originalPrefab, gameObject);
    }
}
