using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private List<Transform> route;
    private int currentCheckpointIndex;
    public int moveSpeed;

    private int currentHealth;
    private int maxHealth;

    public GameObject originalPrefab;

    private void OnEnable()
    {
        currentHealth = maxHealth;
        currentCheckpointIndex = 0;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        StartCoroutine(DieRoutine());
    }

    private IEnumerator DieRoutine()
    {
        // Play death animation or effects here

        yield return new WaitForSeconds(1f); // Wait for the animation to finish

        PoolManager.Instance.ReturnToPool(originalPrefab, gameObject);
    }

    public void SetRoute(List<Transform> checkpoints)
    {
        route = checkpoints;
        currentCheckpointIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (route == null || route.Count == 0) return;

        Transform target = route[currentCheckpointIndex];
        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (route == null || currentCheckpointIndex >= route.Count) return;

        if (other.transform == route[currentCheckpointIndex])
        {
            currentCheckpointIndex++;
            if (currentCheckpointIndex >= route.Count)
            {
                Die();
                // Fin du jeu
            }
        }
    }
}
