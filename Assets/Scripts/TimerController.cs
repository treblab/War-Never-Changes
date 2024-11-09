using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Input;
using UnityEngine;
using UnityEngine.UI;
using Naninovel;

public class TimerController : MonoBehaviour
{
    [SerializeField] private Image timerBar;
    private float timeRemaining; 
    public float maxTime = 30f;
    private bool timerActive = true;
    // Start is called before the first frame update
    void Start()
    {
        timeRemaining = maxTime; 
    }

    private void Update()
    {
        if(timeRemaining > 0 && timerActive)
        {
            timeRemaining -= Time.deltaTime;
            timerBar.fillAmount = timeRemaining / maxTime;
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
    }
    
    public void StopTimer()
    {
        timerActive = false;
    }
    
    
    private void OnTimerEnd()
    {
        // Perform actions when the timer ends, such as selecting a default choice
        Debug.Log("Timer has ended!");
    }

    [CommandAlias("startTimer")]    
    public class StartTimerCommand : Command
    {
        public override UniTask ExecuteAsync(AsyncToken aToken = default)
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
