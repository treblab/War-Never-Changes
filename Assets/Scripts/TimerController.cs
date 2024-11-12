using System;
using System.Collections;
using System.Collections.Generic;
using Naninovel;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer timerBar;
    public float maxTime = 10f;
    private float timeRemaining; 
    private bool timerActive = true;
    private Vector3 initialScale;

    void Start()
    {
        timeRemaining = maxTime;
        initialScale = timerBar.transform.localScale; // Store the initial scale of the timer bar
    }

    void Update()
    {
        if (timerActive && timeRemaining > 0)
        {
            // Update the remaining time
            timeRemaining -= Time.deltaTime;

            // Calculate the new scale for the timer bar
            float scaleRatio = timeRemaining / maxTime;
            timerBar.transform.localScale = new Vector3(initialScale.x * scaleRatio, initialScale.y, initialScale.z);

            // Check if the time has run out
            if (timeRemaining <= 0)
            {
                timeRemaining = 0;
                timerActive = false;
                OnTimerEnd();
            }
        }
    }

    public void StartTimer()
    {
        timerActive = true;
        timeRemaining = maxTime;
        timerBar.transform.localScale = initialScale; // Reset to initial scale
    }

    public void StopTimer()
    {
        timerActive = false;
    }

    private void OnTimerEnd()
    {
        Debug.Log("Timer has ended!");
    }

    // Naninovel Commands
    [CommandAlias("startTimer")]
    public class StartTimerCommand : Command
    {
        public override UniTask ExecuteAsync(AsyncToken asyncToken = default)
        {
            GameObject.FindObjectOfType<TimerController>()?.StartTimer();
            return UniTask.CompletedTask;
        }
    }

    [CommandAlias("stopTimer")]
    public class StopTimerCommand : Command
    {
        public override UniTask ExecuteAsync(AsyncToken asyncToken = default)
        {
            GameObject.FindObjectOfType<TimerController>()?.StopTimer();
            return UniTask.CompletedTask;
        }
    }
}
