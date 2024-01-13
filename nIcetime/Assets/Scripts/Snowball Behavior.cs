using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SnowballBehavior : MonoBehaviour
{
    private Rigidbody rb;

    private bool targetHit;

    public TrailRenderer Trail;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Trail = GetComponentInChildren<TrailRenderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            EnemyAI target = collision.transform.gameObject.GetComponent<EnemyAI>();
            target.TakeDamage(1);
        }

        //Make sure projectile moves with target
        rb.isKinematic = true;

        //Make sure projectile moves with target
        transform.SetParent(collision.transform);

        Trail.transform.parent = null;
        Trail.autodestruct = true;


        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
