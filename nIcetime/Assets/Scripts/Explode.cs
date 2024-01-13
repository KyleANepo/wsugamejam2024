using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    public float GrenadeRadius = 10f;
    public float ExplosionForce = 1000f;
    public int Damage = 5;

    private void Start()
    {
        Explosion();
    }

    private void Explosion()
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
}
