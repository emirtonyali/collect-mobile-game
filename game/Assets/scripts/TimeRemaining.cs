using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TimeRemaining : MonoBehaviour
{
    public float timeRemaining = 120;
    public bool timerIsRunning = false;
    public TMP_Text timeText;
    public MainMove mainMove;

    private void Start()
    {
        // Starts the timer automatically
        timerIsRunning = true;
    }
    void Update()
    {
        if (timerIsRunning&&mainMove.gamePlayable)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {

                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
               

                this.gameObject.SetActive(false);
            }
        }
    }
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
