using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HMPenguinAI : EnemyAI
{
    // Penguin Bullet
    public GameObject objectToThrow;
    [SerializeField] private float frequency = 3.0f;
    [SerializeField] private float magnitude = 3.0f;
    [SerializeField] private float offset = 0f;
    [SerializeField] private bool vertical = false;

    private Vector3 startPosition;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        startPosition = transform.position;
    }

    public override void Update()
    {
        base.Update();

        // Check if player is detected
        if (!frozen)
        {
            transform.LookAt(player);
            if (Vector3.Distance(transform.position, player.position) < detectionRadius)
            {
                // Player is detected
                Debug.Log("Player detected");
                AttackPlayer();
            }
            Move();
        }
    }

    private void Move()
    {
        if (!vertical)
        {
            transform.position = startPosition + transform.right * Mathf.Sin(Time.time * frequency + offset) * magnitude;
        }
        else
        {
            transform.position = startPosition + transform.up * Mathf.Sin(Time.time * frequency + offset) * magnitude;
        }
    }

    private void AttackPlayer()
    {

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
