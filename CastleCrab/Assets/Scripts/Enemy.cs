using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private List<Transform> route;
    private int currentCheckpointIndex;
    public float initialMoveSpeed = 0.8f;
    private float moveSpeed;

    private int currentHealth;
    private int maxHealth;

    public GameObject originalPrefab;

    private bool isDead = false;

    private bool isAgainstBarrier = false;
    public GameObject barrierFaced;
    private bool isAttackingBarrier = false;

    private void OnEnable()
    {
        currentHealth = maxHealth;
        currentCheckpointIndex = 0;
        moveSpeed = initialMoveSpeed;
        isAgainstBarrier = false;
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
        isDead = true;
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
        if (!isDead)
        {
            if (!isAgainstBarrier)
            {
                moveSpeed = initialMoveSpeed;
                barrierFaced = null;
                isAttackingBarrier = false;
                if (route == null || route.Count == 0) return;

                Transform target = route[currentCheckpointIndex];
                transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, target.position) < 0.05f)
                {
                    if (currentCheckpointIndex <= route.Count - 1)
                    {
                        currentCheckpointIndex++;
                    }
                    if (currentCheckpointIndex >= route.Count)
                    {
                        Die();
                        // Fin du jeu
                    }
                }
            }
            else
            {
                FacingBarrier();
            }
            
        }
        
    }

    private void FacingBarrier()
    {
        moveSpeed = 0f;

        if (!isAttackingBarrier)
        {
            isAttackingBarrier = true;
            StartCoroutine(AttackBarrier());
        }

       // StartCoroutine(AttackBarrier());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Barrier"))
        {
            isAgainstBarrier = true;
            barrierFaced = collision.gameObject;
        }
    }

    private IEnumerator AttackBarrier()
    {
        yield return new WaitForSeconds(1f); // Wait for 1 second before attacking
        if (barrierFaced != null)
        {
            Barrier barrierScript = barrierFaced.GetComponent<Barrier>();
            if (barrierScript != null)
            {
                barrierScript.TakeDamage(1); // Deal 10 damage to the barrier
                isAttackingBarrier = false; // Reset the attacking state after dealing damage
            }
        }
    }
}
