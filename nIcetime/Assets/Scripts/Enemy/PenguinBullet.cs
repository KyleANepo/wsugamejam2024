using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinBullet : MonoBehaviour
{
    [SerializeField] public float speed = 10f;
    [SerializeField] public int damage = 20;
    int destroyTime = 2;

    private void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Add player damage function here! :3

            Destroy(gameObject);
        }
    }
}
