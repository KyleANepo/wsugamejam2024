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

    public int finalScore;

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
            timerText.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds) + "  Score: " + string.Format("{00}",HealthDisplay.scoreSubtract);
        }
        if (minutes == 10)
        {
            PauseTimer();
            
        }
    }

    public void PauseTimer()
    {
        timerActive = false;
        finalScore = ((int)(1000 - (elapsedTime - HealthDisplay.scoreSubtract)));
        timerText.text = " ";

    }

    public int returnScore()
    {
        return finalScore;
    }



}
