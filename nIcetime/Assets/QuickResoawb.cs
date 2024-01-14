using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Its meant to say respawn oops
public class QuickResoawb : MonoBehaviour
{

    public Transform respawnPoint;
    public GameObject player;


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered");
        if (other.tag == "Player")
        {
            Debug.Log("Detected");
            player.transform.position = respawnPoint.position;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
