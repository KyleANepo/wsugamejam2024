using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowballBehavior : MonoBehaviour
{
    private Rigidbody rb;

    private bool targetHit;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
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

        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
