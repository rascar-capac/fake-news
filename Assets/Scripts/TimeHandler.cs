using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeHandler : MonoBehaviour
{
    [SerializeField] [Range(0, 10f)] private float dayPeriod = 0.5f;
    [SerializeField] private int gameDuration = 100;
    [SerializeField] private GameManager gameManager = null;
    private UnityEvent _onTimeChanged;
    private UnityEvent _onLastDayReached;
    private int _dayCount;
    private float timer;

    public UnityEvent OnTimeChanged => _onTimeChanged;
    public UnityEvent OnLastDayReached => _onLastDayReached;
    public int DayCount
    {
        get => _dayCount;
        set
        {
            _dayCount = value;
            OnTimeChanged.Invoke();
        }
    }

    private void Awake()
    {
        _onTimeChanged = new UnityEvent();
        _onLastDayReached = new UnityEvent();
    }

    private void Start()
    {
        _dayCount = 0;
        timer = dayPeriod;
    }

    private void Update()
    {
        if(gameManager.IsGameRunning)
        {
            timer -= Time.deltaTime;
            if(timer<= 0)
            {
                DayCount++;
                if(DayCount >= gameDuration)
                {
                    _onLastDayReached.Invoke();
                }
                timer = dayPeriod;
            }
        }
    }
}
