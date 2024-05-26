using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class TimerScript : MonoBehaviour
{
    public PlayerController pc;
   
    private float startTime;
    private bool timerActive = false;
    /*    public void StartTimer(float duration)
        {
            startTime = duration;
            timerActive = true;
        }*/

    private void Start()
    {
        startTime = 480;
        timerActive = true;
    }

    private void Update()
    {
        if (timerActive)
        {
            startTime -= Time.deltaTime;
            UpdateTimer(startTime);
        }
        if(startTime <= 0)
        {
            timerActive = false;
            GameManager.S.EndGame();
        }
    }

    void UpdateTimer(float currentTime)
    {
        currentTime = Mathf.Max(0, currentTime);
        UIManager.instance.UpdateTimerUI(currentTime);
    }

    public void StopTimer()
    {
        timerActive = false;
        pc.setDieStatus();
        if(startTime <= 0)
        {
            UIManager.instance.TimesUpMessage();
            UIManager.instance.ShowButton();
        }
    }
}
