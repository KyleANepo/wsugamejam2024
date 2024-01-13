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
    [SerializeField]
    protected GameObject Ice;

    protected LayerMask playerLayer; // Set this in the inspector to the layer where your player is.
    protected Transform player;
    protected bool alreadyAttacked;
    protected bool frozen;

    public virtual void Update()
    {
        if (frozen)
        {
            Frozen();
        }
    }

    public virtual void Frozen()
    {

    }

    public virtual void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }

    public virtual void DestroyEnemy()
    {
        frozen = true;
        GameObject freeze = Instantiate(Ice, transform.position, transform.rotation);
        // Destroy(gameObject);
    }
}
