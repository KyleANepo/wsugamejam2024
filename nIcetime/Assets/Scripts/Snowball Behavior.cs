using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SnowballBehavior : MonoBehaviour
{
    private Rigidbody rb;

    private bool targetHit;

    public TrailRenderer Trail;

    // public GameObject explosion;

    [SerializeField] int destroyTime = 3;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Trail = GetComponentInChildren<TrailRenderer>();
        Destroy(gameObject, destroyTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            EnemyAI target = collision.transform.gameObject.GetComponent<EnemyAI>();
            target.TakeDamage(1);
        }

        // GameObject exp = Instantiate(explosion, transform.position, transform.rotation);

        //Make sure projectile moves with target
        rb.isKinematic = true;

        //Make sure projectile moves with target
        transform.SetParent(collision.transform);

        Trail.transform.parent = null;
        Trail.autodestruct = true;

        // Destroy(exp, 0.5f);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
