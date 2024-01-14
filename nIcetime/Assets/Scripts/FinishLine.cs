using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private int one = 1;
    TimerDisplay td;
    string finishedTime;

    private void Start()
    {
        td = GameObject.FindGameObjectWithTag("Bts").GetComponent<TimerDisplay>();
    }

    private void OnTriggerEnter()
    {

        if (one == 1)
        {
            finishedTime = td.PauseTimer();
            one--;  //Code runs once only.
        }
    }
}
