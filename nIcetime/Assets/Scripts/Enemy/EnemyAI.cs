using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyAI : MonoBehaviour
{
    [SerializeField]
    protected int health;
    [SerializeField]
    protected int detectionRadius;
    [SerializeField]
    protected float timeBetweenAttacks;

    protected LayerMask playerLayer; // Set this in the inspector to the layer where your player is.
    protected Transform player;
    protected bool alreadyAttacked;

    public virtual void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }

    public virtual void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
