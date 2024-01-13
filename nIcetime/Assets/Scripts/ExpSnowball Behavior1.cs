using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ExpSnowballBehavior : MonoBehaviour
{
    public float GrenadeRadius = 10f;
    public float ExplosionForce = 1000f;
    public int Damage = 5;

    public GameObject explosion;

    private Rigidbody rb;

    private bool targetHit;

    public TrailRenderer Trail;

    public string objectLayer = "YourObjectLayer";
    public float disableDuration = 1f;

    private float timer;

    void Start()
    {
        // Set initial timer value
        timer = disableDuration;

        // Disable collisions between the object and all layers initially
        DisableAllCollisions();


        rb = GetComponent<Rigidbody>();
        Trail = GetComponentInChildren<TrailRenderer>();
    }

    void Update()
    {
        // Countdown the timer
        timer -= Time.deltaTime;

        // Re-enable collisions after the first second
        if (timer <= 0f)
        {
            EnableAllCollisions();
        }
    }

    void DisableAllCollisions()
    {
        // Get the layer index of the object
        int objectLayerIndex = LayerMask.NameToLayer(objectLayer);

        // Disable collisions between the object's layer and all layers
        for (int i = 0; i < 32; i++)
        {
            Physics.IgnoreLayerCollision(objectLayerIndex, i, true);
        }
    }

    void EnableAllCollisions()
    {
        // Get the layer index of the object
        int objectLayerIndex = LayerMask.NameToLayer(objectLayer);

        // Enable collisions between the object's layer and all layers
        for (int i = 0; i < 32; i++)
        {
            Physics.IgnoreLayerCollision(objectLayerIndex, i, false);
        }

        // Disable this script to stop further updates
        enabled = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Explode();

        //Make sure projectile moves with target
        rb.isKinematic = true;

        //Make sure projectile moves with target
        transform.SetParent(collision.transform);

        Trail.transform.parent = null;
        Trail.autodestruct = true;

        Destroy(gameObject);
    }

    private void Explode()
    {
        //Add explosion later....
        //Instantiate(ExplosionEffect, transform.position, transform.rotation);
        Collider[] touchedObjects = Physics.OverlapSphere(transform.position, GrenadeRadius);

        foreach (Collider touchedObject in touchedObjects)
        {
            Rigidbody rigidbody = touchedObject.GetComponent<Rigidbody>();
            if (rigidbody != null)
            {
                Debug.Log("Player in radius!!!!");
                rigidbody.AddExplosionForce(ExplosionForce, transform.position, GrenadeRadius, 100.0f);
            }

            if (touchedObject.gameObject.tag == "Enemy")
            {
                var target = touchedObject.gameObject.GetComponent<EnemyAI>();
                target.TakeDamage(Damage);
            }

        }
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, GrenadeRadius);
    }
}
