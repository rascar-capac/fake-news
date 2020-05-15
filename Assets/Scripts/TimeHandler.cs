using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeHandler : MonoBehaviour
{
    public int DayCount
    {
        get => dayCount;
        set
        {
            dayCount = value;
            onTimeChanged.Invoke();
            if(dayCount >= gameDuration)
            {
                onLastDayReached.Invoke();
            }
        }
    }
    public UnityEvent OnTimeChanged => onTimeChanged;
    public UnityEvent OnLastDayReached => onLastDayReached;

    [SerializeField] [Range(0, 10f)] private float dayPeriod = 1f;
    [SerializeField] private int gameDuration = 100;
    private int dayCount;
    private float timer;
    private GameManager gameManager;
    private UnityEvent onTimeChanged;
    private UnityEvent onLastDayReached;

    private void Awake()
    {
        dayCount = 0;
        timer = dayPeriod;
        gameManager = GetComponent<GameManager>();
        onTimeChanged = new UnityEvent();
        onLastDayReached = new UnityEvent();
    }

    private void Update()
    {
        if(gameManager.IsGameRunning)
        {
            timer -= Time.deltaTime;
            if(timer<= 0)
            {
                DayCount++;
                timer = dayPeriod;
            }
        }
    }
}
