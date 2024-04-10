using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [Header("Component")]
    public TextMeshProUGUI timerText;

    [Header("Timer Settings")]
    public float currentTime;
    public bool countDown;

    [Header("Limit Settings")]
    public bool hasLimit;
    public float timerLimit;

    private ITimerStrategy timerStrategy;

    void Start()
    {
        SetTimerStrategy();
    }

    void Update()
    {
        timerStrategy.UpdateTimer(ref currentTime, Time.deltaTime);

        if (hasLimit && ((countDown && currentTime <= timerLimit) || (!countDown && currentTime >= timerLimit)))
        {
            currentTime = timerLimit;
            timerText.color = Color.red;
            enabled = false;
        }
        SetTimerText();
    }

    private void SetTimerText()
    {
        timerText.text = currentTime.ToString("F2"); // Fixed two decimal places
    }

    private void SetTimerStrategy()
    {
        if (countDown)
        {
            timerStrategy = new CountDownStrategy();
        }
        else
        {
            timerStrategy = new CountUpStrategy();
        }
    }
}

public interface ITimerStrategy
{
    void UpdateTimer(ref float currentTime, float deltaTime);
}

public class CountUpStrategy : ITimerStrategy
{
    public void UpdateTimer(ref float currentTime, float deltaTime)
    {
        currentTime += deltaTime;
    }
}

public class CountDownStrategy : ITimerStrategy
{
    public void UpdateTimer(ref float currentTime, float deltaTime)
    {
        currentTime -= deltaTime;
        if (currentTime < 0f) currentTime = 0f;
    }
}