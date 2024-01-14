using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KingPenguinAI : EnemyAI
{
    // Penguin Bullet
    public GameObject objectToThrow;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
    }

    public override void Update()
    {
        base.Update();
        // Check if player is detected
        if (Vector3.Distance(transform.position, player.position) < detectionRadius && !frozen)
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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
