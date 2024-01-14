using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    [Header("Audio")]
    public AudioSource hit1;
    public AudioSource hit2;
    public AudioSource hit3;

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

        int randomNumber = Random.Range(0, 2);
        if (randomNumber == 0) hit1.Play();
        else if (randomNumber == 1) hit2.Play();
        else hit3.Play();

        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }

    public virtual void DestroyEnemy()
    {
        frozen = true;
        GameObject freeze = Instantiate(Ice, transform.position, transform.rotation);
        // Destroy(gameObject);
    }
}
