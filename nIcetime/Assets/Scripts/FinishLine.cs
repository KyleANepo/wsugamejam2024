using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private int one = 1;
    public TimerDisplay td;

    private void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {

        if (one == 1 && (other.tag == ("Player")))
        {
            td.PauseTimer();
            one--;  //Code runs once only.
        }
    }
}
