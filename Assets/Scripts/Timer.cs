using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static Timer Instance;
    public Text TimeCounter;
    public int SurvivalTime;
    private TimeSpan TimePlaying;
    private bool TimerGoing;
    private float ElapsedTime;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        TimeCounter.text = TimeSpan.FromSeconds((double) SurvivalTime).ToString("g");
        TimerGoing = false;
    }

    public void BeginTimer()
    {
        TimerGoing = true;
        ElapsedTime = 0f;
        StartCoroutine(UpdateTimer());
    }

    public void EndTimer()
    {
        TimerGoing = false;
    }

    private IEnumerator UpdateTimer()
    {
        while (TimerGoing)
        {
            ElapsedTime += Time.deltaTime;
            TimePlaying = TimeSpan.FromSeconds(ElapsedTime);
            //
            yield return null;
        }
    }
}
