using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportFix : MonoBehaviour
{
    public Transform tpFix;
    public Transform player;

    private void OnTriggerStay(Collider collision)
    {
        if ( collision.tag == "Player")
        {
         
             //Debug.Log("DETECTED PLAYER");
             player.transform.position = new Vector3(player.position.x,tpFix.position.y,tpFix.position.z);
            
        }
    }


}
