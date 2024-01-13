using System.Collections;
using System.Collections.Generic;
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
        if (targetHit)
            return;
        else targetHit = true;

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
