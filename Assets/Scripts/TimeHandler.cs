using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeHandler : MonoBehaviour
{
    [SerializeField] [Range(0, 10f)] private float dayPeriod = 0.5f;
    private UnityEvent _onTimeChanged;
    private int _dayCount;
    private float timer;

    public UnityEvent OnTimeChanged => _onTimeChanged;
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
    }

    private void Start()
    {
        DayCount = 0;
        timer = dayPeriod;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer<= 0)
        {
            DayCount++;
            timer = dayPeriod;
        }
    }
}
