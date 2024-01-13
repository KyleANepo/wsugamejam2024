using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PenguinAI : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] float detectionRadius = 5f;

    public GameObject objectToThrow;

    public LayerMask playerLayer; // Set this in the inspector to the layer where your player is.

    public Transform player;


    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
    }

    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, playerLayer);

        // Check if player is detected
        if (colliders.Length > 0)
        {
            // Player is detected
            Debug.Log("Player detected");
            AttackPlayer();
        }
    }

    private void AttackPlayer()
    {
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            GameObject bullet = Instantiate(objectToThrow, transform.position, transform.rotation);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

}
