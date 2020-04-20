﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PopulationHandler : MonoBehaviour
{
    [SerializeField] [Range(0, int.MaxValue)] private int startingTrustLevel = 0;
    [SerializeField] [Range(0, int.MaxValue)] private int startingParanoiaLevel = 0;
    [SerializeField] [Range(0, int.MaxValue)] private int startingContaminationLevel = 0;
    [SerializeField] [Range(0, int.MaxValue)] private int startingCasualtiesCount = 0;
    [SerializeField] [Range(0, 1f)] private float randomContaminationVariationProbability = 0.1f;
    [SerializeField] private TimeHandler timeHandler = null;
    private UnityEvent _onTrustLevelChanged;
    private UnityEvent _onParanoiaLevelChanged;
    private UnityEvent _onContaminationLevelChanged;
    private UnityEvent _onCasualtiesCountChanged;
    private int _trustLevel;
    private int _paranoiaLevel;
    private int _contaminationLevel;
    private int _casualtiesCount;
    private float timer;

    public UnityEvent OnTrustLevelChanged => _onTrustLevelChanged;
    public UnityEvent OnParanoiaLevelChanged => _onParanoiaLevelChanged;
    public UnityEvent OnContaminationLevelChanged => _onContaminationLevelChanged;
    public UnityEvent OnCasualtiesCountChanged => _onCasualtiesCountChanged;
    public int TrustLevel
    {
        get => _trustLevel;
        set
        {
            _trustLevel = Mathf.Clamp(value, 0, 100);
            OnTrustLevelChanged.Invoke();
        }
    }
    public int ParanoiaLevel
    {
        get => _paranoiaLevel;
        set
        {
            _paranoiaLevel = Mathf.Clamp(value, 0, 100);
            OnParanoiaLevelChanged.Invoke();
        }
    }
    public int ContaminationLevel
    {
        get => _contaminationLevel;
        set
        {
            _contaminationLevel = Mathf.Clamp(value, 0, 100);
            OnContaminationLevelChanged.Invoke();
        }
    }
    public int CasualtiesCount
    {
        get => _casualtiesCount;
        set
        {
            _casualtiesCount = Mathf.Clamp(value, 0, 100);
            OnCasualtiesCountChanged.Invoke();
        }
    }

    private void Awake()
    {
        _onTrustLevelChanged = new UnityEvent();
        _onParanoiaLevelChanged = new UnityEvent();
        _onContaminationLevelChanged = new UnityEvent();
        _onCasualtiesCountChanged = new UnityEvent();
    }

    private void Start()
    {
        timeHandler.OnTimeChanged.AddListener(ApplyRandomContaminationVariation);
        TrustLevel = startingTrustLevel;
        ParanoiaLevel = startingParanoiaLevel;
        ContaminationLevel = startingContaminationLevel;
        CasualtiesCount = startingCasualtiesCount;
    }

    private void ApplyRandomContaminationVariation()
    {
        if(Random.value <= randomContaminationVariationProbability)
        {
            ContaminationLevel += Random.Range(1, 10);
        }
    }
}
