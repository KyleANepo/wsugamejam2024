using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    float elapsedTime;
    private int minutes;
    private int seconds;
    private bool timerActive;

    private void Start()
    {
        timerActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerActive)
        {
            elapsedTime += Time.deltaTime;
            minutes = Mathf.FloorToInt(elapsedTime / 60);
            seconds = Mathf.FloorToInt(elapsedTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        if (minutes == 10)
        {
            PauseTimer();
            
        }
    }

    public string PauseTimer()
    {
        timerActive = false;
        return timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

    }
}
