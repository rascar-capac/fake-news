using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PopulationHandler : MonoBehaviour
{
    [SerializeField] [Range(0, 100)] private int startingTrustLevel = 0;
    [SerializeField] [Range(0, 100)] private int startingParanoiaLevel = 0;
    [SerializeField] [Range(0, 100)] private int startingContaminationLevel = 0;
    [SerializeField] [Range(0, 100)] private int startingCasualtiesCount = 0;
    [SerializeField] [Range(0, 1f)] private float randomContaminationVariationProbability = 0.1f;
    [SerializeField] private TimeHandler timeHandler = null;
    private UnityEvent _onTrustLevelChanged;
    private UnityEvent _onParanoiaLevelChanged;
    private UnityEvent _onContaminationLevelChanged;
    private UnityEvent _onCasualtiesCountChanged;
    private UnityEvent _onFullContamination;
    private int _trustLevel;
    private int _paranoiaLevel;
    private int _contaminationLevel;
    private int _casualtiesCount;
    private float timer;

    public UnityEvent OnTrustLevelChanged => _onTrustLevelChanged;
    public UnityEvent OnParanoiaLevelChanged => _onParanoiaLevelChanged;
    public UnityEvent OnContaminationLevelChanged => _onContaminationLevelChanged;
    public UnityEvent OnCasualtiesCountChanged => _onCasualtiesCountChanged;
    public UnityEvent OnFullContamination => _onFullContamination;
    public int TrustLevel
    {
        get => _trustLevel;
        set
        {
            if(value != _trustLevel)
            {
                _trustLevel = Mathf.Clamp(value, 0, 100);
                OnTrustLevelChanged.Invoke();
            }
        }
    }
    public int ParanoiaLevel
    {
        get => _paranoiaLevel;
        set
        {
            if(value != _paranoiaLevel)
            {
                _paranoiaLevel = Mathf.Clamp(value, 0, 100);
                OnParanoiaLevelChanged.Invoke();
            }
        }
    }
    public int ContaminationLevel
    {
        get => _contaminationLevel;
        set
        {
            if(value != _contaminationLevel)
            {
                _contaminationLevel = Mathf.Clamp(value, 0, 100);
                if(_contaminationLevel == 100)
                {
                    OnFullContamination.Invoke();
                }
                OnContaminationLevelChanged.Invoke();
            }
        }
    }
    public int CasualtiesCount
    {
        get => _casualtiesCount;
        set
        {
            if(value != _casualtiesCount)
            {
                _casualtiesCount = Mathf.Clamp(value, 0, 100);
                OnCasualtiesCountChanged.Invoke();
            }
        }
    }

    private void Awake()
    {
        _onTrustLevelChanged = new UnityEvent();
        _onParanoiaLevelChanged = new UnityEvent();
        _onContaminationLevelChanged = new UnityEvent();
        _onCasualtiesCountChanged = new UnityEvent();
        _onFullContamination = new UnityEvent();
    }

    private void Start()
    {
        timeHandler.OnTimeChanged.AddListener(ApplyRandomContaminationVariation);
        _trustLevel = startingTrustLevel;
        _paranoiaLevel = startingParanoiaLevel;
        _contaminationLevel = startingContaminationLevel;
        _casualtiesCount = startingCasualtiesCount;
    }

    private void ApplyRandomContaminationVariation()
    {
        if(Random.value <= randomContaminationVariationProbability)
        {
            ContaminationLevel += Random.Range(1, 10);
        }
    }
}
