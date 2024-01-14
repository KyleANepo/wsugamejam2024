using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinBullet : MonoBehaviour
{
    [SerializeField] public float speed = 10f;
    [SerializeField] public float damage = 20f;
    [SerializeField] int destroyTime = 2;

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
        if (other.CompareTag("Player"))
        {
            // Add player damage function here! :3
            if(other.gameObject.TryGetComponent<HealthDisplay>(out HealthDisplay player))
            {
                player.TakeDamage(damage);
            }

            Destroy(gameObject);

        }
    }
}
